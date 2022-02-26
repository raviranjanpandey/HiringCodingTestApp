using AutoMapper;
using MediatR;
using HiringCodingTestApis.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.AssignedUser
{
    public class AssignedUserUpdate : IRequest<int>
    {
        public int AssignedUserId { get; set; }
        public string UserId { get; set; }
        public string CreatedByUser { get; set; }
    }

    public class ExamResultUpdateHandler : IRequestHandler<AssignedUserUpdate, int>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;
        public ExamResultUpdateHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(AssignedUserUpdate request, CancellationToken cancellationToken)
        {
            var existing = await _interviewContext.AssignedUsers.FindAsync(request.AssignedUserId);
            if (existing == null) return 0;

            _mapper.Map(request, existing);

            if (await _interviewContext.SaveChangesAsync() > 0)
            {
                return existing.AssignedUserId;
            }

            return 0;
        }
    }
}
