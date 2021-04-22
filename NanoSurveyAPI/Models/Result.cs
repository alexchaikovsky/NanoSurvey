using System;
using System.Collections.Generic;

#nullable disable

namespace NanoSurveyAPI.Models
{
    public partial class Result
    {
        public int Id { get; set; }
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public int InterviewId { get; set; }

        public virtual Answer Answer { get; set; }
        public virtual Interview Interview { get; set; }
        public virtual Question Question { get; set; }
    }
}
