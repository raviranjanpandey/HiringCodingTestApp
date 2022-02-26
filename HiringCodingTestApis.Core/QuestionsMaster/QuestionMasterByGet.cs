using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.QuestionsMaster
{
    public class QuestionMasterByGet : Query<QuestionMasterList>
    {
        public string UserId { get; set; }
        public QuestionMasterByGet(string userId, int? take, int? skip) : base(take, skip)
        {
            UserId = userId;
        }

    

    public class ExamMasterGetHandler : IRequestHandler<QuestionMasterByGet, QuestionMasterList>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;
        public ExamMasterGetHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _mapper = mapper;
            _interviewContext = interviewContext;
        }
        public async Task<QuestionMasterList> Handle(QuestionMasterByGet request, CancellationToken cancellationToken)
        {
            var existing = await _interviewContext.ExamSchedule.Where(x => x.UserId == request.UserId).Include(x => x.Scheduledexamdetails).Skip((int)request.Skip).Take((int)request.Take).ToListAsync();

            var quesList = await _interviewContext.QuestionMaster.ToListAsync();
            if (quesList != null && quesList.Count > 0)
            {
                return new QuestionMasterList
                {
                    QuestionsList = _mapper.Map<List<QuestionMaster>, List<QuestionMastersDto>>(quesList)
                };
            }
            
            return new QuestionMasterList
            {
                QuestionsList = new List<QuestionMastersDto>()
            };
        }
    }
}
    }
