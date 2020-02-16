using Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly string _validUserName, _validPassword;

        public AuthorizationService(string validUserName, string validPassword)
        {
            _validUserName = validUserName;
            _validPassword = validPassword;
        }

        public bool IsValid(string userName, string password)
        {
            return userName.Equals(_validUserName) && password.Equals(_validPassword);
        }
    }
}
