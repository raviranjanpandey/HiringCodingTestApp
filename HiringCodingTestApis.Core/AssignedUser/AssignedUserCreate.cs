using AutoMapper;
using MediatR;
using HiringCodingTestApis.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.AssignedUser
{
    public class AssignedUserCreate : IRequest<int>
    {
        public int AssignedUserId { get; set; }
        public string UserId { get; set; }
        public string CreatedByUser { get; set; }
    }

   
    public class AssignedUserCreateHandler : IRequestHandler<AssignedUserCreate, int>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;
        public AssignedUserCreateHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(AssignedUserCreate request, CancellationToken cancellationToken)
        {
            var det = _mapper.Map<AssignedUserCreate, AssignedUsers>(request);
            _interviewContext.AssignedUsers.Add(det);
            await _interviewContext.SaveChangesAsync();
            return det.AssignedUserId;
        }
    }
}