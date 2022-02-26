using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ScheduledExamDetail
{
    public class SchExamDetGet : IRequest<List<SchExamDto>>
    {
    }
    public class SchExamDetGetHandler : IRequestHandler<SchExamDetGet, List<SchExamDto>>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;

        public SchExamDetGetHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }

        public async Task<List<SchExamDto>> Handle(SchExamDetGet request, CancellationToken cancellationToken)
        {
            var list = await _interviewContext.Scheduledexamdetails
                             .ToListAsync();

            if (list != null && list.Count > 0)
            {
                return _mapper.Map<List<Scheduledexamdetails>, List<SchExamDto>>(list);
            }
            return null;
        }
    }
}
