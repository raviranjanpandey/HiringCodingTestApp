using MediatR;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.QuestionsMaster;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.Services
{
    public class QuestionMasterService
    {
        private readonly IMediator _mediator;
        public QuestionMasterService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<int> Create(QuestionMastersCreate create)
        {
            return await _mediator.Send(create);
        }

        public async Task<bool> CreateRange(QuestionMasterCreateRangeCommand create)
        {
            return await _mediator.Send(create);
        }

        public async Task<int> Update(QuestionMastersUpdate update)
        {
            return await _mediator.Send(update);
        }

        public async Task<bool> Delete(QuestionMastersDelete delete)
        {
            return await _mediator.Send(delete);
        }

        public async Task<QuestionMasterList> GetByExamId(int ExamId, string UserId,int? take, int? skip)
        {
            return await _mediator.Send<QuestionMasterList>(new QuestionMastersGetByExamId(UserId, ExamId, take,skip));
        }

        public async Task<QuestionMasterList> GetByGroupId(QuestionMastersGetByGroupId get)
        {
            return await _mediator.Send(get);
        }
        public async Task<QuestionMasterList> GetByUserId(QuestionMasterByGet get)
        {
            return await _mediator.Send(get);
        }
        public async Task<int> QuestionMasterTotal(QuestionMasterTotal get)
        {
            return await _mediator.Send(get);
        }
        public async Task<int> totalQuestionMasterByExamId(int ExamId, string UserId)
        {
            return await _mediator.Send(new TotalQuestionMasterByExamId(UserId, ExamId));
        }
        public async Task<QuestionMasterList>  QuestionMasterFilter(int ExamId, string Serachvalue, int? take, int? skip)
        {
            return await _mediator.Send(new QuestionMasterFilter(ExamId, Serachvalue, take, skip));
        }
        public async Task<int> totalFilter(int ExamId, string Serachvalue)
        {
            return await _mediator.Send(new TotalCountFilter(Serachvalue,ExamId));
        }
    }
}
