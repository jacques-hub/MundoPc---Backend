namespace Project.Infraestructure.Connection
{
    public static class StringConnection
    {
        private const string Server = @"HPJACQUES\SQLEXPRESS";
        //private const string Server = @"DESKTOP-0VUTJNI";
        private const string DataBase = "MundoPc10";
        private const string User = "juarezpj_SQLLogin_1";
        private const string Password = "6l1lfn46re";

        public static string GetConnectionStringSql => "workstation id=mundopcdb.mssql.somee.com;packet size = 4096; user id = juarezpj_SQLLogin_1; pwd=6l1lfn46re;data source = mundopcdb.mssql.somee.com; persist security info=False;initial catalog = mundopcdb";
        public static string GetConnectionStringWin => $"Data Source={Server}; " +
                                                       $"Initial Catalog={DataBase}; " +
                                                       $"Integrated Security = true";
    }
}
