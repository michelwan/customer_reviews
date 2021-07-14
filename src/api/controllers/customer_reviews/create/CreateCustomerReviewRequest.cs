namespace amazon_customer_reviews_api.controllers.customer_reviews.create
{
    public class CreateCustomerReviewRequest
    {
        public string Asin { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int Note { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
    }
}
