using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SocialMedia.Infrastructure.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {


        public PostRepository(SocialMediaContext context):base(context)
        {

        }

        public async Task<IEnumerable<Post>> GetPostsByUser(int userId)
        {
            return await _entities.Where(x => x.UserId == userId).ToListAsync();
        }
    }


}
