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
    public class PostRepository :IPostRepository
    {
        private readonly SocialMediaContext _context;
        public PostRepository(SocialMediaContext context) 
        {
            _context = context;
        }


        public async Task<IEnumerable<Post>> GetPosts()
        {
            var caja = await _context.Posts.ToArrayAsync();

            return caja;


        }

    }
}
