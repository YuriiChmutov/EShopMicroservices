using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService 
    (DiscountContext dbContext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
            .Coupons
            .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        if (coupon is null)
            coupon = new Models.Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount desc" };

        logger.LogInformation($"Discount is retrieved for ProductName: {coupon.ProductName}, Amount : {coupon.Amount}");

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));
        }

        dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation($"Discount is succesfully created. Product name: {coupon.ProductName}, amount : {coupon.Amount}");

        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));
        }

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation($"Discount is succesfully updated. Product name: {coupon.ProductName}, amount : {coupon.Amount}");

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
            .Coupons
            .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, "Product not found"));

        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation($"Discount for {request.ProductName} is succesfully deleted.");

        return new DeleteDiscountResponse { Success = true };
    }
}