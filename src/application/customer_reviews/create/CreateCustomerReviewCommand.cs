using amazon_customer_reviews_data.models;
using MediatR;

namespace amazon_customer_reviews_application.customer_reviews.create
{
    public class CreateCustomerReviewCommand : IRequest<CustomerReview>
    {
        public CreateCustomerReviewModel CreateModel { get; set; }

        public CreateCustomerReviewCommand(CreateCustomerReviewModel createModel)
        {
            CreateModel = createModel;
        }
    }
}
