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
    public class UserRepository //: BaseRepository<User>, IUserRepository
    {
    

        /*
        private readonly SocialMediaContext _context;
        public UserRepository(SocialMediaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.ToArrayAsync();
            return users;
        }


        public async Task<User> GetUser(int id)
        {

            var caja = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            return caja;

        }
        */

 
    }
}
