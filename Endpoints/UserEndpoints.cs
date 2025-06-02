using userJwtApp.Controllers;
using userJwtApp.Exceptions;
using Microsoft.AspNetCore.Mvc;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("Signin",
            async ([FromServices] UserController controller,
            [FromBody] UserSignRequestModel signRequest) =>
            {
                string newUserJwt;

                try
                {
                    newUserJwt = await controller.SignUser(signRequest);
                }
                catch (InvalidRequestInfoException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (UserAllreadyRegisteredException ex)
                {
                    return Results.Conflict(ex.Message);
                }

                return Results.Created("user/signin", newUserJwt);
            }
        );

        return group;
    }
}