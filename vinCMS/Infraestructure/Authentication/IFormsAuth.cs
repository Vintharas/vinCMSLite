using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vinCMS.Infraestructure.Authentication
{
    public interface IFormsAuth
    {
        void SetAuthCookie(string userName, bool createPersistentCookie);
        void SignOut();
    }
}
