namespace MiaTicket.Setting
{
    public static class AppConstant
    {
        public const int ACCESS_TOKEN_EXPIRE_IN_MINUTES = 10;
        public const int REFRESH_TOKEN_EXPIRE_IN_DAYS = 10;
        public const int VERIFICATION_CODE_EXPIRE_IN_MINUTES = 30;
        public const int VERIFICATION_CODE_LENGHT = 32;
        public const int PAYMENT_LINK_EXPIRE_IN_MINUTES = 15;
        public const string EMAIL_VERIFY_FINISH_PATH = "http://localhost:4200/email-verify-finish";
        public const string RESET_PASSWORD_PATH = "http://localhost:4200/reset-password";
        public const string EVENT_COUNT_KEYWORD = "EventCount";
    }
}
