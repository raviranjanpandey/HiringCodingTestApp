using MediatR;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.ExamGroups;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.Services
{
    public class ExamGroupService
    {
        private readonly IMediator _mediator;
        public ExamGroupService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<int> Create(ExamGroupCreate examGroup)
        {
            return await _mediator.Send(examGroup);
        }

        public async Task<int> Update(ExamGroupUpdate examGroup)
        {
            return await _mediator.Send(examGroup);
        }

        public async Task<bool> Delete(ExamGroupDelete examGroup)
        {
            return await _mediator.Send(examGroup);
        }

        public async Task<IEnumerable<ExamGroupDto>> Get(ExamGroupGet get)
        {
            return await _mediator.Send((get));
        }

        public async Task<ExamGroupList> GetById(ExamGroupGetById getbyid)
        {
            return await _mediator.Send(getbyid);
        }

        public async Task<ExamGroupList> GetByName(ExamGroupGetByName examGroupbyName)
        {
            return await _mediator.Send(examGroupbyName);
        }
    }
}
