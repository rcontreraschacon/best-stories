using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BestStories;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v0/[controller]")]
    [ApiController]
    public class BestStoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BestStoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoryDto>>> Get()
        {
            return await _mediator.Send(new List.Query());
        }
    }
}
