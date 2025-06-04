using userJwtApp.Controllers;
using userJwtApp.Exceptions;
using userJwtApp.Models.ProductModel;
using userJwtApp.Services.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace userJwtApp.Endpoints;

public static class ProductEndpoints
{
    public static RouteGroupBuilder MapClientEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/",
            async (HttpContext context,
            [FromServices] ProductController controller) =>
            {
                int userClaimId = int.Parse(context.User.Claims
                    .First(claim => claim.Type == JwtConsts.CLAIM_ID).Value);

                IReadOnlyList<ProductReadModel> userProducts;
                try
                {
                    userProducts = await controller.GetUserProducts(userClaimId);
                }
                catch (UserNotFoundException ex)
                {
                    return Results.BadRequest(ex.Message)
                }

                return Results.Ok(userProducts);
            }
        );
        group.MapPost("/",
         async (HttpContext context,
            [FromServices] ProductController controller,
            [FromBody] ProductRegisterRequestModel registerRequest) =>
            {
                int userClaimId = int.Parse(context.User.Claims
                    .First(claim => claim.Type == JwtConsts.CLAIM_ID).Value);

                int newProductId;
                try
                {
                    newProductId = await controller.RegisterProduct(registerRequest, userClaimId);
                }
                catch (Exception e) when (e is InvalidRequestInfoException or UserNotFoundException)
                {
                    return Results.BadRequest(e.Message);
                }

                return Results.Created($"client/{newProductId}", newProductId);
            }
        );
        return group;
    }
}