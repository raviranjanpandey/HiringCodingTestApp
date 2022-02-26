using AutoMapper;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ExamsMaster
{
    public class ExamMasterGet : IRequest<IEnumerable<ExamMasterDto>>
    {
        public string UserId { get; set; }
    }

    public class ExamMasterGetHandler : IRequestHandler<ExamMasterGet, IEnumerable<ExamMasterDto>>
    {
        private readonly ISqlConnectionFactory _connection;
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;

        public ExamMasterGetHandler(ISqlConnectionFactory connection, InterviewContext interviewContext, IMapper mapper)
        {
            _connection = connection;
            _interviewContext = interviewContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ExamMasterDto>> Handle(ExamMasterGet request, CancellationToken cancellationToken)
        {
            using var connection = _connection.GetOpenConnection();

            string sql = $"select * from interview.admin_exam_master('{request.UserId}')";

            try
            {
                return await connection.QueryAsync<ExamMasterDto>(sql);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
