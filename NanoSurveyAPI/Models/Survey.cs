using System;
using System.Collections.Generic;

#nullable disable

namespace NanoSurveyAPI.Models
{
    public partial class Survey
    {
        public Survey()
        {
            Interviews = new HashSet<Interview>();
            SurveyQuestions = new HashSet<SurveyQuestion>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Interview> Interviews { get; set; }
        public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; }
    }
}
