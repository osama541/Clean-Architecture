using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Product.Quries
{
    public class GetAllProductQuery : IRequest<ApiResponse<List<Domain.Entites.Product>>>
    {
        internal class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, ApiResponse<List<Domain.Entites.Product>>>
        {
            private readonly IApplicationDbContext _context;

            public GetAllProductQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ApiResponse<List<Domain.Entites.Product>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
            {
                var result = await _context.Products.ToListAsync(cancellationToken);

                if (!result.Any())
                {
                    throw new ApiExceptions("No products found");
                }

                return new ApiResponse<List<Domain.Entites.Product>>(result, "Data fetch successful");
            }
        }
    }


}
