using Dapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ExamSchedules
{
    public class GetExamScheduleByUserId : IRequest<IEnumerable<ExamDetScheduleDto>>
    {
        public string UserId { get; set; }
    }

    public class GetExamScheduleByUserIdValidator : AbstractValidator<GetExamScheduleByUserId>
    {
        public GetExamScheduleByUserIdValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Please select valid User id to fetch.");
        }
    }

    public class GetExamScheduleByUserIdHandler : IRequestHandler<GetExamScheduleByUserId, IEnumerable<ExamDetScheduleDto>>
    {
        private readonly ISqlConnectionFactory _connection;
        private readonly InterviewContext _interviewContext;


        public GetExamScheduleByUserIdHandler(ISqlConnectionFactory connection, InterviewContext interviewContext)
        {
            _connection = connection;
            _interviewContext = interviewContext;
        }

        public async Task<IEnumerable<ExamDetScheduleDto>> Handle(GetExamScheduleByUserId request, CancellationToken cancellationToken)
        {

            using var connection = _connection.GetOpenConnection();

            string sql = $"select * from interview.admin_exam_schedule('{request.UserId}')";

            try
            {
                List<ExamDetScheduleDto> createUser = new List<ExamDetScheduleDto>();
                var ret = await connection.QueryAsync<ExamSheduleByuserIdDto>(sql);              
                foreach (var user in ret)
                {
                    createUser.Add(new ExamDetScheduleDto
                    {
                        ScheduleId = user.ScheduleId,
                        GroupId = user.GroupId,
                        StartDate = user.StartDate,
                        EndDate = user.EndDate,
                        NumOfQuestions = user.NumOfQuestions,
                        Active = user.Active,
                        GroupName = user.GroupName,
                        UserId = user.UserId,
                        TestDuration = user.TestDuration,
                        Isresult = user.Isresult,
                        Isanswer = user.Isanswer,
                        Isquestimer = user.Isquestimer,
                        Createdbyuser = user.Createdbyuser,
                        ExamDetails = JsonConvert.DeserializeObject<List<SchDetDto>>(user.ExamDetails),
                        
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

