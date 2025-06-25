using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Product.Quries
{
    public class GetAllProductByIdQuery : IRequest<ApiResponse<List<Domain.Entites.Product>>>
    {
        public int Id { get; set; }

        public class GetAllProductByIdQueryHandler : IRequestHandler<GetAllProductByIdQuery, ApiResponse<List<Domain.Entites.Product>>>
        {
            private readonly IApplicationDbContext _context;

            public GetAllProductByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ApiResponse<List<Domain.Entites.Product>>> Handle(GetAllProductByIdQuery request, CancellationToken cancellationToken)
            {
                // Fetch products (if expecting list, use Where + ToList)
                var result = await _context.Products
                    .Where(x => x.Id == request.Id)
                    .ToListAsync(cancellationToken);

                if (result == null || result.Count == 0)
                {
                    throw new ApiExceptions("No product found with this ID.");
                }

                return new ApiResponse<List<Domain.Entites.Product>>(result, "Product fetched successfully.");
            }
        }
    }
}
