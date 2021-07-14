using amazon_customer_reviews_data.models;
using amazon_customer_reviews_data.repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace amazon_customer_reviews_application.customer_reviews.create
{
    public class CreateCustomerReviewHandler : AbstractHandler, IRequestHandler<CreateCustomerReviewCommand, CustomerReview>
    {
        private readonly ILogger<CreateCustomerReviewHandler> _logger;

        public CreateCustomerReviewHandler(IGenericRepository genericRepository, ILogger<CreateCustomerReviewHandler> logger) : base(genericRepository)
        {
            _logger = logger;
        }

        public async Task<CustomerReview> Handle(CreateCustomerReviewCommand request, CancellationToken cancellationToken)
        {
            var product = await _genericRepository.FindAsync<Product>(x => x.Asin == request.CreateModel.Asin);
            if (product == null)
            {
                product = await _genericRepository.InsertAsync(new Product
                {
                    Asin = request.CreateModel.Asin
                });
            }

            var customerReview = new CustomerReview
            {
                Comment = request.CreateModel.Comment,
                Note = request.CreateModel.Note,
                ProductId = product.Id,
                Title = request.CreateModel.Title,
                UserId = request.CreateModel.UserId,
                UserName = request.CreateModel.UserName,
            };
            try
            {
                return await _genericRepository.InsertAsync(customerReview);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to insert customer review");
                return null;
            }
        }
    }
}
