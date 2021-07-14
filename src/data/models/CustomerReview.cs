using System;

namespace amazon_customer_reviews_data.models
{
    public class CustomerReview : Base
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int Note { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
    }
}
