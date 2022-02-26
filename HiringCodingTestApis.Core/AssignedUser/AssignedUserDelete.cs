using MediatR;
using Microsoft.EntityFrameworkCore;
using HiringCodingTestApis.Core.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.AssignedUser
{
    public class AssignedUserDelete : IRequest<bool>
    {
        public string UserId { get; set; }
        public string CreatedByuser { get; set; }
    }


    public class AssignedUserDeleteHandler : IRequestHandler<AssignedUserDelete, bool>
    {
        private readonly InterviewContext _interviewContext;
        public AssignedUserDeleteHandler(InterviewContext interviewContext)
        {
            _interviewContext = interviewContext;
        }

        public async Task<bool> Handle(AssignedUserDelete request, CancellationToken cancellationToken)
        {           
            var item = await _interviewContext.AssignedUsers.Where(x => x.UserId == request.UserId).ToListAsync();

            if (item.Count == 1)
            {
                var data = _interviewContext.AssignedUsers.Where(x => x.UserId == request.UserId).SingleOrDefault();
                var data1 = _interviewContext.AspNetUsers.Where(x => x.Id == request.UserId).SingleOrDefault();
                _interviewContext.AssignedUsers.RemoveRange(data);
                _interviewContext.AspNetUsers.RemoveRange(data1);
            }
            else
            {
                var existing = await _interviewContext.AssignedUsers.
                                  Where(x => x.UserId == request.UserId && x.CreatedByUser == request.CreatedByuser).
                                  ToListAsync();

                if (existing == null) return false;
                _interviewContext.AssignedUsers.RemoveRange(existing);
               
            }
            return await _interviewContext.SaveChangesAsync() > 0;
        }
    }
}
