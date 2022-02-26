using MediatR;
using HiringCodingTestApis.Core.Answer;
using HiringCodingTestApis.Core.DTO;
using System;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.Services
{
    public class AnswerService
    {
        private readonly IMediator _mediator;
        public AnswerService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<int> Create(AnswerCreate create)
        {
            return await _mediator.Send(create);
        }

        public async Task<bool> CreateRange(AnswerCreateRangeCommand create)
        {
            return await _mediator.Send(create);
        }

        public async Task<int> Update(AnswerUpdate update)
        {
            return await _mediator.Send(update);
        }

        public async Task<bool> Delete(AnswersDelete delete)
        {
            return await _mediator.Send(delete);
        }

        public async Task<bool> DeleteByUserID(AnswerDeleteByUserId delete)
        {
            return await _mediator.Send(delete);
        }

        public async Task<bool> DeleteByGroupID(DeleteByExamGroupId delete)
        {
            return await _mediator.Send(delete);
        }

        public async Task<AnswersList> GetAnswersByExamGroupId(GetAnswersByExamGroupId get)
        {
            return await _mediator.Send(get);
        }

        public async Task<AnswersList> GetAnswersByExamId(GetAnswersByExamId get)
        {
            return await _mediator.Send(get);
        }

        public async Task<AnswersList> GetAnswersByUserId(GetAnswersByUserId get)
        {
            return await _mediator.Send(get);
        }
        public async Task<AnswersList> GetAnswerByScheduleId(AnswerByScheduleId get)
        {
            return await _mediator.Send(get);
        }
        public async Task<AnswerByQueListDto> GetAnswerByQueId(GetAnswerByQueId get)
        {
            return await _mediator.Send(get);
        }
    }
}
