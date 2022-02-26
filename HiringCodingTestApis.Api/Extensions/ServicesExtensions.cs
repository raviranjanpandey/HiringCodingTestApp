using HiringCodingTestApis.Core;
using HiringCodingTestApis.Core.ExamsMaster;
using HiringCodingTestApis.Core.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HiringCodingTestApis.Api.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigServices(this IServiceCollection
             services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(ExamMasterCreate).Assembly);
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddIdentityServices(configuration);
            services.AddScoped<UserService>();
            services.AddScoped<QuestionMasterService>();
            services.AddScoped<ExamMasterService>();
            services.AddScoped<ExamGroupService>();
            services.AddScoped<AnswerService>();
            services.AddScoped<ExamResultService>();
            services.AddScoped<ExamScheduleService>();
            services.AddScoped<SchExamDetService>();
            services.AddScoped<CreateUserService>();
            services.AddScoped<AssignedUserService>();
            return services;
        }
    }
}
