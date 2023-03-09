// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using rei_esperantolib.Models.Email;

namespace rei_identityserver.Quickstart.Account;

/// <summary>
/// This sample controller implements a typical login/logout/provision workflow for local and external accounts.
/// The login service encapsulates the interactions with the user data store. This data store is in-memory only and cannot be used for production!
/// The interaction service provides a way for the UI to communicate with identityserver for validation and context retrieval
/// </summary>
[SecurityHeaders]
[AllowAnonymous]
public class AccountController : Controller
{
    private readonly TestUserStore _users;
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IClientStore _clientStore;
    private readonly IAuthenticationSchemeProvider _schemeProvider;
    private readonly IEventService _events;
    private readonly SignInManager<Usuario> _signInManager;
    private readonly UserManager<Usuario> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IEnvioEmail _envioEmail;

    public AccountController(
        IIdentityServerInteractionService interaction,
        IClientStore clientStore,
        IAuthenticationSchemeProvider schemeProvider,
        IEventService events,
        SignInManager<Usuario> p_signInManager,
        UserManager<Usuario> p_userManager,
        RoleManager<IdentityRole> p_roleManager,
        IEnvioEmail p_envioEmail,
        TestUserStore users = null)
    {
        // if the TestUserStore is not in DI, then we'll just use the global users collection
        // this is where you would plug in your own custom identity management library (e.g. ASP.NET Identity)
        _users = users ?? new TestUserStore(TestUsers.Users);

        _interaction = interaction;
        _clientStore = clientStore;
        _schemeProvider = schemeProvider;
        _events = events;

        _signInManager = p_signInManager;
        _userManager = p_userManager;
        _roleManager = p_roleManager;
        _envioEmail = p_envioEmail;
    }

    /// <summary>
    /// Entry point into the login workflow
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Login(string returnUrl)
    {
        // build a model so we know what to show on the login page
        var vm = await BuildLoginViewModelAsync(returnUrl);

        if (vm.IsExternalLoginOnly)
        {
            // we only have one option for logging in and it's an external provider
            return RedirectToAction("Challenge", "External", new { scheme = vm.ExternalLoginScheme, returnUrl });
        }

        return View(vm);
    }

