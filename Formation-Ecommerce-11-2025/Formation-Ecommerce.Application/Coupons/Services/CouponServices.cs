using AutoMapper;
using Formation_Ecommerce_11_2025.Application.Coupons.Dtos;
using Formation_Ecommerce_11_2025.Application.Coupons.Interfaces;
using Formation_Ecommerce_11_2025.Core.Entities.Coupon;
using Formation_Ecommerce_11_2025.Core.Interfaces.Repositories;

namespace Formation_Ecommerce_11_2025.Application.Coupons.Services
{
    /// <summary>
    /// Service applicatif des coupons : gère les cas d'usage autour des codes promo (création, lecture, mise à jour, suppression).
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Couche Application : expose une API orientée cas d'usage pour la couche Web (Controllers).
    /// - L'accès aux données est délégué à <see cref="ICouponRepository"/> (Infrastructure) afin de respecter le principe de séparation des responsabilités.
    /// - AutoMapper permet de manipuler des DTOs côté UI/Application et des entités côté Core/Infrastructure.
    /// </remarks>
    public class CouponServices : ICouponService
    {
        private readonly ICouponRepository _couponRepository;
        private readonly IMapper _mapper;

        public CouponServices(ICouponRepository couponRepository, IMapper mapper)
        {
            _couponRepository = couponRepository;
            _mapper = mapper;
        }

        public async Task<CouponDto> AddAsync(CouponDto couponDto)
        {
            var coupon = _mapper.Map<Coupon>(couponDto);
            var added = await _couponRepository.AddAsync(coupon);
            return _mapper.Map<CouponDto>(added);
        }

        public async Task<CouponDto?> ReadByIdAsync(Guid couponId)
        {
            var coupon = await _couponRepository.ReadByIdAsync(couponId);
            return coupon == null ? null : _mapper.Map<CouponDto>(coupon);
        }

        public async Task<CouponDto?> GetCouponByCodeAsync(string couponCode)
        {
            var coupon = await _couponRepository.ReadByCouponCodeAsync(couponCode);
            return coupon == null ? null : _mapper.Map<CouponDto>(coupon);
        }

        public async Task<IEnumerable<CouponDto>> ReadAllAsync()
        {
            var list = await _couponRepository.ReadAllAsync();
            return _mapper.Map<IEnumerable<CouponDto>>(list);
        }

        public async Task UpdateAsync(UpdateCouponDto updateCouponDto)
        {
            var existing = await _couponRepository.ReadByIdAsync(updateCouponDto.Id);
            if (existing == null)
            {
                throw new KeyNotFoundException("Coupon Not Found");
            }
            var toUpdate = _mapper.Map<Coupon>(updateCouponDto);
            await _couponRepository.UpdateAsync(toUpdate);
        }

        public async Task DeleteAsync(Guid id)
        {
            var existing = await _couponRepository.ReadByIdAsync(id);
            if (existing == null)
            {
                throw new KeyNotFoundException("Coupon Not Found");
            }
            await _couponRepository.DeleteAsync(id);
        }
    }
}
