namespace GeekShopping.Order.Api.Models.Enums
{
    public enum OrderStatus : byte
    {
        WaitingPayment = 1,
        Paid = 2,
        PreparingShipping = 3,
        Shipped = 4,
        Delivered = 5,
        Cancelled = 6,
    }
}
