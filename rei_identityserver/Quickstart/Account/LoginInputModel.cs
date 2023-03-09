// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace rei_identityserver.Quickstart.Account
{
    public class LoginInputModel
    {
        [Required(ErrorMessage = "O campo de usuário é obrigatório.")]
        [DisplayName("Usuário")]
        public string Username { get; set; }
        [Required(ErrorMessage = "O campo de senha é obrigatório.")]
        [DisplayName("Senha")]
        public string Password { get; set; }
        [DisplayName("Lembrar senha")]
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }
    }
}