    /// <summary>
    /// Handle postback from username/password login
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginInputModel model, string button)
    {
        try
        {
            var m_database = new ApplicationContext(new ConnectionStringUtils().CM_GetConnectionString());
            m_database.Users.ToList().Any();
        }
        catch(Exception)
        {
            TempData["Erro"] = "A base de dados atual não está configurada para receber solicitações de ASP.NET Identity Core e IdentityServer.";
            var m_model = new ConnectionStringUtils().CM_GetConnectionStringDoArquivoAppSettingsJson();
            return View("Alterar", m_model);
        }

        // check if we are in the context of an authorization request
        var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

        // the user clicked the "cancel" button
        if (button != "login")
        {
            if (context != null)
            {
                // if the user cancels, send a result back into IdentityServer as if they 
                // denied the consent (even if this client does not require consent).
                // this will send back an access denied OIDC error response to the client.
                await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

                // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                if (context.IsNativeClient())
                {
                    // The client is native, so this change in how to
                    // return the response is for better UX for the end user.
                    return this.LoadingPage("Redirect", model.ReturnUrl);
                }

                return Redirect(model.ReturnUrl);
            }
            else
            {
                // since we don't have a valid context, then we just go back to the home page
                return Redirect("~/");
            }
        }

        if (ModelState.IsValid)
        {
            var m_usuario = await _userManager.FindByNameAsync(model.Username);
            var m_emailEstaConfirmado = await _userManager.IsEmailConfirmedAsync(m_usuario);
            if (m_emailEstaConfirmado == false)
            {
                ModelState.AddModelError(string.Empty, AccountOptions.C_EmailNaoConfirmado);
                return View(await BuildLoginViewModelAsync(model));
            }
                
            var m_resultado = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberLogin, true);
            if (m_resultado.Succeeded)
            {
                await _events.RaiseAsync(new UserLoginSuccessEvent(m_usuario.UserName, m_usuario.Id, m_usuario.UserName, clientId: context?.Client.ClientId));

                if (context != null)
                {
                    if (context.IsNativeClient())
                    {
                        // The client is native, so this change in how to
                        // return the response is for better UX for the end user.
                        return this.LoadingPage("Redirect", model.ReturnUrl);
                    }

                    // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                    return Redirect(model.ReturnUrl);
                }

                // request for a local page
                if (Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                else if (string.IsNullOrEmpty(model.ReturnUrl))
                {
                    return Redirect("~/");
                }
                else
                {
                    // user might have clicked on a malicious link - should be logged
                    throw new Exception("invalid return URL");
                }
            }

            if (m_resultado.RequiresTwoFactor)
                return RedirectToAction(nameof(CM_LoginDoisFatores), new { p_nome = model.Username, p_lembrar = model.RememberLogin, p_urlRetorno = model.ReturnUrl });

            if(m_resultado.IsLockedOut)
            {
                var m_linkEsqueciASenha = Url.Action(nameof(CM_EsqueciASenha), "Account", new { }, Request.Scheme);
                var m_conteudo = string.Format("Seu acesso está bloqueado, para liberar sua conta altere a senha pelo link: {0}", m_linkEsqueciASenha);

                var m_mensagem = new Mensagem(new string[] { m_usuario.Email }, "Acesso R2 bloqueado", m_conteudo, null);
                await _envioEmail.CM_PreparacaoEEnvioDeEmailAsync(m_mensagem);

                ModelState.AddModelError(string.Empty, "Seu acesso está bloqueado!");
                return View(await BuildLoginViewModelAsync(model));
            }

            await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials", clientId: context?.Client.ClientId));
            ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);
        }

        // something went wrong, show form with error
        var vm = await BuildLoginViewModelAsync(model);
        return View(vm);
    }


    /// <summary>
    /// Show logout page
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Logout(string logoutId)
    {
        // build a model so the logout page knows what to display
        var vm = await BuildLogoutViewModelAsync(logoutId);

        if (vm.ShowLogoutPrompt == false)
        {
            // if the request for logout was properly authenticated from IdentityServer, then
            // we don't need to show the prompt and can just log the user out directly.
            return await Logout(vm);
        }

        return View(vm);
    }

    /// <summary>
    /// Handle logout page postback
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout(LogoutInputModel model)
    {
        // build a model so the logged out page knows what to display
        var vm = await BuildLoggedOutViewModelAsync(model.LogoutId);

        if (User?.Identity.IsAuthenticated == true)
        {
            // delete local authentication cookie
            await HttpContext.SignOutAsync("Identity.Application");

            // raise the logout event
            await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
        }

        // check if we need to trigger sign-out at an upstream identity provider
        if (vm.TriggerExternalSignout)
        {
            // build a return URL so the upstream provider will redirect back
            // to us after the user has logged out. this allows us to then
            // complete our single sign-out processing.
            string url = Url.Action("Logout", new { logoutId = vm.LogoutId });

            // this triggers a redirect to the external provider for sign-out
            return SignOut(new AuthenticationProperties { RedirectUri = url }, vm.ExternalAuthenticationScheme);
        }

        return View("LoggedOut", vm);
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }

    [HttpGet]
    public IActionResult CM_EsqueciASenha()
    {
        return View("EsqueciASenha");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CM_EsqueciASenha(EsqueciASenhaModel p_esqueciASenha)
    {
        if (ModelState.IsValid == false)
            return View(p_esqueciASenha);

        var m_usuario = await _userManager.FindByEmailAsync(p_esqueciASenha.Email);
        if (m_usuario == null)
            return RedirectToAction(nameof(CM_ConfirmacaoEsqueciASenha));

        var m_token = await _userManager.GeneratePasswordResetTokenAsync(m_usuario);
        var m_callback = Url.Action(nameof(CM_ResetarSenha), "Account", new { p_token = m_token, p_email = m_usuario.Email }, Request.Scheme);

        var m_mensagem = new Mensagem(new string[] { m_usuario.Email }, "Token para alterar senha R2 Esperanto", m_callback, null);
        await _envioEmail.CM_PreparacaoEEnvioDeEmailAsync(m_mensagem);

        return RedirectToAction(nameof(CM_ConfirmacaoEsqueciASenha));
    }

    public IActionResult CM_ConfirmacaoEsqueciASenha()
    {
        return View("ConfirmacaoEsqueciASenha");
    }

    [HttpGet]
    public IActionResult CM_ResetarSenha(string p_token, string p_email)
    {
        var m_model = new ResetarSenhaModel { C_Token = p_token, C_Email = p_email };
        return View("ResetarSenha", m_model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CM_ResetarSenha(ResetarSenhaModel p_model)
    {
        if(ModelState.IsValid == false)
            return View(p_model);

        var m_usuario = await _userManager.FindByEmailAsync(p_model.C_Email);
        if(m_usuario == null)
            RedirectToAction(nameof(CM_ResetarSenhaConfirmacao));

        var m_resultado = await _userManager.ResetPasswordAsync(m_usuario, p_model.C_Token, p_model.C_Senha);
        if (m_resultado.Succeeded)
            return RedirectToAction(nameof(CM_ResetarSenhaConfirmacao));

        foreach (var m_erro in m_resultado.Errors)
            ModelState.TryAddModelError(m_erro.Code, m_erro.Description);

        return View("ResetarSenha");
    }

    [HttpGet]
    public IActionResult CM_ResetarSenhaConfirmacao()
    {
        return View("ConfirmacaoResetarSenha");
    }

    [HttpGet]
    public async Task<IActionResult> CM_ConfirmarEmail(string p_token, string p_email)
    {
        var m_usuario = await _userManager.FindByEmailAsync(p_email);
        if (m_usuario == null)
            throw new Exception("Usuário não existe!");

        var m_resultado = await _userManager.ConfirmEmailAsync(m_usuario, p_token);
        return View(m_resultado.Succeeded ? "ConfirmarEmail" : throw new Exception("E-mail não verificado!"));
    }

    [HttpGet]
    public async Task<IActionResult> CM_LoginDoisFatores(string p_nome, bool p_lembrar, string p_urlRetorno = null)
    {
        var m_usuario = await _userManager.FindByNameAsync(p_nome);
        if (m_usuario == null)
            throw new Exception("Usuário não encontrado!");

        var m_proveedor = await _userManager.GetValidTwoFactorProvidersAsync(m_usuario);
        if (m_proveedor.Contains("Email") == false)
            throw new Exception("Não existe o proveedor de token!");

        var m_token = await _userManager.GenerateTwoFactorTokenAsync(m_usuario, "Email");

        var m_mensagem = new Mensagem(new string[] { m_usuario.Email }, "Token de Autenticação", m_token, null);
        await _envioEmail.CM_PreparacaoEEnvioDeEmailAsync(m_mensagem);

        TempData["UrlRetorno"] = p_urlRetorno;
        var m_model = new AutenticacaoDoisFatoresModel
        {
            C_Lembrar = p_lembrar,
            C_UrlRetorno = p_urlRetorno
        };
        return View("LoginTFA", m_model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CM_LoginDoisFatores(AutenticacaoDoisFatoresModel p_model)
    {
        if(ModelState.IsValid == false)
            return View("LoginTFA", p_model);

        var m_usuario = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        if (m_usuario == null)
            throw new Exception("Usuário não encontrado!");

        var m_resultado = await _signInManager.TwoFactorSignInAsync("Email", p_model.C_CodigoDoisFatores, p_model.C_Lembrar, rememberClient: false);
        if (m_resultado.Succeeded)
            return Redirect(p_model.C_UrlRetorno);
        else if (m_resultado.IsLockedOut)
        {
            ModelState.AddModelError("", "O acesso está bloqueado!");
            return View("LoginTFA");
        }
        else
        {
            ModelState.AddModelError("", "Tentativa incorreta de login!");
            return View("LoginTFA");
        }
    }

    /*****************************************/
    /* helper APIs for the AccountController */
    /*****************************************/
    private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
    {
        var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
        if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
        {
            var local = context.IdP == IdentityServerConstants.LocalIdentityProvider;

            // this is meant to short circuit the UI and only trigger the one external IdP
            var vm = new LoginViewModel
            {
                EnableLocalLogin = local,
                ReturnUrl = returnUrl,
                Username = context?.LoginHint,
            };

            if (!local)
            {
                vm.ExternalProviders = new[] { new ExternalProvider { AuthenticationScheme = context.IdP } };
            }

            return vm;
        }

        var schemes = await _schemeProvider.GetAllSchemesAsync();

        var providers = schemes
            .Where(x => x.DisplayName != null)
            .Select(x => new ExternalProvider
            {
                DisplayName = x.DisplayName ?? x.Name,
                AuthenticationScheme = x.Name
            }).ToList();

        var allowLocal = true;
        if (context?.Client.ClientId != null)
        {
            var client = await _clientStore.FindEnabledClientByIdAsync(context.Client.ClientId);
            if (client != null)
            {
                allowLocal = client.EnableLocalLogin;

                if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                {
                    providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                }
            }
        }

        return new LoginViewModel
        {
            AllowRememberLogin = AccountOptions.AllowRememberLogin,
            EnableLocalLogin = allowLocal && AccountOptions.AllowLocalLogin,
            ReturnUrl = returnUrl,
            Username = context?.LoginHint,
            ExternalProviders = providers.ToArray()
        };
    }

    private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginInputModel model)
    {
        var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
        vm.Username = model.Username;
        vm.RememberLogin = model.RememberLogin;
        return vm;
    }

    private async Task<LogoutViewModel> BuildLogoutViewModelAsync(string logoutId)
    {
        var vm = new LogoutViewModel { LogoutId = logoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };

        if (User?.Identity.IsAuthenticated != true)
        {
            // if the user is not authenticated, then just show logged out page
            vm.ShowLogoutPrompt = false;
            return vm;
        }

        var context = await _interaction.GetLogoutContextAsync(logoutId);
        if (context?.ShowSignoutPrompt == false)
        {
            // it's safe to automatically sign-out
            vm.ShowLogoutPrompt = false;
            return vm;
        }

        // show the logout prompt. this prevents attacks where the user
        // is automatically signed out by another malicious web page.
        return vm;
    }

    private async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId)
    {
        // get context information (client name, post logout redirect URI and iframe for federated signout)
        var logout = await _interaction.GetLogoutContextAsync(logoutId);

        var vm = new LoggedOutViewModel
        {
            AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
            PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
            ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
            SignOutIframeUrl = logout?.SignOutIFrameUrl,
            LogoutId = logoutId
        };

        if (User?.Identity.IsAuthenticated == true)
        {
            var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
            if (idp != null && idp != IdentityServerConstants.LocalIdentityProvider)
            {
                var providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
                if (providerSupportsSignout)
                {
                    if (vm.LogoutId == null)
                    {
                        // if there's no current logout context, we need to create one
                        // this captures necessary info from the current logged in user
                        // before we signout and redirect away to the external IdP for signout
                        vm.LogoutId = await _interaction.CreateLogoutContextAsync();
                    }

                    vm.ExternalAuthenticationScheme = idp;
                }
            }
        }

        return vm;
    }
}
