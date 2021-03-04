using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public string Descripction { get; set; }
        public DateTime Date { get; set; }
    }
}
