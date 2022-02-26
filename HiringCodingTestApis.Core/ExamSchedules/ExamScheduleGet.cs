using MediatR;
using Microsoft.EntityFrameworkCore;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ExamSchedules
{
    public class ExamScheduleGet : IRequest<ExamDetScheduleList>
    {
    }

    public class ExamScheduleGetHandler : IRequestHandler<ExamScheduleGet, ExamDetScheduleList>
    {
        private readonly InterviewContext _interviewContext;
        public ExamScheduleGetHandler(InterviewContext interviewContext)
        {
            _interviewContext = interviewContext;
        }

        public async Task<ExamDetScheduleList> Handle(ExamScheduleGet request, CancellationToken cancellationToken)
        {
            var existing = await _interviewContext.ExamSchedule.Include(x => x.Group).Include(x => x.Scheduledexamdetails).ToListAsync();
            var exams = await _interviewContext.ExamMaster.ToListAsync();

            if (existing == null) return new ExamDetScheduleList();
            List<ExamDetScheduleDto> list = (from detail in existing
                                             select new ExamDetScheduleDto
                                             {
                                                 ScheduleId = detail.ScheduleId,
                                                 Active = detail.Active,
                                                 EndDate = detail.EndDate,
                                                 GroupId = detail.GroupId,
                                                 GroupName = detail.Group.GroupName,
                                                 StartDate = detail.StartDate,
                                                 NumOfQuestions = detail.NumOfQuestions,
                                                 UserId=detail.UserId,
                                                 TestDuration = detail.TestDuration,
                                                 Isresult = detail.Isresult,
                                                 Isquestimer = detail.Isquestimer,
                                                 Isanswer = detail.Isanswer,
                                                 ExamDetails = (from det in detail.Scheduledexamdetails
                                                                join emdet in exams on det.ExamId equals emdet.ExamId
                                                                select new SchDetDto
                                                                {
                                                                    ExamId = det.ExamId,
                                                                    GroupId = det.GroupId,
                                                                    PassingMarksPercentage = det.PassingMarksPercentage,
                                                                    Questnos = det.Questnos,
                                                                    ScheduleId = det.ScheduleId,
                                                                    SedId = det.SedId,
                                                                    ExamName = emdet.ExamName
                                                                }
                                                                ).ToList()
                                             }).ToList();
            return new ExamDetScheduleList
            {
                ExamSchedules = list
            };
        }
    }
}
