using RocketseatAuction.API.Contracts;
using RocketseatAuction.API.Entities;
using RocketseatAuction.API.Filters;
using RocketseatAuction.API.Repositories;

namespace RocketseatAuction.API.Services;

public class LoggedUser(IHttpContextAccessor _httpContextAccessor, IUserRepository _repository): ILoggedUser
{
    public User GetUser()
    {
        var token = AuthenticationUserAttribute.GetTokenFromRequest(_httpContextAccessor.HttpContext!);
        var email = AuthenticationUserAttribute.FromBase64String(token);

        return _repository.GetUserByEmail(email);
    }
}
