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

namespace HiringCodingTestApis.Core.ExamGroups
{
    public class ExamGroupGet : IRequest<IEnumerable<ExamGroupDto>>
    {
        public string UserId { get; set; }
    }

    public class ExamGroupGetHandler : IRequestHandler<ExamGroupGet, IEnumerable<ExamGroupDto>>
    {
        private readonly InterviewContext _interviewContext;

        private readonly ISqlConnectionFactory _connection;

        public ExamGroupGetHandler(ISqlConnectionFactory connection, InterviewContext interviewContext)
        {
            _connection = connection;
            _interviewContext = interviewContext;

        }


        public async Task<IEnumerable<ExamGroupDto>> Handle(ExamGroupGet request, CancellationToken cancellationToken)
        {
            using var connection = _connection.GetOpenConnection();

            string sql = $"select * from interview.admin_exam_group('{request.UserId}')";

            try
            {
                List<ExamGroupDto> exam = new List<ExamGroupDto>();
                var ret = await connection.QueryAsync<ExamGroupByUserIdDto>(sql);
                foreach (var user in ret)
                {
                    exam.Add(new ExamGroupDto
                    {
                        UserId = user.UserId,
                        Name = user.Name,
                        GroupId = user.GroupId,                       
                        Createdbyuser = user.Createdbyuser,                       
                        Subjects = JsonConvert.DeserializeObject<List<ExamMasterDto>>(user.Subjects),

                    });
                }
                return exam;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}