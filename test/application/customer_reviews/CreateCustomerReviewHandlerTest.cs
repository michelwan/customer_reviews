using amazon_customer_reviews_application.customer_reviews.create;
using amazon_customer_reviews_data.models;
using amazon_customer_reviews_data.repositories;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace amazon_customer_reviews_test.customer_reviews
{
    public class CreateCustomerReviewHandlerTest
    {
        [Fact]
        public async Task CreateCustomerReviewTest()
        {
            var product = new Product
            {
                Id = 1,
                Asin = "B082XY23D5"
            };
            var customerReview = new CustomerReview
            {
                ProductId = product.Id,
                Note = 1,
                Id = 1,
                Comment = "qfjqskfj",
                UserName = "qfsqsd",
                UserId = "qsdqsdqsf",
                Title = "qsdqsd",
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };

            var repository = new Mock<IGenericRepository>();
            repository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .Returns(Task.FromResult(product));
            repository.Setup(x => x.InsertAsync(It.IsAny<CustomerReview>()))
                .Returns(Task.FromResult(customerReview));

            var logger = new Mock<ILogger<CreateCustomerReviewHandler>>();
            var handler = new CreateCustomerReviewHandler(repository.Object, logger.Object);
            var actual = await handler.Handle(new CreateCustomerReviewCommand(new CreateCustomerReviewModel
            {
                Asin = product.Asin,
                Comment = customerReview.Comment,
                Note = customerReview.Note,
                Title = customerReview.Title,
                UserId = customerReview.UserId,
                UserName = customerReview.UserName
            }), new System.Threading.CancellationToken());

            Assert.NotNull(actual);
            Assert.Equal(customerReview.Id, actual.Id);
            Assert.Equal(customerReview.Comment, actual.Comment);
            Assert.Equal(customerReview.Note, actual.Note);
            Assert.Equal(customerReview.Note, actual.Note);
            Assert.Equal(customerReview.UserId, actual.UserId);
            Assert.Equal(customerReview.UserName, actual.UserName);
            Assert.Equal(customerReview.Created, actual.Created);
            Assert.Equal(customerReview.Updated, actual.Updated);
        }
    }
}
