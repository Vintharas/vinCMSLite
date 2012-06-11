using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vinCMS.Infraestructure.Authentication
{
    public interface IMembership
    {
        bool ValidateUser(string username, string password);
        int MaxInvalidPasswordAttempts { get; }
    }
}
