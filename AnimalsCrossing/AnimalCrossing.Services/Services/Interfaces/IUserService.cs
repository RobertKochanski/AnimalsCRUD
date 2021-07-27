using AnimalCrossing.Services.RestModels.Users;
using AnimalCrossing.Services.ViewModels.User;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AnimalCrossing.Services.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoggedInUserResponse> Authenticate(string username, string password);

        Task AddAsync(CreateUserRequest request);

        Task<List<UserViewModel>> GetAllAsync();

        Task<UserViewModel> GetById(int id, ClaimsPrincipal claimsPrincipal);

        Task EditAsync(UpdateUserRequest request, ClaimsPrincipal claimsPrincipal);

        Task<UserViewModel> GetMyInfo(ClaimsPrincipal user);

        Task Remove(int id, ClaimsPrincipal claimsPrincipal);
    }
}
