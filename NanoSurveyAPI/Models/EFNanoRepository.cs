using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NanoSurveyAPI.Models
{
    public class EFNanoRepository : INanoRepository
    {
        private readonly NanoSurveyContext context;
        public EFNanoRepository(NanoSurveyContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Question> Questions => context.Questions;
        public IQueryable<Interview> Interviews => context.Interviews;
        public IQueryable<SurveyQuestion> SurveyQuestions => context.SurveyQuestions;

        public async Task<string> AddNewInterview(int surveyId)
        {
            string userInfo = Guid.NewGuid().ToString();
            await context.Interviews.AddAsync(new Interview {UserInfo = userInfo, SurveyId = surveyId});
            await context.SaveChangesAsync();
            return userInfo;
        }

        public Task <SurveyQuestion> GetQuestion(int surveyId, int questionId)
        {

            return context.SurveyQuestions
                .Where(x => x.SurveyId == surveyId)
                .Include(x => x.Question)
                .ThenInclude(x => x.Answers)
                .SingleOrDefaultAsync(x => x.QuestionId == questionId);
        }
        public async Task<int?> GetInterviewId(string sessionIdentifier)
        {
            if (sessionIdentifier is null)
            {
                return null;
            }
            return (await context.Interviews.FirstOrDefaultAsync(x => x.UserInfo == sessionIdentifier))?.Id;
        }
        public Task SaveResult(int questionId, List<int> answers, int interviewId)
        {
            
            foreach (var answer in answers)
            {
                Result newResult = new Result { AnswerId = answer, InterviewId = interviewId, QuestionId = questionId };
                context.Results.Add(newResult);
            }
            return context.SaveChangesAsync();
        }

        public async Task<int?> GetNextQuestionId(int surveyId, int previousQuestionId)
        {
            return (await context.SurveyQuestions
                .Where(x => x.SurveyId == surveyId)
                .Where(x => x.Id > previousQuestionId)
                .FirstOrDefaultAsync())?.QuestionId;
        }
    }
}
