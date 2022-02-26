using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HiringCodingTestApis.Core.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.QuestionsMaster
{
    public class QuestionMastersCreate : IRequest<int>
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
        public string UserId { get; set; }
    }

    public class QuestionMastersCreateValidator : AbstractValidator<QuestionMastersCreate>
    {
        public QuestionMastersCreateValidator()
        {
            RuleFor(x => x.ExamId).Must(examid => examid != null && examid > 0).WithMessage("Exam Id is mandatory.");
            RuleFor(x => x.Question).NotEmpty().WithMessage("Question is mandatory.");
        }
    }

    public class QuestionMastersCreateHandler : IRequestHandler<QuestionMastersCreate, int>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;

        public QuestionMastersCreateHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(QuestionMastersCreate request, CancellationToken cancellationToken)
        {
            var det = _mapper.Map<QuestionMastersCreate, QuestionMaster>(request);            
            if (request.Question == det.Question)
            {
                var existing = await _interviewContext.QuestionMaster.Where(x => x.Question == request.Question).FirstOrDefaultAsync();
                if (existing != null)
                {
                    existing.Question = request.Question;
                    await _interviewContext.SaveChangesAsync();
                    return existing.QueId;

                }
            }
            _interviewContext.QuestionMaster.Add(det);
            await _interviewContext.SaveChangesAsync();
            return det.QueId;
        }
    }
}
