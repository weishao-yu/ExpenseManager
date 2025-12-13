namespace ExpenseManager
{
    public static class Session
    {
        public static int UserId { get; set; } = -1;
        public static string Username { get; set; } = "";

        public static bool IsLoggedIn => UserId > 0;

        public static void Logout()
        {
            UserId = -1;
            Username = "";
        }
    }
}
