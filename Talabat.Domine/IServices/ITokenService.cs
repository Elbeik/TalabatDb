using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domine.Entites.Identity;

namespace Talabat.Domine.IServices
{
    public interface ITokenService
    {
        Task<string> CreatToken(AppUser appUser, UserManager<AppUser> userManager);
    }
}
