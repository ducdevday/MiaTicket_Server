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
        private const string REDIS_CONNECTION_STRING_KEY = "RedisConnectionString";
        private const string RABBITMQ_CONNECTION_STRING_KEY = "RabbitMQConnectionString";
        private const string RABBITMQ_USERNAME_KEY = "RabbitMQUserName";
        private const string RABBITMQ_PASSWORD_KEY = "RabbitMQPassword";
        private const string VNPAY_TMNCODE = "VNPay_TMNCODE";
        private const string VNPAY_HASHSECRET = "VNPay_HashSecret";
        private const string ZALOPAY_KEY1 = "ZaloPay_Key1";
        private const string ZALOPAY_KEY2 = "ZaloPay_Key2";

        private static string _connectionString = string.Empty;
        private static string _issuer = string.Empty;
        private static string _audience = string.Empty;
        private static string _secret = string.Empty;
        private static string _couldinaryUrl = string.Empty;
        private static string _smtpUrl = string.Empty;
        private static int _smtpPort = 587;
        private static string _smtpEmail = string.Empty;
        private static string _smtpAppPassword = string.Empty;
        private static string _redisConnectionString = string.Empty;
        private static string _vnPayTmnCode = string.Empty;
        private static string _vnPayHashsecret = string.Empty;
        private static string _zaloPayKey1 = string.Empty;
        private static string _zaloPayKey2 = string.Empty;
        private static string _rabbitMQConnectionString = string.Empty;
        private static string _rabbitMQUserName = string.Empty;
        private static string _rabbitMQPassword = string.Empty;

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
                        _redisConnectionString = Environment.GetEnvironmentVariable(REDIS_CONNECTION_STRING_KEY);
                        _vnPayTmnCode = Environment.GetEnvironmentVariable(VNPAY_TMNCODE);
                        _vnPayHashsecret = Environment.GetEnvironmentVariable(VNPAY_HASHSECRET);
                        _zaloPayKey1 = Environment.GetEnvironmentVariable(ZALOPAY_KEY1);
                        _zaloPayKey2 = Environment.GetEnvironmentVariable(ZALOPAY_KEY2);
                        _rabbitMQConnectionString = Environment.GetEnvironmentVariable(RABBITMQ_CONNECTION_STRING_KEY);
                        _rabbitMQUserName = Environment.GetEnvironmentVariable(RABBITMQ_USERNAME_KEY);
                        _rabbitMQPassword = Environment.GetEnvironmentVariable(RABBITMQ_PASSWORD_KEY);
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
        public string GetRedisConnectionString() => _redisConnectionString;
        public string GetVnPayTmnCode() => _vnPayTmnCode;
        public string GetVnPayHashSecret() => _vnPayHashsecret;
        public string GetZaloPayKey1() => _zaloPayKey1;
        public string GetZaloPayKey2() => _zaloPayKey2;
        public string GetRabbitMQConnectionString() => _rabbitMQConnectionString;
        public string GetRabbitMQUserName() => _rabbitMQUserName;
        public string GetRabbitMQPassword() => _rabbitMQPassword;
    }
}
