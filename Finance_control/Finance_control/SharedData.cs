using Finance_control.Models;

namespace Finance_control
{
    public class SharedData
    {
        public static List<User> Users { get; } = new List<User>
        {
            new User(){ Login = "user", Password = "user" },
            new User(){ Login = "admin", Password = "admin" },
        };
    }
}
