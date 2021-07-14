using amazon_customer_reviews_data.repositories;

namespace amazon_customer_reviews_application
{
    public abstract class AbstractHandler
    {
        protected readonly IGenericRepository _genericRepository;

        protected AbstractHandler(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
    }
}
