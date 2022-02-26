using AutoMapper;
using FluentValidation;
using MediatR;
using HiringCodingTestApis.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.QuestionsMaster
{
    public class QuestionMasterCreateRange
    {
        public int? ExamId { get; set; }
        public string SubModule { get; set; }
        public string Question { get; set; }
        public short? AnswerType { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public short? CorrectOption { get; set; }
        public short? Seconds { get; set; }
        public int? GroupId { get; set; }
        public string UserId { get; set; }

    }

    public class QuestionMasterCreateRangeCommand : IRequest<bool>
    {
        public List<QuestionMasterCreateRange> QuestionMasterList { get; set; }
    }

    public class QuestionMasterCreateRangeCommandValidator : AbstractValidator<QuestionMasterCreateRangeCommand>
    {
        public QuestionMasterCreateRangeCommandValidator()
        {
            RuleForEach(x => x.QuestionMasterList).ChildRules(QuestionMaster =>
            {
                QuestionMaster.RuleFor(x => x.ExamId).Must(examid => examid != null && examid > 0).WithMessage("Exam Id is mandatory.");
                QuestionMaster.RuleFor(x => x.Question).NotEmpty().WithMessage("Question is mandatory.");
                QuestionMaster.RuleFor(x => x.GroupId).GreaterThan(0).WithMessage("Group id is mandatory.");
            });
        }
    }

    public class QuestionMasterCreateRangeCommandHandler : IRequestHandler<QuestionMasterCreateRangeCommand, bool>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;

        public QuestionMasterCreateRangeCommandHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }

        public async Task<bool> Handle(QuestionMasterCreateRangeCommand request, CancellationToken cancellationToken)
        {
            var det = _mapper.Map<List<QuestionMasterCreateRange>, List<QuestionMaster>>(request.QuestionMasterList);
            _interviewContext.QuestionMaster.AddRange(det);
            return await _interviewContext.SaveChangesAsync() > 0;
        }
    }
}
