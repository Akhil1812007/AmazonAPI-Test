namespace AmazonAPI.Services
{
    public interface IEmailService
    {
        void SendEmail(EmailDTO request);
    }
}
