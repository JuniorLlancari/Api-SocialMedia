using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetPosts();
    }
}
