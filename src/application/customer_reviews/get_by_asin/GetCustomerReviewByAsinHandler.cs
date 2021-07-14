using amazon_customer_reviews_application.exceptions;
using amazon_customer_reviews_data.models;
using amazon_customer_reviews_data.repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace amazon_customer_reviews_application.customer_reviews.get_by_asin
{
    public class GetCustomerReviewByAsinHandler : AbstractHandler, IRequestHandler<GetCustomerReviewByAsinCommand, IList<CustomerReview>>
    {
        private readonly ILogger<GetCustomerReviewByAsinHandler> _logger;

        public GetCustomerReviewByAsinHandler(IGenericRepository genericRepository, ILogger<GetCustomerReviewByAsinHandler> logger) : 
            base(genericRepository)
        {
            _logger = logger;
        }

        public async Task<IList<CustomerReview>> Handle(GetCustomerReviewByAsinCommand request, CancellationToken cancellationToken)
        {
            var product = await _genericRepository.FindAsync<Product>(x => x.Asin == request.Asin);
            if (product == null)
            {
                _logger.LogWarning($"Research on a non-existing product (asin:{request.Asin})");
                throw new NotFoundException("Product does not exist");
            }

            return await _genericRepository.GetAsync<CustomerReview>(x => x.ProductId == product.Id);
        }
    }
}
