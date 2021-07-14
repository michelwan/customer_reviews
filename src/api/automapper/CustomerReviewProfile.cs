using amazon_customer_reviews_api.controllers.customer_reviews.create;
using amazon_customer_reviews_api.controllers.dtos;
using amazon_customer_reviews_application.customer_reviews.create;
using amazon_customer_reviews_data.models;
using AutoMapper;

namespace amazon_customer_reviews_api.automapper
{
    public class CustomerReviewProfile : Profile
    {
        public CustomerReviewProfile()
        {
            CreateMap<CreateCustomerReviewRequest, CreateCustomerReviewModel>();
            CreateMap<CustomerReview, CustomerReviewDto>();
        }
    }
}
