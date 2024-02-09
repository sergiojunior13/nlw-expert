using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RocketseatAuction.API.Repositories;

namespace RocketseatAuction.API.Filters;

public class AuthenticationUserAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        try
        {
            var repository = new RocketseatAuctionDbContext();

            var token = GetTokenFromRequest(context.HttpContext);
            var reqEmail = FromBase64String(token);

            var userExists = repository.Users.Any(user => user.Email.Equals(reqEmail));

            if (!userExists)
            {
                context.Result = new UnauthorizedObjectResult("E-mail not valid.");
            }
        }
        catch (Exception ex)
        {
            context.Result = new UnauthorizedObjectResult(ex);
        }
    }

    public static string GetTokenFromRequest(HttpContext context)
    {
        var authentication = context.Request.Headers.Authorization.ToString();

        if (string.IsNullOrEmpty(authentication))
        {
            throw new Exception("Token is missing.");
        }

        var token = authentication["Bearer ".Length..];

        return token;
    }

    public static string FromBase64String(string base64)
    {
        var data = Convert.FromBase64String(base64);

        return System.Text.Encoding.UTF8.GetString(data);
    }
}
