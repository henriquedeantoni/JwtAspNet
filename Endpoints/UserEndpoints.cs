using userJwtApp.Controllers;
using userJwtApp.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace userJwtApp.Endpoints;
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
        group.MapPost("login", async ([FromServices] UserController userController, [FromBody] UserLoginRequestModel userLoginRequestModel) =>
        {
            string userJwt;
            try
            {
                userJwt = await userController.LoginUser(userLoginRequestModel);
            }
            catch (UserNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
            catch (Exception ex) when (ex is InvalidRequestInfoException or WrongUserPasswordException)
            {
                return Results.BadRequest(ex.Message);
            }
            return Results.Ok(userJwt);
        });

        return group;
    }
}