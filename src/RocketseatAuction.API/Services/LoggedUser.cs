using RocketseatAuction.API.Entities;
using RocketseatAuction.API.Filters;
using RocketseatAuction.API.Repositories;

namespace RocketseatAuction.API.Services;

public class LoggedUser(IHttpContextAccessor httpContext)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContext;

    public User GetUser()
    {
        var repository = new RocketseatAuctionDbContext();

        var token = AuthenticationUserAttribute.GetTokenFromRequest(_httpContextAccessor.HttpContext!);
        var email = AuthenticationUserAttribute.FromBase64String(token);

        return repository.Users.First(user => user.Email.Equals(email));
    }
}
