using AutoMapper;
using CommentsApp.Data;
using CommentsApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CommentsApp.Services.AssetServices
{
    public class AssetService : IAssetService
    {
        private readonly CommentsContext _context;
        private readonly IMapper _mapper;

        public AssetService(CommentsContext commentsContext, IMapper mapper)
        {
            _context = commentsContext;
            _mapper = mapper;
        }
        public async Task<Asset> CreateAsset(string assetId)
        {
            //var asset = _mapper.Map<Asset>(requestDto);
            var asset = new Asset() { Id = assetId, Comments = new List<Comment>() };
            await _context.Assets.AddAsync(asset);
            await _context.SaveChangesAsync();
            return await _context.Assets.FindAsync(assetId);
        }
    }
}
