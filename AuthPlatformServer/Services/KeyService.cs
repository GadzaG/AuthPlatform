using AuthPlatformServer.Repositories;
using Grpc.Core;
using AuthPlatformServer.Models;

namespace AuthPlatformServer.Services
{
    public class KeyService : Key.KeyBase
    {
        private readonly KeyRepository _repository;

        public KeyService(KeyRepository repository)
        {
            _repository = repository;
        }

        public override async Task<GenerateKeysReply> GenerateKeys(GenerateKeysRequest request, ServerCallContext context)
        {
            await _repository.Create(new Guid(request.ProductId.ToString()), request.KeyCount);
            return new GenerateKeysReply
            {
                Message = "Keys generated successfully."
            };
        }

        public override async Task<DeleteKeysReply> DeleteKeys(DeleteKeysRequest request, ServerCallContext context)
        {
            var ids = request.KeyId.Select(id => new Guid(id.ToString())).ToList();
            await _repository.Delete(ids);
            return new DeleteKeysReply
            {
                Message = "Keys deleted successfully."
            };
        }

        public override async Task<ChangeKeyStatusReply> ChangeKeyStatus(ChangeKeyStatusRequest request, ServerCallContext context)
        {
            var status = Enum.Parse<Models.Status>(request.NewStatus);
            await _repository.Change(new Guid(request.KeyId.ToString()), status);
            return new ChangeKeyStatusReply
            {
                Message = "Key status changed successfully."
            };
        }

        public override async Task<GetKeysReply> GetKeys(GetKeysRequest request, ServerCallContext context)
        {
            var keys = await _repository.GetByProduct(new Guid(request.ProductId.ToString()));
            var keyItems = keys.Select(k => new KeyItem
            {
                Id = (int)k.Id.GetHashCode(),
                ProductId = (int)k.ProductId.GetHashCode(),
                KeyValue = k.KeyText,
                Status = k.Status.ToString(),
                StartTime = k.ActivationTime?.ToString(),
                Data = k.Data?.ToString()
            }).ToList();

            return new GetKeysReply
            {
                Message = "Keys retrieved successfully.",
                Keys = { keyItems }
            };
        }

        public override async Task<DownloadDataKeyReply> DownloadDataKey(DownloadDataKeyRequest request, ServerCallContext context)
        {
            var data = await _repository.GetData(new Guid(request.KeyId.ToString()));
            return new DownloadDataKeyReply
            {
                Data = data?.ToString()
            };
        }
    }
}
