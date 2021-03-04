using System;
using System.Collections.Generic;
using System.Text;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System.Linq;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository :IPostRepository
    {
        public IEnumerable<Post> GetPosts()
        {

            var posts = Enumerable.Range(1, 5).Select(x => new Post
            {
                PostId=x,
                Descripction=$"Hola este post {x}",
                Date=DateTime.Now
            });

            return posts;
        }

    }
}
