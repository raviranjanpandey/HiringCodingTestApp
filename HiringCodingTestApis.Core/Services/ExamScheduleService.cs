using MediatR;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.ExamSchedules;
using HiringCodingTestApis.Core.ScheduledExamDetail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.Services
{
    public class ExamScheduleService
    {
        private readonly IMediator _mediator;
        public ExamScheduleService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<int> Create(ExamScheduleCreate create)
        {
            var scheduleId = await _mediator.Send(create);
            if (scheduleId > 0)
            {
                foreach (var item in create.ExamDetails)
                {
                    item.ScheduleId = scheduleId;
                }
                if (await _mediator.Send(new SchExamDetCreate { ExamDetails = create.ExamDetails }))
                {
                    return scheduleId;
                }
            }
            return 0;
        }

        public async Task<int> Update(ExamScheduleUpdate update)
        {
            return await _mediator.Send(update);
        }

        public async Task<bool> Delete(ExamScheduleDelete delete)
        {
            var examdetdelete = await _mediator.Send(new ExamScheduleDetDeleteByScheduleId { ScheduleId = delete.ScheduleId });
            if (examdetdelete)
            {
                return await _mediator.Send(delete);
            }
            return false;
        }

        public async Task<ExamDetScheduleList> Get(ExamScheduleGet get)
        {
            return await _mediator.Send(get);
        }

        public async Task<ScheduleExamDetails> GetByScheduleId(GetExamDetailsByScheduleId get)
        {
            return await _mediator.Send(get);
        }

        public async Task<ExamScheduleList> GetByExamGroup(ExamScheduleGetByExamGroup get)
        {
            return await _mediator.Send(get);
        }
        public async Task<IEnumerable<ExamDetScheduleDto>> GetExamScheduledUserId(GetExamScheduleByUserId get)
        {
            return await _mediator.Send(get);
        }
    }
}
