using MiaTicket.Email.Model;
using System.Net.Mail;
using System.Net;
using MiaTicket.Setting;

namespace MiaTicket.Email
{
    public class EmailService
    {
        private static EmailService _instance;
        private List<IEmailModel> _queue;
        private bool _isInProgress = false;
        private static object _locker = new object();
        private EnviromentSetting _setting = EnviromentSetting.GetInstance();
        private EmailService() { }
        public static EmailService GetInstance() {
            if (_instance == null) {
                lock (_locker)
                {
                    if (_instance == null)
                    {
                        _instance = new EmailService();
                        _instance._queue = new List<IEmailModel>();
                    }
                    return _instance;
                }
            }
            return _instance;
        }

        public Task Push(IEmailModel model)
        {
            _queue.Add(model);
            if (!_isInProgress)
            {
                _isInProgress = true;
                Task.Run(SendMail);
            }
            return Task.CompletedTask;
        }

        private Task SendMail()
        {
            while(_queue.Count > 0)
            {
                var emailModel = _queue.First();
                try
                {
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient(_setting.GetSMTPURL());

                    message.From = new MailAddress(emailModel.Sender);
                    message.To.Add(new MailAddress(emailModel.Receiver));
                    message.Subject = emailModel.Subject;
                    message.Body = emailModel.Body;
                    message.IsBodyHtml = true;

                    smtp.Port = _setting.GetSMTPPort();
                    smtp.Credentials = new NetworkCredential(_setting.GetSMTPEmail(), _setting.GetSMTPAppPassword());
                    smtp.EnableSsl = true;

                    smtp.Send(message);
                    _queue.Remove(emailModel);
                }
                catch
                {
                    _queue.Remove(emailModel);
                }
            }
            _isInProgress = false;
            return Task.CompletedTask;
        }
    }
}
