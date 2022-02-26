using AutoMapper;
using FluentValidation;
using MediatR;
using HiringCodingTestApis.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ExamsMaster
{
    public class ExamMasterUpdate : IRequest<int>
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
    }
    public class ExamMasterUpdateValidator : AbstractValidator<ExamMasterUpdate>
    {
        public ExamMasterUpdateValidator()
        {
            RuleFor(x => x.ExamId).NotEmpty().WithMessage("Exam Id can not be empty or zero.");
            RuleFor(x => x.ExamName).NotEmpty().WithMessage("Exam name can not be empty.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description can not be empty.");
        }
    }
    public class ExamMasterUpdateHandler : IRequestHandler<ExamMasterUpdate, int>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;
        public ExamMasterUpdateHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(ExamMasterUpdate request, CancellationToken cancellationToken)
        {
            var det = _mapper.Map<ExamMasterUpdate, ExamMaster>(request);

            var existing = await _interviewContext.ExamMaster.FindAsync(request.ExamId);
            if (existing == null) return 0;

            existing.ExamName = request.ExamName;
            existing.Description = request.Description;

            await _interviewContext.SaveChangesAsync();
            return det.ExamId;
        }
    }
}
