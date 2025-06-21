namespace CleanArchMvc.WebApi.DTOs
{
    public class UserToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
