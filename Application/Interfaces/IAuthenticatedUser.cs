namespace WebApi.SharedServices
{
    public interface IAuthenticatedUser
    {
        public string UserId { get; set; }
    }
}