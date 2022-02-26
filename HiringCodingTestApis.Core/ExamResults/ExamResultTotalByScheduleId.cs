using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HiringCodingTestApis.Core.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ExamResults
{
    public class ExamResultTotalByScheduleId : IRequest<int>
    {
        public int ScheduleId { get; set; }
    }
    public class ExamResultTotalByScheduleIdHandler : IRequestHandler<ExamResultTotalByScheduleId, int>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;
        public ExamResultTotalByScheduleIdHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(ExamResultTotalByScheduleId request, CancellationToken cancellationToken)
        {
            return await _interviewContext.Results.Include(x => x.Exam).Include(x => x.User).Where(x => x.ScheduleId == request.ScheduleId).CountAsync();

        }

    }
}
