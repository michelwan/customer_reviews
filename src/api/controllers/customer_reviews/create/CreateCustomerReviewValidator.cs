using FluentValidation;

namespace amazon_customer_reviews_api.controllers.customer_reviews.create
{
    public class CreateCustomerReviewValidator : AbstractValidator<CreateCustomerReviewRequest>
    {
        public CreateCustomerReviewValidator()
        {
            RuleFor(r => r.Asin).NotEmpty().WithMessage("Asin cannot be empty");
            RuleFor(r => r.UserId).NotEmpty().WithMessage("UserId cannot be empty");
        }
    }
}
