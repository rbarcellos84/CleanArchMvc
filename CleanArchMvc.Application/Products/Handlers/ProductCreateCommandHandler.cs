using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers
{
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        public ProductCreateCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Product> Handle(ProductCreateCommand request,
            CancellationToken cancellationToken)
        {
            if (request.CategoryId > 0)
            {
                var product = new Product(request.CategoryId, request.Name, request.Description, request.Price,
                              request.Stock, request.Image);

                if (product == null)
                {
                    throw new ApplicationException($"Error creating entity.");
                }
                else
                {
                    return await _productRepository.CreateAsync(product);
                }
            }
            else
            {
                throw new ApplicationException($"Error creating entity.");
            }
        }
    }
}
