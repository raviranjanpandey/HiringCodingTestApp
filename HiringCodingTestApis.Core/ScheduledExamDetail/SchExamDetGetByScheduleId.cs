using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ScheduledExamDetail
{
    public class SchExamDetGetByScheduleId : IRequest<List<SchExamDto>>
    {
        public int ScheduleId { get; set; }
    }
    public class SchExamDetGetByScheduleIdHandler : IRequestHandler<SchExamDetGetByScheduleId, List<SchExamDto>>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;

        public SchExamDetGetByScheduleIdHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }

        public async Task<List<SchExamDto>> Handle(SchExamDetGetByScheduleId request, CancellationToken cancellationToken)
        {
            var list = await _interviewContext.Scheduledexamdetails
                             .Where(x => x.ScheduleId == request.ScheduleId)
                             .ToListAsync();

            if (list != null && list.Count > 0)
            {
                return _mapper.Map<List<Scheduledexamdetails>, List<SchExamDto>>(list);
            }
            return null;
        }
    }
}
