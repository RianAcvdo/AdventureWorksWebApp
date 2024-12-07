using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorksWebApp.Utils
{
    public class Constants
    {
        public static readonly int MAXIMUM_ALLOWED_FILE_UPLOAD = 4_194_304; // 4MB

        public static readonly string[] ALLOWED_EXTENSIONS = new[] { ".jpg" };
    }
}
