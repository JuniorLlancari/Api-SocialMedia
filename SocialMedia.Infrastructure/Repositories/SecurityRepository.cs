using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SocialMedia.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace SocialMedia.Infrastructure.Repositories
{
    public class SecurityRepository : BaseRepository<Security>, ISecurityRepository
    {

        public SecurityRepository(SocialMediaContext context):base(context)
        {

        }

        public async Task<Security> GetLoginByCredentials(UserLogin login)
        {
           return await _entities.FirstOrDefaultAsync(x => x.User == login.User);
        }
    }
}
