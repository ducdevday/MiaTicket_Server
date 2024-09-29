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
        private const string CLOUDINARY_URL = "CloudinaryUrl";
        private const string SMTP_URL = "SmtpServer";
        private const string SMTP_PORT = "SmtpPort";
        private const string SMTP_EMAIL = "SmtpEmail";
        private const string SMTP_APP_PASSWORD = "SmtpAppPassword";
        private const string VNPAY_SECRET_KEY = "VnPaySecretKey";
        private const string VNPAY_TMN_CODE = "VNPayTMNCode";
        private const string VNPAY_RETURN_URL = "VNPayReturnUrl";

        private static string _connectionString = string.Empty;
        private static string _issuer = string.Empty;
        private static string _audience = string.Empty;
        private static string _secret = string.Empty;
        private static string _couldinaryUrl = string.Empty;
        private static string _smtpUrl = string.Empty;
        private static int _smtpPort = 587;
        private static string _smtpEmail = string.Empty;
        private static string _smtpAppPassword = string.Empty;
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
                        _couldinaryUrl = Environment.GetEnvironmentVariable(CLOUDINARY_URL);
                        _smtpUrl = Environment.GetEnvironmentVariable(SMTP_URL);
                        int.TryParse(Environment.GetEnvironmentVariable(SMTP_PORT), out _smtpPort);
                        _smtpEmail = Environment.GetEnvironmentVariable(SMTP_EMAIL);
                        _smtpAppPassword = Environment.GetEnvironmentVariable(SMTP_APP_PASSWORD);
                    }
                }
            }
            return _instance;
        }

        public string GetConnectionString() => _connectionString;
        public string GetIssuer() => _issuer;
        public string GetAudience() => _audience;
        public string GetSecret() => _secret;
        public string GetCouldinaryUrl() => _couldinaryUrl;
        public string GetSMTPURL() => _smtpUrl;
        public int GetSMTPPort() => _smtpPort;
        public string GetSMTPEmail() => _smtpEmail;
        public string GetSMTPAppPassword() => _smtpAppPassword;
    }
}
