using SocialMedia.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Core.CustomEntities;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostService
    {
        Task InsertPost(Post post);
        PagedList<Post> GetPosts(PostQueryFilter filter);
        Task<Post> GetPost(int id);
        Task<bool> UpdatePost(Post post);
        Task<bool> DeletePost(int id);
    }
}