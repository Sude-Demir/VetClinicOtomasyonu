using System.Collections.Generic;

namespace VetClinic.UI1
{
    public static class UserStore
    {
        public static List<User> Users = new List<User>();
    }

    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
