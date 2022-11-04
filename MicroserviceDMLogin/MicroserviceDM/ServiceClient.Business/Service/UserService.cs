using Microsoft.EntityFrameworkCore;
using ServiceClient.Business.DTOs;
using ServiceClient.Business.IService;
using ServicesCommon;
using ServicesIdentity.DataAccess.Data;
using ServicesIdentity.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClient.Business.Service
{
    public class UserService : IUserService
    {

        private readonly ApplicationDbContext _db;

        public UserService(ApplicationDbContext db)
        {

            _db = db;

        }


        public async Task<UserDto> CreateUpdateUserAsync(UserDto userDto)
        {

            User user = new User();

            try
            {
                user = userDto.MapTo<User>();


                if (user.Id > 0)
                {
                    _db.Update(user);
                    await _db.SaveChangesAsync();

                }
                else
                {
                    _db.User.Add(user);
                    await _db.SaveChangesAsync();
                }


            }
            catch (Exception ex)
            {
                //  _logger.LogError("Error al insertar en base de datos: {0}", ex.ToString());

                return new UserDto();
            }


            return user.MapTo<UserDto>();
        }

        public async Task<bool> DeteleUserAsync(int idUser)
        {
            User user = new User();

            try
            {
                user = _db.User.Where(c => c.Id == idUser).FirstOrDefault();

                if (user.Id > 0)
                {
                    _db.User.Remove(user);
                    await _db.SaveChangesAsync();

                }

            }
            catch (Exception ex)
            {
                // _logger.LogError("Error al eliminar en base de datos: {0}", ex.ToString());

                return false;
            }

            return true;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            User user = new User();

          
            try
            {
                user = _db.User.Where(c => c.Id == id).FirstOrDefault();

               
            }
            catch (Exception ex)
            {
                //  _logger.LogError("Error al traer los registros: {0}", ex.ToString());

                return new UserDto();
            }

            return user.MapTo<UserDto>();
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            List<User> user = new List<User>();


            try
            {
                user = await _db.User.ToListAsync();



            }
            catch (Exception ex)
            {
                //  _logger.LogError("Error al traer los registros: {0}", ex.ToString());

                return new List<UserDto>();
            }

            return user.MapTo<List<UserDto>>();
        }
    }
}
