using AutoMapper;
using FluentValidation;
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
    public class ExamResultGet : IRequest<ExamResultList>
    {
        public string UserId { get; set; }
    }
      

    public class ExamResultGetHandler : IRequestHandler<ExamResultGet, ExamResultList>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;
        public ExamResultGetHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }

        public async Task<ExamResultList> Handle(ExamResultGet request, CancellationToken cancellationToken)
        {
            var existing = await _interviewContext.Results.Where(x => x.UserId == request.UserId).ToListAsync();
            if (existing == null) return new ExamResultList();
            return new ExamResultList
            {
                Results = _mapper.Map<List<Results>, List<ExamResultDto>>(existing)
            };
        }
    }
}
