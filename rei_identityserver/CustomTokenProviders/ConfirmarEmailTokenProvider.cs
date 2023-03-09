using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;

namespace rei_identityserver.CustomTokenProviders;

public class ConfirmarEmailTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
{
    public ConfirmarEmailTokenProvider(
        IDataProtectionProvider p_dataProtectionProvider,
        IOptions<ConfirmarEmailTokenProviderOptions> p_options,
        ILogger<DataProtectorTokenProvider<TUser>> p_logger)
        : base (p_dataProtectionProvider, p_options, p_logger) { }
}

public class ConfirmarEmailTokenProviderOptions : DataProtectionTokenProviderOptions { }
