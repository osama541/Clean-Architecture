﻿using Application.Features.Product.Commands;
using Application.Features.Product.Quries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _mediator.Send(new GetAllProductQuery());
            return Ok(result);
        }


        [Authorize(Roles ="User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _mediator.Send(new GetAllProductByIdQuery { Id = id});
            return Ok(result);
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductCommand createProduct , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(createProduct , cancellationToken);
            return Ok(result);
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand updateProduct, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(updateProduct, cancellationToken);
            return Ok(result);
        }

        [HttpPost("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteProductCommand { Id=id}, cancellationToken);
            return Ok(result);
        }
    }
}
