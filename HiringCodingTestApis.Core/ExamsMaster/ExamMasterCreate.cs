using AutoMapper;
using FluentValidation;
using MediatR;
using HiringCodingTestApis.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ExamsMaster
{
    public class ExamMasterCreate : IRequest<int>
    {
        public string ExamName { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }

    }
    public class ExamMasterCreateValidator : AbstractValidator<ExamMasterCreate>
    {
        public ExamMasterCreateValidator()
        {
            RuleFor(x => x.ExamName).NotEmpty().WithMessage("Exam name can not be empty.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description can not be empty.");
        }
    }
    public class ExamMasterCreateHandler : IRequestHandler<ExamMasterCreate, int>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;
        public ExamMasterCreateHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(ExamMasterCreate request, CancellationToken cancellationToken)
        {
            var det = _mapper.Map<ExamMasterCreate, ExamMaster>(request);
            _interviewContext.ExamMaster.Add(det);
            await _interviewContext.SaveChangesAsync();
            return det.ExamId;
        }
    }
}
