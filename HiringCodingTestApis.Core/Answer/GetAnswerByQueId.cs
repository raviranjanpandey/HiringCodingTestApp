using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.Answer
{
    public class GetAnswerByQueId : IRequest<AnswerByQueListDto>
    {
        public int? ExamId { get; set; }
        public string UserId { get; set; }
        public int? ScheduleId { get; set; }
    }



    public class GetAnswerByQueIdHandler : IRequestHandler<GetAnswerByQueId, AnswerByQueListDto>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;

        public GetAnswerByQueIdHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }

        public async Task<AnswerByQueListDto> Handle(GetAnswerByQueId request, CancellationToken cancellationToken)
        {
             List<AnswerByQueDto> answers = new List<AnswerByQueDto>();
            var answersByExam = await _interviewContext.Answers.Include(x => x.Que).Include(x => x.User).Where(x => x.UserId == request.UserId && x.ScheduleId == request.ScheduleId && x.ExamId == request.ExamId).ToListAsync();
            //var result = _mapper.Map<AnswerByQueDto>(answersByExam);

            foreach (var user in answersByExam)
            {
                answers.Add(new AnswerByQueDto
                {
                    Question = user.Que.Question,
                    Option1 = user.Que.Option1,
                    Option2 = user.Que.Option2,
                    Option3 = user.Que.Option3,
                    Option4 = user.Que.Option4,
                    CorrectOption = user.Que.CorrectOption,
                    AnsOption = user.AnsOption,


                });
            }
            return new AnswerByQueListDto
            {
                AnswerByQueList = answers
            }; 

        }
    }
}
