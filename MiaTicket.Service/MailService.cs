namespace MiaTicket.Service
{
    public class MailService
    {
        private static MailService _instance;
        private static object _lockObject = new object();
        private MailService() { }

        public static MailService GetInstance() {
            if (_instance == null) {
                lock (_lockObject) {
                    _instance = new MailService();

                }
            }
            return _instance;
        
        }

        public void sendMail
    }
}
