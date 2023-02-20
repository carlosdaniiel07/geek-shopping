namespace GeekShopping.Cart.Api.Services
{
    public class UserService : IUserService
    {
        public Guid GetLoggedUserId() =>
            Guid.Parse("5e5d3838-5e49-4547-a22b-851d19910499");
    }
}
