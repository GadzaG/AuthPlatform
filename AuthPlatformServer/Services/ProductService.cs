using Grpc.Core;
using AuthPlatformServer.Repositories;

namespace AuthPlatformServer.Services
{
    public class ProductService : Product.ProductBase
    {
        private readonly ProductRepository _repository;

        public ProductService(ProductRepository repository)
        {
            _repository = repository;
        }

        public override async Task<CreateProductReply> CreateProduct(CreateProductRequest request, ServerCallContext context)
        {
            await _repository.Create(request.Name, request.Period, string.Empty);
            return new CreateProductReply
            {
                ReplyMessage = "Product created successfully."
            };
        }

        public override async Task<DeleteProductReply> DeleteProduct(DeleteProductRequest request, ServerCallContext context)
        {
            await _repository.Delete(new Guid(request.ProductId.ToString()));
            return new DeleteProductReply
            {
                ReplyMessage = "Product deleted successfully."
            };
        }

        public override async Task<GetAllProductReply> GetAllProduct(GetAllProductRequest request, ServerCallContext context)
        {
            var products = await _repository.Get();
            var productItems = products.Select(p => new ProductItem
            {
                Id = (int)p.Id.GetHashCode(),
                Name = p.Title,
                Period = p.Period
            }).ToList();

            return new GetAllProductReply
            {
                Products = { productItems }
            };
        }

        public override async Task<ChangeProductReply> ChangeProduct(ChangeProductRequest request, ServerCallContext context)
        {
            await _repository.Change(new Guid(request.Id.ToString()), request.Name, request.Period, string.Empty);
            return new ChangeProductReply
            {
                ReplyMessage = "Product updated successfully."
            };
        }
    }
}
