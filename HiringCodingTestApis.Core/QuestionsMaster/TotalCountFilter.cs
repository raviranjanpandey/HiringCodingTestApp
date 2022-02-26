using AutoMapper;
using Dapper;
using MediatR;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.QuestionsMaster
{
   public class TotalCountFilter : IRequest<int>
    {
        public int ExamId { get; set; }
        public string Serachvalue { get; set; }
        public TotalCountFilter(string serachvalue, int examId)
        {
            Serachvalue = serachvalue;
            ExamId = examId;
        }
    }

    public class TotalCountFilterHandler : IRequestHandler<TotalCountFilter, int>
    {
        private readonly ISqlConnectionFactory _connection;
        private readonly IMapper _mapper;
        public TotalCountFilterHandler(ISqlConnectionFactory connection, IMapper mapper)
        {
            _connection = connection;
            _mapper = mapper;
        }
        public async Task<int> Handle(TotalCountFilter request, CancellationToken cancellationToken)
        {
            using var connection = _connection.GetOpenConnection();

            String sql = $"select count(que_id) from interview.question_master where exam_id='{request.ExamId}'and question ILIKE '%{request.Serachvalue}%'";

            var result = await connection.QueryAsync<int>(sql.ToString());
            return result == null ? 0 : Convert.ToInt32(result.AsList()[0]);
        }

    }
}
