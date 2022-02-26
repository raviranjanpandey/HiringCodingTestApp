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

namespace HiringCodingTestApis.Core.QuestionsMaster
{
    public class QuestionMastersGetByExamId : Query<QuestionMasterList>
    {
        public int ExamId { get; set; }
        public string UserId { get; set; }
    
     
        public QuestionMastersGetByExamId(string userId, int examId, int? take, int? skip) : base(take, skip)
        {
            UserId = userId;
            ExamId = examId;
        }
    }

    public class QuestionMastersGetByExamIdValidator : AbstractValidator<QuestionMastersGetByExamId>
    {
        public QuestionMastersGetByExamIdValidator()
        {
            RuleFor(x => x.ExamId).GreaterThan(0).WithMessage("Examid can not be zero.");
        }
    }

    public class QuestionMastersGetByExamIdHandler : IRequestHandler<QuestionMastersGetByExamId, QuestionMasterList>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;

        public QuestionMastersGetByExamIdHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }

        public async Task<QuestionMasterList> Handle(QuestionMastersGetByExamId request, CancellationToken cancellationToken)
        {
            var result = await _interviewContext.QuestionMaster.Where(x => x.ExamId == request.ExamId &&  x.UserId == request.UserId).Skip((int)request.Skip).Take((int)request.Take).ToListAsync();
            if (result != null)
            {
                return new QuestionMasterList
                {
                    QuestionsList = _mapper.Map<List<QuestionMaster>, List<QuestionMastersDto>>(result)
                };
            }
            return null;
        }
    }
}

