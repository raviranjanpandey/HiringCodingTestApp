using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HiringCodingTestApis.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ExamResults
{
    public class ExamResultGetAllTotal : IRequest<int>
    {

    }
    public class ExamResultGetAllTotalHandler : IRequestHandler<ExamResultGetAllTotal, int>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;
        public ExamResultGetAllTotalHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(ExamResultGetAllTotal request, CancellationToken cancellationToken)
        {
            return await _interviewContext.Results.CountAsync();

        }

    }
}

