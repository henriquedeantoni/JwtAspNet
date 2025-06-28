using userJwtApp.Controllers;
using userJwtApp.Exceptions;
using userJwtApp.Models.ProductModel;
using userJwtApp.Services.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace userJwtApp.Endpoints;

public static class ProductEndpoints
{
    public static RouteGroupBuilder MapProductEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/",
            async (HttpContext context,
            [FromServices] ProductController controller) =>
            {
                Guid userClaimId = Guid.Parse(context.User.Claims
                    .First(claim => claim.Type == JwtConsts.CLAIM_ID).Value);

                IReadOnlyList<ProductReadModel> userProducts;
                try
                {
                    userProducts = await controller.GetUserProducts(userClaimId);
                }
                catch (UserNotFoundException ex)
                {
                    return Results.BadRequest(ex.Message);
                }

                return Results.Ok(userProducts);
            }
        );

        group.MapGet("/",
        async (HttpContext context,
            [FromQuery] DateTime date, 
            [FromServices] ProductController controller) =>
            {
                IReadOnlyList<ProductReadModel> productsListDate;
                try
                {
                    productsListDate
                }
                catch (Exception e)
                {

                }
            }
        );

        group.MapPost("/",
         async (HttpContext context,
            [FromServices] ProductController controller,
            [FromBody] ProductRegisterRequestModel registerRequest) =>
            {
                Guid userClaimId = Guid.Parse(context.User.Claims
                    .First(claim => claim.Type == JwtConsts.CLAIM_ID).Value);

                Guid newProductId;
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

        group.MapPut("{id:Guid}", async (HttpContext context,
            [FromRoute] Guid id,
            [FromServices] ProductController productController,
            [FromBody] ProductUpdateRequestModel updateRequest) =>
        {

            Guid updatedProductId;
            try
            {
                updatedProductId = await productController.UpdateProduct(updateRequest, id);
            }
            catch (InvalidRequestInfoException ex)
            {
                return Results.BadRequest(ex.Message);
            }
            catch (ProductNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
            return Results.Ok(updatedProductId);
        });

        group.MapDelete("{id:Guid}", async (HttpContext context,
            [FromRoute] Guid id,
            [FromServices] ProductController productController) =>
        {
            Guid deletedProductId;
            try
            {
                deletedProductId = await productController.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

            return Results.Ok(deletedProductId);
        });

        return group;
    }
}