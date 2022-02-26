using AutoMapper;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HiringCodingTestApis.Core.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.CreateUser
{
    public class TotalCreateUserCommand : IRequest<int>
    {
        public string UserId { get; set; }
        public bool IsAll { get; set; }

        public TotalCreateUserCommand(string userId, bool isAll)
        {
            UserId = userId;
            IsAll = isAll;
        }
        public class TotalCountQueryHandler : IRequestHandler<TotalCreateUserCommand, int>
        {
            private readonly ISqlConnectionFactory _connection;
            private readonly IMapper _mapper;
            public TotalCountQueryHandler(ISqlConnectionFactory connection, IMapper mapper)
            {
                _connection = connection;
                _mapper = mapper;
            }
            public async Task<int> Handle(TotalCreateUserCommand request, CancellationToken cancellationToken)
            {
                using var connection = _connection.GetOpenConnection();

                string sql = $"select * from interview.admin_user_master_count('{request.UserId}','{request.IsAll}')";

                try
                {
                    var result = await connection.QueryAsync<int>(sql);
                    return result == null ? 0 : Convert.ToInt32(result.AsList()[0]);
                }
                catch (Exception e)
                {
                    throw e;
                }

            }

        }
    }
}
