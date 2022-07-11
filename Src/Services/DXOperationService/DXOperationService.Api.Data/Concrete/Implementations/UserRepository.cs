using DXOperationService.Api.Core.Abstracts.Repositores;

using Med.Shared.Entities;
using Microsoft.AspNetCore.Identity;

namespace DXOperationService.Api.Data.Concrete.Implementations
{

    public class UserRepository: IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
       
    }
}
