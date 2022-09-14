namespace Amazon.Services
{
    public interface IEmailService
    {
        void SendEmail(EmailDTO request);
    }
}
