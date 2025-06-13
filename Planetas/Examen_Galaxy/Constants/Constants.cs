using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_Galaxy.Constants
{
    internal class Constants
    {
        internal const string BASE_URL = "https://localhost:7041/";
        internal const string PLANET_RESOURCE = "Planeta";
        internal const string RESOURCES_PATH = "/Resources/";
        internal const string IMAGES_EXTENSION = ".jpg";
        internal const string PATH_IMAGE_NOT_FOUND = "Not_found.png"; 
        public const string JSON_FILTER = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
        internal static List<string> PLANETAS_POSIBLES = new List<string>()
        {
            "Planet_1.jpg",
            "Planet_2.jpg",
            "Planet_3.jpg",
            "Planet_4.jpg",
            "Planet_5.jpg",
            "Planet_6.jpg",
            "Planet_7.jpg",
            "Planet_8.jpg",
            "Planet_9.jpg",
            "Planet_10.jpg",
            "Planet_11.jpg",
         
        };

    }
}
