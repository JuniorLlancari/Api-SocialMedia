using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;

namespace SocialMedia.Infrastructure.Repositories
{
    public class UnirOfWork : IUnitOfWork
    {

        private readonly SocialMediaContext _context;

        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Comment> _commentRepository;



        public UnirOfWork(SocialMediaContext context)
        {
            _context = context;
        }

        public IRepository<Post> PostRepository => _postRepository??new BaseRepository<Post>(_context);

        public IRepository<User> UserRepository => _userRepository ?? new BaseRepository<User>(_context);

        public IRepository<Comment> CommentRepository => _commentRepository ?? new BaseRepository<Comment>(_context);


        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public async Task SaveChangeAsync()
        {
           await _context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
