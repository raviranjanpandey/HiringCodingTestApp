using MediatR;
using Newtonsoft.Json;
using HiringCodingTestApis.Core.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.CreateUser
{
    public class CreateUserCommand : IRequest<string>
    {

        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> AllowedSchedule { get; set; }
        public short UserType { get; set; }
        public string CreatedByUser { get; set; }
        public int? EntityId { get; set; }
    }



    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly InterviewContext _interviewContext;
        public CreateUserCommandHandler(InterviewContext interviewContext)
        {
            _interviewContext = interviewContext;
        }

        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            AspNetUsers users = new AspNetUsers
            {
                
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                AllowedSchedule = JsonConvert.SerializeObject(request.AllowedSchedule),
                UserName = request.Email,
                UserType = request.UserType,
                CreatedByUser = request.CreatedByUser,
            };

            _interviewContext.AspNetUsers.Add(users);
            var res = await _interviewContext.SaveChangesAsync() > 0;
            return res.ToString();

        }
    }
}