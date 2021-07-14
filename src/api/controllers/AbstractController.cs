using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace amazon_customer_reviews_api.controllers
{
    [ApiController]
    public abstract class AbstractController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly IMapper _mapper;

        public AbstractController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
    }
}
