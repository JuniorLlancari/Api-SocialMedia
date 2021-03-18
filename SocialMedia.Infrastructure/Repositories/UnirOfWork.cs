using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class UnirOfWork : IUnitOfWork
    {

        private readonly SocialMediaContext _context;

        private readonly IPostRepository _postRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly ISecurityRepository _securityrepository;




        public UnirOfWork(SocialMediaContext context)
        {
            _context = context;
        }

        public IPostRepository PostRepository => _postRepository ?? new PostRepository(_context);

        public IRepository<User> UserRepository => _userRepository ?? new BaseRepository<User>(_context);

        public IRepository<Comment> CommentRepository => _commentRepository ?? new BaseRepository<Comment>(_context);

        public ISecurityRepository SecurityRepository => _securityrepository ?? new SecurityRepository(_context);




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
