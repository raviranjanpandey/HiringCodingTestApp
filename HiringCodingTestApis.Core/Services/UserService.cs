using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.Services
{
    public class UserService
    {
        private readonly InterviewContext _context;

        public UserService(InterviewContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            return await _context.AspNetUsers.Select(y => new UserDto
            {
                Id = y.Id,
                Email = y.Email,
                PhoneNumber = y.PhoneNumber,
                UserType = y.UserType,
                Name = y.Name,
                UserName = y.UserName
            }).ToListAsync();
        }
    }
}
