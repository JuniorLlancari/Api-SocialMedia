using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface ISecurityServices
    {
        Task RegisterUser(Security security);
        Task<Security> GetLoginByCredentials(UserLogin login);
    }
}
