using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleRESTApi.DTO;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class AspUserEF : IAspUser
    {
        private readonly ApplicationDbContext __context;
        public AspUserEF(ApplicationDbContext context)
        {
            __context = context;
        }

        public AspUser DeleteUser(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username cannot be null or empty", nameof(username));
            }

            var user = __context.AspUsers.Find(username);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with username '{username}' not found.");
            }

            __context.AspUsers.Remove(user);
            __context.SaveChanges();
            return user;
        }

        public IEnumerable<AspUser> GetAllUsers()
        {
            return __context.AspUsers.ToList();
        }

        public AspUser GetUserByUsername(string username)
        {
            var user = __context.AspUsers.Find(username);
            if (user == null) throw new Exception("User not found");
            return user;
        }

        public bool Login(LoginDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "LoginDTO cannot be null");
            }
           // Assuming HashHelper.HashPassword is a method that hashes the password
            var hashedPassword = Helpers.HashHelper.HashPassword(dto.Password);
            return __context.AspUsers.Any(u =>
                u.Username == dto.Username &&
                u.Password == hashedPassword);
        }

        // public AspUser RegisterUser(AspUser user)
        // {
        //     try
        //     {
        //         if (user == null)
        //         {
        //             throw new ArgumentException(nameof(user), "User cannot be null");
        //         }
        //         user.Password = Helpers.HashHelper.HashPassword(user.Password);
        //                         __context.AspUsers.Add(user);
        //         __context.SaveChanges();
        //         return user;
        //     }
        //     catch (DbUpdateException dbex)
        //     {
        //         // Log the exception (not implemented here)
        //         throw new Exception("An error occurred while registering the user.", dbex);
        //     }
        //     catch (System.Exception ex)
        //     {
        //         throw new Exception("An unexpected error occured", ex);
        //     }
        // }

        public AspUser RegisterUser(RegisterDTO dto)
        {
            if(dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "RegisterDTO cannot be null");
            }

            var user = new AspUser
            {
                Username = dto.Username,
                Password = Helpers.HashHelper.HashPassword(dto.Password),
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                Address = dto.Address,
                City = dto.City,
                Country = dto.Country
            };

            __context.AspUsers.Add(user);
            __context.SaveChanges();
            return user;
        }

        public AspUser UpdateUser(AspUser user)
        {
            var existing = __context.AspUsers.Find(user.Username);
            if (existing == null)
            {
                throw new KeyNotFoundException($"User with username '{user.Username}' not found.");
            }
            existing.Email = user.Email;
            existing.PhoneNumber = user.PhoneNumber;
            existing.Firstname = user.Firstname;
            existing.Lastname = user.Lastname;
            existing.Address = user.Address;
            existing.City = user.City;
            existing.Country = user.Country;
            try
            {
                __context.SaveChanges();
                return existing;
            }
            catch (DbUpdateException dbex)
            {
                throw new Exception("Error updating user", dbex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating user", ex);
            }
        }
    }
}