namespace SACLA_App.Core
{
    public class Constants
    {
        public static class Roles
        {
            public const string Admin = "Admin";
            public const string Author = "Author";
        }

        public static class Policies
        {
            public const string RequiresAdmin = "RequireAdmin";
            public const string RequiresAuthor = "RequireAuthor";
        }
    }
}
