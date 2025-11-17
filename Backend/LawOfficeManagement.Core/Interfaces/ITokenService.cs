using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Core.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(string userId, string email, string userName, List<string> roles);
    }
}
