using MediatR;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.ExamResults;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.Services
{
    public class ExamResultService
    {
        private readonly IMediator _mediator;
        public ExamResultService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<int> Create(ExamResultCreate create)
        {
            return await _mediator.Send(create);
        }

        public async Task<int> Update(ExamResultUpdate update)
        {
            return await _mediator.Send(update);
        }

        public async Task<bool> Delete(ExamResultDelete delete)
        {
            return await _mediator.Send(delete);
        }

        public async Task<ExamResultList> Get(ExamResultGet get)
        {
            return await _mediator.Send(get);
        }

        public async Task<ExamResultList> GetByScheduleId(ExamResultGetByScheduleId get)
        {
            return await _mediator.Send(get);
        }

        public async Task<ExamResultList> GetAll(ExamResultGetAll get)
        {
            return await _mediator.Send(get);
        }
        public async Task<ExamResultList> GetExamResultUserId(GetExamResultByUserId get)
        {
            return await _mediator.Send(get);
        }
        public async Task<int> ExamMasterTotalGetAll(ExamResultGetAllTotal get)
        {
            return await _mediator.Send(get);
        }
        public async Task<int> ExamMasterTotalGet(ExamResultTotalGet get)
        {
            return await _mediator.Send(get);
        }
        public async Task<int> ExamMasterTotalByScheduleid(ExamResultTotalByScheduleId get)
        {
            return await _mediator.Send(get);
        }
        public async Task<ExamResultList> ResultByUserScheduleId(GetResultByUserScheduleId get)
        {
            return await _mediator.Send(get);
        }
    }
}
