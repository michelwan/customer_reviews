using System;

namespace amazon_customer_reviews_api.controllers.dtos
{
    public class CustomerReviewDto
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int Note { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public DateTime Created { get; set; }
    }
}
