using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HiringCodingTestApis.Core.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ExamResults
{
    public class ExamResultTotalGet : IRequest<int>
    {
        public string UserId { get; set; }
    }
    public class ExamResultTotalGetHandler : IRequestHandler<ExamResultTotalGet, int>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;
        public ExamResultTotalGetHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(ExamResultTotalGet request, CancellationToken cancellationToken)
        {
            return await _interviewContext.Results.Where(x => x.UserId == request.UserId).CountAsync();

        }

    }
}