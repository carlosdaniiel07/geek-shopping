using GeekShopping.Payment.Subscriber.Models.Context;
using Microsoft.EntityFrameworkCore;
using PaymentEntity = GeekShopping.Payment.Subscriber.Models.Entities.Payment;

namespace GeekShopping.Payment.Subscriber.Repository
{
    public class PaymentRepository : BaseRepository<PaymentEntity>, IPaymentRepository
    {
        public PaymentRepository(MySqlContext context) : base(context)
        {

        }

        public async Task<PaymentEntity> GetByCheckoutExternalIdAsync(string checkoutExternalId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(payment => payment.CheckoutExternalId== checkoutExternalId);
        }
    }
}
