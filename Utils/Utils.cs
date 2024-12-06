using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Bcrypt = BCrypt.Net.BCrypt;

namespace AdventureWorksWebApp.Utils
{
    internal class Utils
    {
        public static bool CheckIfPasswordMatches(string rawPassword, string hashedPassword)
        {
            return !string.IsNullOrEmpty(rawPassword) && Bcrypt.Verify(rawPassword, hashedPassword);
        }

        public static string GetHashedPassword(string rawPassword)
        {
            return Bcrypt.HashPassword(rawPassword);
        }
    }
}