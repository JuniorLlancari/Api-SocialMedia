using SocialMedia.Core.Entities;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Post> PostRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Comment> CommentRepository { get; }

        void SaveChanges();
        Task SaveChangeAsync();



    }
}
