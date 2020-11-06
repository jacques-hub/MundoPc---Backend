namespace Project.Infraestructure.Connection
{
    public static class StringConnection
    {
        private const string Server = @"HPJACQUES\SQLEXPRESS";
        //private const string Server = @"DESKTOP-0VUTJNI";
        private const string DataBase = "MundoPc10";
        private const string User = "sa";
        private const string Password = "40697698";

        public static string GetConnectionStringSql => $"Data Source={Server}; " +
                                                       $"Initial Catalog={DataBase}; " +
                                                       $"User Id={User}; " +
                                                       $"Password={Password};";
        public static string GetConnectionStringWin => $"Data Source={Server}; " +
                                                       $"Initial Catalog={DataBase}; " +
                                                       $"Integrated Security = true";
    }
}
