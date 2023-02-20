using GeekShopping.Cart.Api.BaseRepository;
using GeekShopping.Cart.Api.Models.Dto;
using GeekShopping.Cart.Api.Models.Entities;
using CartEntity = GeekShopping.Cart.Api.Models.Entities.Cart;

namespace GeekShopping.Cart.Api.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IUserService _userService;

        public CartService(ICartRepository cartRepository, ICartItemRepository cartItemRepository, IUserService userService)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _userService = userService;
        }

        public async Task AddItemAsync(AddCartItemDto addCartItemDto)
        {
            var userId = _userService.GetLoggedUserId();
            var userCart = await _cartRepository.GetByUserIdAsync(userId);
            
            if (userCart == null)
            {
                await _cartRepository.SaveAsync(new CartEntity
                {
                    UserId = userId,
                    Items = new List<CartItem>()
                    {
                        new CartItem
                        {
                            ProductId = addCartItemDto.Product.Id,
                            Name = addCartItemDto.Product.Name,
                            Description = addCartItemDto.Product.Description,
                            Price = addCartItemDto.Product.Price,
                            Quantity = addCartItemDto.Quantity,
                        },
                    },
                });
            }
            else
            {
                var item = userCart.Items.FirstOrDefault(i => i.ProductId == addCartItemDto.Product.Id);
                var isItemOnCart = item != null;

                if (isItemOnCart)
                {
                    item.Quantity += addCartItemDto.Quantity;
                    await _cartItemRepository.UpdateAsync(item);
                }
                else
                {
                    var cartItem = new CartItem
                    {
                        ProductId = addCartItemDto.Product.Id,
                        Name = addCartItemDto.Product.Name,
                        Description = addCartItemDto.Product.Description,
                        Price = addCartItemDto.Product.Price,
                        Quantity = addCartItemDto.Quantity,
                        CartId = userCart.Id,
                    };
                    await _cartItemRepository.SaveAsync(cartItem);
                }
            }
        }

        public async Task<GetCartDto> GetByUserIdAsync(Guid userId)
        {
            var userCart = await _cartRepository.GetByUserIdAsync(userId);

            if (userCart == null)
                return null;

            return new GetCartDto
            {
                Items = userCart.Items.Select(cartItem => new GetCartDto.ItemDto
                {
                    Id = cartItem.Id,
                    Name = cartItem.Name,
                    Description = cartItem.Description,
                    Price = cartItem.Price,
                    Quantity = cartItem.Quantity,
                }),
                Coupon = userCart.Coupon,
            };
        }

        public async Task RemoveItemAsync(Guid id)
        {
            var cartItem = await _cartItemRepository.GetByIdAsync(id);

            if (cartItem == null)
                return;

            cartItem.Quantity -= 1;

            var shouldDelete = cartItem.Quantity == 0;

            if (shouldDelete)
                await _cartItemRepository.DeleteAsync(cartItem);
            else
                await _cartItemRepository.UpdateAsync(cartItem);
        }

        public async Task ClearAsync(Guid userId)
        {
            var userCart = await _cartRepository.GetByUserIdAsync(userId);

            if (userCart == null)
                return;

            await _cartItemRepository.DeleteByCartIdAsync(userCart.Id);
        }

        public async Task ApplyCouponAsync(Guid userId, string coupon)
        {
            var userCart = await _cartRepository.GetByUserIdAsync(userId);

            if (userCart == null)
                return;

            userCart.Coupon = coupon;

            await _cartRepository.UpdateAsync(userCart);
        }

        public async Task RemoveCouponAsync(Guid userId)
        {
            var userCart = await _cartRepository.GetByUserIdAsync(userId);

            if (userCart == null)
                return;

            userCart.Coupon = null;

            await _cartRepository.UpdateAsync(userCart);
        }
    }
}
