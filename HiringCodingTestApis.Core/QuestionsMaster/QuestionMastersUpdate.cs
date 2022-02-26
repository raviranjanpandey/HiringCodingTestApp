using AutoMapper;
using FluentValidation;
using MediatR;
using HiringCodingTestApis.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.QuestionsMaster
{
    public class QuestionMastersUpdate : IRequest<int>
    {
        public int QueId { get; set; }
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
       // public string UserId { get; set; }
    }

    public class QuestionMastersUpdateValidator : AbstractValidator<QuestionMastersUpdate>
    {
        public QuestionMastersUpdateValidator()
        {
            RuleFor(x => x.QueId).Must(examid => examid > 0).WithMessage("Question Id is mandatory.");
            RuleFor(x => x.ExamId).Must(examid => examid != null && examid > 0).WithMessage("Exam Id is mandatory.");
            RuleFor(x => x.Question).NotEmpty().WithMessage("Question is mandatory.");
        }
    }

    public class QuestionMastersUpdateHandler : IRequestHandler<QuestionMastersUpdate, int>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;

        public QuestionMastersUpdateHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(QuestionMastersUpdate request, CancellationToken cancellationToken)
        {
            var existing = await _interviewContext.QuestionMaster.FindAsync(request.QueId);
            if (existing == null) return 0;

            _mapper.Map(request, existing);

            if (await _interviewContext.SaveChangesAsync() > 0)
            {
                return existing.QueId;
            }

            return 0;
        }
    }
}
