using MediatR;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.ScheduledExamDetail;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.Services
{
    public class SchExamDetService
    {
        private readonly IMediator _mediator;
        public SchExamDetService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Create(SchExamDetCreate create)
        {
            return await _mediator.Send(create);
        }

        public async Task<bool> Delete(SchExamDetDelete delete)
        {
            return await _mediator.Send(delete);
        }

        public async Task<List<SchExamDto>> Get(SchExamDetGet get)
        {
            return await _mediator.Send(get);
        }

        public async Task<List<SchExamDto>> GetByScheduleId(SchExamDetGetByScheduleId get)
        {
            return await _mediator.Send(get);
        }
    }
}
