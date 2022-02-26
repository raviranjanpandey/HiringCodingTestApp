using HiringCodingTestApis.Core.Constants;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HiringCodingTestApis.Core.Models
{
    public partial class InterviewContext : IdentityDbContext<AspNetUsers>
    {
        public InterviewContext()
        {
        }

        public InterviewContext(DbContextOptions<InterviewContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answers> Answers { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AssignedUsers> AssignedUsers { get; set; }
        public virtual DbSet<ExamGroup> ExamGroup { get; set; }
        public virtual DbSet<ExamMaster> ExamMaster { get; set; }
        public virtual DbSet<ExamSchedule> ExamSchedule { get; set; }
        public virtual DbSet<QuestionMaster> QuestionMaster { get; set; }
        public virtual DbSet<RefreshToken> RefreshToken { get; set; }
        public virtual DbSet<Results> Results { get; set; }
        public virtual DbSet<Scheduledexamdetails> Scheduledexamdetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder); //this is required
            optionsBuilder.UseNpgsql(ConnectionManager.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); //this is required
            modelBuilder.Entity<Answers>(entity =>
            {
                entity.HasKey(e => e.AnsId)
                    .HasName("pk_ans_id");

                entity.ToTable("answers", "interview");

                entity.Property(e => e.AnsId)
                    .HasColumnName("ans_id")
                    .HasDefaultValueSql("nextval('interview.ans_id_seq'::regclass)");

                entity.Property(e => e.AnsOption).HasColumnName("ans_option");

                entity.Property(e => e.Answer)
                    .HasColumnName("answer")
                    .HasColumnType("character varying");

                entity.Property(e => e.ExamId).HasColumnName("exam_id");

                entity.Property(e => e.Flag).HasColumnName("flag");

                entity.Property(e => e.QueId).HasColumnName("que_id");

                entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.ExamId)
                    .HasConstraintName("fk_ans_exam_id");

                entity.HasOne(d => d.Que)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QueId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("constraint_fk_queid");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.ScheduleId)
                    .HasConstraintName("fk_answers_schedule_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_ans_user_id");
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.ToTable("AspNetRoleClaims", "interview");

                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.ToTable("AspNetRoles", "interview");

                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.ToTable("AspNetUserClaims", "interview");

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.ToTable("AspNetUserLogins", "interview");

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.ToTable("AspNetUserRoles", "interview");

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.ToTable("AspNetUserTokens", "interview");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.ToTable("AspNetUsers", "interview");

                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.AllowedSchedule)
                    .HasColumnName("allowed_schedule")
                    .HasColumnType("json");

                entity.Property(e => e.CreatedByUser).HasColumnName("created_by_user");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.EntityId).HasColumnName("entity_id");

                entity.Property(e => e.LockoutEnd).HasColumnType("timestamp with time zone");

                entity.Property(e => e.Name).HasMaxLength(35);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AssignedUsers>(entity =>
            {
                entity.HasKey(e => e.AssignedUserId)
                    .HasName("pk_assigned_user_id");

                entity.ToTable("assigned_users", "interview");

                entity.Property(e => e.AssignedUserId)
                    .HasColumnName("assigned_user_id")
                    .HasDefaultValueSql("nextval('interview.assigned_user_id_seq'::regclass)");

                entity.Property(e => e.CreatedByUser).HasColumnName("created_by_user");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AssignedUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_assigned_user_userid");
            });

            modelBuilder.Entity<ExamGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId)
                    .HasName("pk_ex_grp_id");

                entity.ToTable("exam_group", "interview");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasDefaultValueSql("nextval('interview.ex_grp_id_seq'::regclass)");

                entity.Property(e => e.ExamIdJson)
                    .HasColumnName("exam_id_json")
                    .HasColumnType("json");

                entity.Property(e => e.GroupName)
                    .HasColumnName("group_name")
                    .HasMaxLength(100);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ExamGroup)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_eg_user_id");
            });

            modelBuilder.Entity<ExamMaster>(entity =>
            {
                entity.HasKey(e => e.ExamId)
                    .HasName("pk_exam_id");

                entity.ToTable("exam_master", "interview");

                entity.Property(e => e.ExamId)
                    .HasColumnName("exam_id")
                    .HasDefaultValueSql("nextval('interview.exam_id_seq'::regclass)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(255);

                entity.Property(e => e.ExamName)
                    .HasColumnName("exam_name")
                    .HasMaxLength(35);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ExamMaster)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_em_user_id");
            });

            modelBuilder.Entity<ExamSchedule>(entity =>
            {
                entity.HasKey(e => e.ScheduleId)
                    .HasName("pk_schedule_id");

                entity.ToTable("exam_schedule", "interview");

                entity.Property(e => e.ScheduleId)
                    .HasColumnName("schedule_id")
                    .HasDefaultValueSql("nextval('interview.schedule_id_seq'::regclass)");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.EndDate).HasColumnName("end_date");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.Isanswer).HasColumnName("isanswer");

                entity.Property(e => e.Isquestimer).HasColumnName("isquestimer");

                entity.Property(e => e.Isresult).HasColumnName("isresult");

                entity.Property(e => e.NumOfQuestions).HasColumnName("num_of_questions");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.TestDuration).HasColumnName("test_duration");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.ExamSchedule)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("fk_es_group_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ExamSchedule)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_es_user_id");
            });          

            modelBuilder.Entity<QuestionMaster>(entity =>
            {
                entity.HasKey(e => e.QueId)
                    .HasName("pk_que_id");

                entity.ToTable("question_master", "interview");

                entity.Property(e => e.QueId)
                    .HasColumnName("que_id")
                    .HasDefaultValueSql("nextval('interview.que_id_seq'::regclass)");

                entity.Property(e => e.AnswerType).HasColumnName("answer_type");

                entity.Property(e => e.CorrectOption).HasColumnName("correct_option");

                entity.Property(e => e.ExamId).HasColumnName("exam_id");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.Option1)
                    .HasColumnName("option1")
                    .HasColumnType("character varying");

                entity.Property(e => e.Option2)
                    .HasColumnName("option2")
                    .HasColumnType("character varying");

                entity.Property(e => e.Option3)
                    .HasColumnName("option3")
                    .HasColumnType("character varying");

                entity.Property(e => e.Option4)
                    .HasColumnName("option4")
                    .HasColumnType("character varying");

                entity.Property(e => e.Question)
                    .HasColumnName("question")
                    .HasMaxLength(500);

                entity.Property(e => e.Seconds).HasColumnName("seconds");

                entity.Property(e => e.SubModule)
                    .HasColumnName("sub_module")
                    .HasMaxLength(25);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.QuestionMaster)
                    .HasForeignKey(d => d.ExamId)
                    .HasConstraintName("fk_qm_exam_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.QuestionMaster)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_qm_user_id");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshToken", "interview");

                entity.HasIndex(e => e.AppUserId);

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.RefreshToken)
                    .HasForeignKey(d => d.AppUserId);
            });

            modelBuilder.Entity<Results>(entity =>
            {
                entity.HasKey(e => e.ResId)
                    .HasName("pk_res_id");

                entity.ToTable("results", "interview");

                entity.Property(e => e.ResId)
                    .HasColumnName("res_id")
                    .HasDefaultValueSql("nextval('interview.res_id_seq'::regclass)");

                entity.Property(e => e.CorrectAnswer).HasColumnName("correct_answer");

                entity.Property(e => e.ExamId).HasColumnName("exam_id");

                entity.Property(e => e.FinalResult).HasColumnName("final_result");

                entity.Property(e => e.Flag).HasColumnName("flag");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

                entity.Property(e => e.SkippedAnswer).HasColumnName("skipped_answer");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.WrongAnswer).HasColumnName("wrong_answer");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.ExamId)
                    .HasConstraintName("fk_res_exam_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("fk_qm_group_id");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.ScheduleId)
                    .HasConstraintName("fk_schedule_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_res_user_id");
            });

            modelBuilder.Entity<Scheduledexamdetails>(entity =>
            {
                entity.HasKey(e => e.SedId)
                    .HasName("pk_sed_id");

                entity.ToTable("scheduledexamdetails", "interview");

                entity.Property(e => e.SedId)
                    .HasColumnName("sed_id")
                    .HasDefaultValueSql("nextval('interview.sed_id_seq'::regclass)");

                entity.Property(e => e.ExamId).HasColumnName("exam_id");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.PassingMarksPercentage).HasColumnName("passing_marks_percentage");

                entity.Property(e => e.Questnos).HasColumnName("questnos");

                entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.Scheduledexamdetails)
                    .HasForeignKey(d => d.ExamId)
                    .HasConstraintName("fk_qm_exam_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Scheduledexamdetails)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("fk_qm_group_id");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.Scheduledexamdetails)
                    .HasForeignKey(d => d.ScheduleId)
                    .HasConstraintName("fk_sed_id");
            });

            modelBuilder.HasSequence("ans_id_seq", "interview");

            modelBuilder.HasSequence("assigned_user_id_seq", "interview");

            modelBuilder.HasSequence("errorlist_id", "interview");

            modelBuilder.HasSequence("ex_grp_id_seq", "interview");

            modelBuilder.HasSequence("exam_id_seq", "interview");

            modelBuilder.HasSequence("progress_bar_id_seq", "interview");

            modelBuilder.HasSequence("que_id_seq", "interview");

            modelBuilder.HasSequence("res_id_seq", "interview");

            modelBuilder.HasSequence("schedule_id_seq", "interview");

            modelBuilder.HasSequence("sed_id_seq", "interview").StartsAt(20);

            modelBuilder.HasSequence("user_id_seq", "interview");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
