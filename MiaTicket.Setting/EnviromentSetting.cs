using System.Dynamic;

namespace MiaTicket.Setting
{
    public class EnviromentSetting
    {
        private static EnviromentSetting _instance;

        private static object _lockObject = new object();

        private const string CONNECTION_STRING_KEY = "MiaTickConnectionString";
        private static string _connectionString = string.Empty;

        private EnviromentSetting() { }

        public static EnviromentSetting GetInstance()
        {

            if (_instance == null)
            {
                lock (_lockObject)
                {
                    _instance = new EnviromentSetting();

                    _connectionString = Environment.GetEnvironmentVariable(CONNECTION_STRING_KEY);
                   
                    return _instance;

                }
            }
            return _instance;
        }

        public string GetConnectionString() => _connectionString;

    }


}
