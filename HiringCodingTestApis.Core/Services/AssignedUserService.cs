using MediatR;
using HiringCodingTestApis.Core.AssignedUser;
using HiringCodingTestApis.Core.DTO;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.Services
{
    public class AssignedUserService
    {
        private readonly IMediator _mediator;
        public AssignedUserService(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task<int> Create(AssignedUserCreate create)
        {
            return await _mediator.Send(create);
        }

        public async Task<int> Update(AssignedUserUpdate update)
        {
            return await _mediator.Send(update);
        }
        public async Task<AssignedUserListDto> Get(AssignedUserGet get)
        {
            return await _mediator.Send(get);
        }
        public async Task<bool> Delete(AssignedUserDelete delete)
        {
            return await _mediator.Send(delete);
        }
    }
}
