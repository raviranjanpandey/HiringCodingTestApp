using AutoMapper;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.CreateUser
{
    public class TotalGetUserFilterCommand : IRequest<int>
    {
        public string UserId { get; set; }
        public bool IsAll { get; set; }
        public string Serachvalue { get; set; }

        public TotalGetUserFilterCommand(string userId, bool isAll, string serachvalue)
        {
            UserId = userId;
            IsAll = isAll;
            Serachvalue = serachvalue;
        }
        public class TotalGetUserFilterCommandHandler : IRequestHandler<TotalGetUserFilterCommand, int>
        {
            private readonly ISqlConnectionFactory _connection;
            private readonly IMapper _mapper;
            public TotalGetUserFilterCommandHandler(ISqlConnectionFactory connection, IMapper mapper)
            {
                _connection = connection;
                _mapper = mapper;
            }
            public async Task<int> Handle(TotalGetUserFilterCommand request, CancellationToken cancellationToken)
            {
                using var connection = _connection.GetOpenConnection();

                string sql = $"select * from interview.admin_user_master_filter_count('{request.UserId}','{request.Serachvalue}','{request.IsAll}')";
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