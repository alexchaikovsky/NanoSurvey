using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace NanoSurveyAPI.Models
{
    public partial class NanoSurveyContext : DbContext
    {
        public NanoSurveyContext()
        {
        }

        public NanoSurveyContext(DbContextOptions<NanoSurveyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Interview> Interviews { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<Survey> Surveys { get; set; }
        public virtual DbSet<SurveyQuestion> SurveyQuestions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.utf8");

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answer");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AnswerText)
                    .IsRequired()
                    .HasColumnName("answer_text");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("question_id");
            });

            modelBuilder.Entity<Interview>(entity =>
            {
                entity.ToTable("Interview");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.SurveyId).HasColumnName("survey_id");

                entity.Property(e => e.UserInfo)
                    .IsRequired()
                    .HasColumnName("user_info");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.Interviews)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("survey_id");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.QuestionText)
                    .IsRequired()
                    .HasColumnName("question_text");
            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.ToTable("Result");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AnswerId).HasColumnName("answer_id");

                entity.Property(e => e.InterviewId).HasColumnName("interview_id");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.AnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("answer_id");

                entity.HasOne(d => d.Interview)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.InterviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("interview_id");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("question_id");
            });

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.ToTable("Survey");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Description).HasColumnName("description");
            });

            modelBuilder.Entity<SurveyQuestion>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.Property(e => e.SurveyId).HasColumnName("survey_id");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.SurveyQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("question_id");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.SurveyQuestions)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("survey_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
