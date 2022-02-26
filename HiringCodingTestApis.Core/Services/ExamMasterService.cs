using MediatR;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.ExamsMaster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.Services
{
    public class ExamMasterService
    {
        private readonly IMediator _mediator;
        public ExamMasterService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<ExamMasterDto>> Get(ExamMasterGet get)
        {
            return await _mediator.Send(get);
        }

        public async Task<int> Create(ExamMasterCreate create)
        {
            return await _mediator.Send(create);
        }

        public async Task<int> Update(ExamMasterUpdate update)
        {
            return await _mediator.Send(update);
        }

        public async Task<bool> Delete(ExamMasterDelete delete)
        {
            return await _mediator.Send(delete);
        }
    }
}
