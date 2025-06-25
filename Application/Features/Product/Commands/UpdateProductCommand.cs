using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.SharedServices;

namespace Application.Features.Product.Commands
{
    public class UpdateProductCommand : IRequest<ApiResponse<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }

        internal class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ApiResponse<int>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IAuthenticatedUser _authenticatedUser;
            public UpdateProductCommandHandler(IApplicationDbContext context, IMapper mapper, IAuthenticatedUser authenticatedUser)
            {
                _context = context;
                _mapper = mapper;
                _authenticatedUser = authenticatedUser;
            }
            public async Task<ApiResponse<int>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                // Your logic to create the product and return its ID
                var product = await _context.Products.Where(x => x.Id == request.Id).FirstOrDefaultAsync();
                if (product == null)
                {

                    throw new ApiExceptions("Produnt not found");
                }

                product.Name = request.Name;
                product.Description = request.Description;
                product.Rate = request.Rate;
                product.ModifiedBy = _authenticatedUser.UserId;
                product.ModifiedOn = DateTime.Now;
                await _context.SaveChangesAsync();
                return new ApiResponse<int>(product.Id, "Product updated successful");





            }
        }

    }
}
