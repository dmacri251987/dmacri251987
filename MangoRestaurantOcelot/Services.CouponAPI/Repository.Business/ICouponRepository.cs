using Services.CouponAPI.Models.Dto;

namespace Services.CouponAPI.Repository.Business
{
    public interface ICouponRepository
    {
        Task<CouponDto>GetCouponByCode(string couponCode);
    }
}
