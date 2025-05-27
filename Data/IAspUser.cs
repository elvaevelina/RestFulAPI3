using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.DTO;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public interface IAspUser
    {
        IEnumerable<AspUser> GetAllUsers();
        AspUser RegisterUser(RegisterDTO dto);
        AspUser GetUserByUsername(string username);
        AspUser UpdateUser(AspUser user);
        AspUser DeleteUser(string username);
        bool Login(LoginDTO dto);
    }
}