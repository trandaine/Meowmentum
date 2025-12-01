namespace EnglishApp.Infrastructure.Constants
{
    public static class ConnectionConstants
    {
        public const string KEY_CART = "Cart";
        //public const string SQL_SERVER = "., 1433;";
        public const string SQL_SERVER_CONNECTION_STRING = "Server= host.docker.internal, 1433; Database=EnglishAppDb; User Id=sa; password=Dai@2018; TrustServerCertificate=True; Trusted_Connection=False; MultipleActiveResultSets=true;";
        //public const string SQL_SERVER_CONNECTION_STRING = "Server= 172.27.0.22, 1433; Database=EnglishAppDb; User Id=sa; password=Dai@2018; TrustServerCertificate=True; Trusted_Connection=False; MultipleActiveResultSets=true;";
        public const string SQL_SERVER_IDENTITY_CONNECTION_STRING = "Server= host.docker.internal, 1433; Database=EnglishAppIdentityDb; User Id=sa; password=Dai@2018; TrustServerCertificate=True; Trusted_Connection=False; MultipleActiveResultSets=true;";
        //public const string SQL_SERVER_IDENTITY_CONNECTION_STRING = "Server= 172.27.0.22, 1433; Database=EnglishAppIdentityDb; User Id=sa; password=Dai@2018; TrustServerCertificate=True; Trusted_Connection=False; MultipleActiveResultSets=true;";
    }
}
