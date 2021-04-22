using System;
using System.Collections.Generic;

#nullable disable

namespace NanoSurveyAPI.Models
{
    public partial class Answer
    {
        public Answer()
        {
            Results = new HashSet<Result>();
        }

        public int Id { get; set; }
        public string AnswerText { get; set; }
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
        public virtual ICollection<Result> Results { get; set; }
    }
}
