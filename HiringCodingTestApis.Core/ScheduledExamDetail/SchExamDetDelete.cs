using MediatR;
using HiringCodingTestApis.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ScheduledExamDetail
{
    public class SchExamDetDelete : IRequest<bool>
    {
        public int sedid { get; set; }
    }

    public class SchExamDetDeleteHandler : IRequestHandler<SchExamDetDelete, bool>
    {
        private readonly InterviewContext _interviewContext;

        public SchExamDetDeleteHandler(InterviewContext interviewContext)
        {
            _interviewContext = interviewContext;
        }

        public async Task<bool> Handle(SchExamDetDelete request, CancellationToken cancellationToken)
        {
            var scheduledexamdetails = await _interviewContext.Scheduledexamdetails
                             .FindAsync(request.sedid);

            if (scheduledexamdetails != null)
            {
                _interviewContext.Scheduledexamdetails.Remove(scheduledexamdetails);
            }

            return await _interviewContext.SaveChangesAsync() > 0;
        }
    }
}
