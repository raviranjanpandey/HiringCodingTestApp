using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.AssignedUser
{
    public class AssignedUserGet : IRequest<AssignedUserListDto>
    {
        public string CreatedByUser { get; set; }
    }


    public class AssignedUserGetHandler : IRequestHandler<AssignedUserGet, AssignedUserListDto>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;
        public AssignedUserGetHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }

        public async Task<AssignedUserListDto> Handle(AssignedUserGet request, CancellationToken cancellationToken)
        {
            List<AssignedUserDto> assignedUser = new List<AssignedUserDto>();

            var existing = await _interviewContext.AssignedUsers.Where(x => x.CreatedByUser == request.CreatedByUser).ToListAsync();

            foreach (var user in existing)
            {
                assignedUser.Add(new AssignedUserDto
                {
                    AssignedUserId = user.AssignedUserId,
                    UserId = user.UserId,
                    CreatedByUser = user.CreatedByUser

                });
            }
            return new AssignedUserListDto
            {
                AssignedUserList = assignedUser
            };
        }
    }
}
