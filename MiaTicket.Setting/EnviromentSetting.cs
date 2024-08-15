using System.Dynamic;

namespace MiaTicket.Setting
{
    public class EnviromentSetting
    {
        private static EnviromentSetting _instance;

        private static object _lockObject = new object();

        private const string CONNECTION_STRING_KEY = "MiaTickConnectionString";
        private const string ISSUER_STRING_KEY = "MiaTickIssuer";
        private const string AUDIENCE_STRING_KEY = "MiaTickAudience";
        private const string SECRET_STRING_KEY = "SecretKey";

        private static string _connectionString = string.Empty;
        private static string _issuer = string.Empty;
        private static string _audience = string.Empty;
        private static string _secret = string.Empty;
        private static string _passwordSalt = string.Empty;

        private EnviromentSetting() { }

        public static EnviromentSetting GetInstance()
        {

            if (_instance == null)
            {
                lock (_lockObject)
                {
                    if(_instance == null)
                    {
                        _instance = new EnviromentSetting();

                        _connectionString = Environment.GetEnvironmentVariable(CONNECTION_STRING_KEY);
                        _issuer = Environment.GetEnvironmentVariable(ISSUER_STRING_KEY);
                        _audience = Environment.GetEnvironmentVariable(AUDIENCE_STRING_KEY);
                        _secret = Environment.GetEnvironmentVariable(SECRET_STRING_KEY);
                    }
                }
            }
            return _instance;
        }

        public string GetConnectionString() => _connectionString;
        public string GetIssuer() => _issuer;
        public string GetAudience() => _audience;
        public string GetSecret() => _secret;
    }


}
