using MediatR;
using HiringCodingTestApis.Core.CreateUser;
using HiringCodingTestApis.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.Services
{
    public class CreateUserService
    {
        private readonly IMediator _mediator;
        public CreateUserService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<string> Create(CreateUserCommand create)
        {
            return await _mediator.Send(create);
        }
        public async Task<string> Update(UpdateUserCommand update)
        {
            return await _mediator.Send(update);
        }
        public async Task<IEnumerable<CreateGetUserDto>> Get(GetUserCommand Get)
        {
            return await _mediator.Send(Get);
        }
        public async Task<bool> DuplicationCheck(UserDuplicationCheck Get)
        {
            return await _mediator.Send(Get);
        }
        public async Task<int> TotalCreateUser(string userId, bool isAll)
        {
            return await _mediator.Send(new TotalCreateUserCommand(userId, isAll));
        }
        public async Task<IEnumerable<CreateGetUserDto>> CreateMasterFilter(GetUserFilterCommand Get)
        {
            return await _mediator.Send(Get);
        }
        public async Task<int> TotalGetCreateFilter(string userId, bool isAll, string serachvalue)
        {
             return await _mediator.Send(new TotalGetUserFilterCommand(userId, isAll, serachvalue));
        }
    }
}
