using amazon_customer_reviews_api.controllers.customer_reviews.create;
using amazon_customer_reviews_api.controllers.dtos;
using amazon_customer_reviews_application.customer_reviews.create;
using amazon_customer_reviews_application.customer_reviews.get_by_asin;
using amazon_customer_reviews_application.exceptions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amazon_customer_reviews_api.controllers.customer_reviews
{
    [Route("customer-reviews")]
    public class CustomerReviewsController : AbstractController
    {
        public CustomerReviewsController(IMediator mediator, IMapper mapper) : base(mediator, mapper) { }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<CustomerReviewDto>>> GetByAsinAsync([FromQuery] string asin)
        {
            IList<CustomerReviewDto> result = new List<CustomerReviewDto>();
            try
            {
                var customerReviews = await _mediator.Send(new GetCustomerReviewByAsinCommand(asin));
                if (customerReviews != null && customerReviews.Any())
                    result = customerReviews.Select(x => _mapper.Map<CustomerReviewDto>(x)).ToList();

                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateCustomerReviewResponse>> CreateAsync([FromBody] CreateCustomerReviewRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var customerReview = await _mediator.Send(new CreateCustomerReviewCommand(_mapper.Map<CreateCustomerReviewModel>(request)));
                return Ok(new CreateCustomerReviewResponse
                {
                    CustomerReview = _mapper.Map<CustomerReviewDto>(customerReview)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
