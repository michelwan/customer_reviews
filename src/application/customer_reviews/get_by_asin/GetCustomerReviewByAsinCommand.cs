using amazon_customer_reviews_data.models;
using MediatR;
using System.Collections.Generic;

namespace amazon_customer_reviews_application.customer_reviews.get_by_asin
{
    public class GetCustomerReviewByAsinCommand : IRequest<IList<CustomerReview>>
    {
        public string Asin { get; set; }

        public GetCustomerReviewByAsinCommand(string asin)
        {
            Asin = asin;
        }
    }
}
