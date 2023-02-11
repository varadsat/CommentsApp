using CommentsApp.Models;

namespace CommentsApp.Services.AssetServices
{
    public interface IAssetService
    {
        Task<Asset> CreateAsset(string id);
    }
}
