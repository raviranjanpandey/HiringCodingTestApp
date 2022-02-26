using AutoMapper;
using Dapper;
using MediatR;
using HiringCodingTestApis.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.QuestionsMaster
{
    public class QuestionMasterFilter : Query<QuestionMasterList>
    {
        public int ExamId { get; set; }
        public string Serachvalue { get; set; }

        public QuestionMasterFilter(int examId, string serachvalue, int? take, int? skip) : base(take, skip)
        {
            ExamId = examId;
            Serachvalue = serachvalue;
        }



        public class QuestionMasterFilterHandler : IRequestHandler<QuestionMasterFilter, QuestionMasterList>
        {
            private readonly ISqlConnectionFactory _connection;
            private readonly IMapper _mapper;
            public QuestionMasterFilterHandler(ISqlConnectionFactory connection, IMapper mapper)
            {
                _connection = connection;
                _mapper = mapper;
            }
            public async Task<QuestionMasterList> Handle(QuestionMasterFilter request, CancellationToken cancellationToken)
            {

                using var connection = _connection.GetOpenConnection();

                String sql = $"select * from interview.question_master where exam_id='{request.ExamId}'and question ILIKE '%{request.Serachvalue}%'  limit {request.Take}  offset {request.Skip} ";
                List<QuestionMastersDto> answers = new List<QuestionMastersDto>();
                var ret = await connection.QueryAsync<QuestionMastersDto>(sql);
                //var result = _mapper.Map<AnswerByQueDto>(answersByExam);

                foreach (var user in ret)
                {
                    answers.Add(new QuestionMastersDto


                    {
                        SubModule = user.SubModule,
                        UserId = user.UserId,
                        Question = user.Question,
                        AnswerType = user.AnswerType,
                        Option1 = user.Option1,
                        Option2 = user.Option2,
                        Option3 = user.Option3,
                        Option4 = user.Option4,
                        CorrectOption = user.CorrectOption,
                        Seconds = user.Seconds,
                        QueId = user.QueId,
                        ExamId=user.ExamId
                    });
                }
                return new QuestionMasterList
                {
                    QuestionsList = answers
                };
            }
        }
    }
}
                