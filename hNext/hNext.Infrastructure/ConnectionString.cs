namespace hNext.Infrastructure
{
    public static class ConnString
    {
        private static string localDb = "hNextDbConnectionString";
        private static string remoteHomeDb = "remoteDbConnectionString";

        public static string ConnectionString => localDb;
    }
}