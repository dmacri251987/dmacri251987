using ServiceClient.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClient.Business.IService
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> CreateUpdateUserAsync(UserDto userDto);
        Task<bool> DeteleUserAsync(int idUser);
    }
}
