using MediatR;
using Newtonsoft.Json;
using HiringCodingTestApis.Core.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.CreateUser
{
    public class UpdateUserCommand : IRequest<string>
    {

        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> AllowedSchedule { get; set; }
        public short UserType { get; set; }
        public string CreatedByUser { get; set; }
        public int? EntityId { get; set; }
    }



    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, string>
    {
        private readonly InterviewContext _interviewContext;
        public UpdateUserCommandHandler(InterviewContext interviewContext)
        {
            _interviewContext = interviewContext;
        }

        public async Task<string> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var existing = await _interviewContext.AspNetUsers.FindAsync(request.UserId);

            if (existing != null)
            {
                existing.UserName = request.Email;
                existing.Name = request.Name;
                existing.PhoneNumber = request.PhoneNumber;
                existing.UserType = request.UserType;
                existing.CreatedByUser = request.CreatedByUser;
                existing.AllowedSchedule = JsonConvert.SerializeObject(request.AllowedSchedule);
                await _interviewContext.SaveChangesAsync();
                return existing.Id;
            }
            return existing.Id;
        }
    }
}
