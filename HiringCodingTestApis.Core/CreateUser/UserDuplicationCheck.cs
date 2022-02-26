using MediatR;
using Microsoft.EntityFrameworkCore;
using HiringCodingTestApis.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.CreateUser
{
    public class UserDuplicationCheck : IRequest<bool>
    {
        public string UserId { get; set; }
        public string CreatedByUser { get; set; }
    }


    public class UserDuplicationCheckHandler : IRequestHandler<UserDuplicationCheck, bool>
    {
        private readonly InterviewContext _interviewContext;
        public UserDuplicationCheckHandler(InterviewContext interviewContext)
        {
            _interviewContext = interviewContext;
        }

        public async Task<bool> Handle(UserDuplicationCheck request, CancellationToken cancellationToken)
        {
            var item = await _interviewContext.AssignedUsers.Where(x => x.UserId == request.UserId && x.CreatedByUser==request.CreatedByUser).ToListAsync();

            if (item == null || item.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }


        }
         
                
    }
   
}
             
        
    