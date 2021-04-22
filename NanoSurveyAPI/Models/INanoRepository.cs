using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NanoSurveyAPI.Models
{
    public interface INanoRepository
    {
        IQueryable<Question> Questions { get; }
        IQueryable <Interview> Interviews { get; }
        IQueryable<SurveyQuestion> SurveyQuestions { get; }
        Task SaveResult(int questionId, List<int> answers, int interviewId);
        Task<SurveyQuestion> GetQuestion(int surveyId, int id);
        Task<string> AddNewInterview(int surveyId);
        public Task<int?> GetInterviewId(string sessionIdentifier);
        public Task<int?> GetNextQuestionId(int surveyId, int previousQuestionId);
    }
}
