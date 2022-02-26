using AutoMapper;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.CreateUser
{
    public class GetUserCommand : Query<IEnumerable<CreateGetUserDto>>
    {
        public string UserId { get; set; }
        public bool IsAll { get; set; }

        public GetUserCommand(string userId, bool isAll, int? take, int? skip) : base(take, skip)
        {
            UserId = userId;
            IsAll = isAll;
        }

        public class ExamGroupGetHandler : IRequestHandler<GetUserCommand, IEnumerable<CreateGetUserDto>>
        {
            private readonly ISqlConnectionFactory _connection;
            private readonly IMapper _mapper;
            public ExamGroupGetHandler(ISqlConnectionFactory connection, IMapper mapper)
            {
                _connection = connection;
                _mapper = mapper;
            }
            public async Task<IEnumerable<CreateGetUserDto>> Handle(GetUserCommand request, CancellationToken cancellationToken)
            {

                using var connection = _connection.GetOpenConnection();

                string sql = $"select * from interview.admin_user_master('{request.UserId}','{request.Take}','{request.Skip}','{request.IsAll}')";
                try
                {
                    List<CreateGetUserDto> createUser = new List<CreateGetUserDto>();
                    var res = await connection.QueryAsync<CreateUserByUserIdDto>(sql);
                    foreach (var user in res)
                    {
                        createUser.Add(new CreateGetUserDto
                        {
                            Name = user.Name,
                            UserId = user.UserId,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber,
                            UserType = user.UserType,
                            CreatedByUser = user.CreatedByUser,
                            AllowedSchedule = JsonConvert.DeserializeObject<List<string>>(user.AllowedSchedule),
                            EntityId = user.EntityId
                        });
                    }
                    return createUser;

                }

                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
