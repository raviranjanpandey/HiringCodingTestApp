using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ExamResults
{
    public class ExamResultGetAll : Query<ExamResultList>
    {
        public ExamResultGetAll(int? take, int? skip) : base(take, skip)
        {

        }
    }

    public class ExamResultGetAllHandler : IRequestHandler<ExamResultGetAll, ExamResultList>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;
        public ExamResultGetAllHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }

        public async Task<ExamResultList> Handle(ExamResultGetAll request, CancellationToken cancellationToken)
        {
            var existing = await _interviewContext.Results.Skip((int)request.Skip).Take((int)request.Take).ToListAsync();
            if (existing == null) return new ExamResultList();
            return new ExamResultList
            {
                Results = _mapper.Map<List<Results>, List<ExamResultDto>>(existing)
            };
        }
    }
}
