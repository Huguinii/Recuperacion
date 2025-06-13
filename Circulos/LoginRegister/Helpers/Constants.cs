using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LoginRegister.Helpers
{
    public static class Constants
    {
        public const string JSON_FILTER = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";

        public const string BASE_URL = "http://localhost:1433/api/";
        public const string DICATADOR_URL = "TiendaDB";
        public const string LOGIN_PATH = "/api/users/login";
        public const string REGISTER_PATH = "/api/users/register";
        public const string PATH_IMAGE_NOT_FOUND = "/Resources/Not_found.png";

    }
}
