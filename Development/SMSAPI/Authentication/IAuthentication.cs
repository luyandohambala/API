namespace SMSAPI.Authentication
{
    public interface IAuthentication
    {
        bool IsAuthenticated(string ApiKey);
    }
}
