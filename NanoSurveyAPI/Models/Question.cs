using System;
using System.Collections.Generic;

#nullable disable

namespace NanoSurveyAPI.Models
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            Results = new HashSet<Result>();
            SurveyQuestions = new HashSet<SurveyQuestion>();
        }

        public int Id { get; set; }
        public string QuestionText { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Result> Results { get; set; }
        public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; }
    }
}
