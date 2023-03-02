using GeekShopping.Payment.Subscriber.Models.Context;
using PaymentEntity = GeekShopping.Payment.Subscriber.Models.Entities.Payment;

namespace GeekShopping.Payment.Subscriber.Repository
{
    public class PaymentRepository : BaseRepository<PaymentEntity>, IPaymentRepository
    {
        public PaymentRepository(MySqlContext context) : base(context)
        {

        }
    }
}
