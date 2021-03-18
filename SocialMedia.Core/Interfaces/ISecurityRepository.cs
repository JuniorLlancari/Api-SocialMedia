using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SocialMedia.Core.Entities;

namespace SocialMedia.Core.Interfaces
{
    public interface ISecurityRepository:IRepository<Security>
    {
        Task<Security> GetLoginByCredentials(UserLogin login);
    }
}
