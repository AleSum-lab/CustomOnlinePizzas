using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IAuthorizationService
    {
        bool IsValid(string userName, string password);

    }
}
