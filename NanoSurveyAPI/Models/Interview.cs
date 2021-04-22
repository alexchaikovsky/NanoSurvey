using System;
using System.Collections.Generic;

#nullable disable

namespace NanoSurveyAPI.Models
{
    public partial class Interview
    {
        public Interview()
        {
            Results = new HashSet<Result>();
        }

        public int Id { get; set; }
        public int SurveyId { get; set; }
        public string UserInfo { get; set; }

        public virtual Survey Survey { get; set; }
        public virtual ICollection<Result> Results { get; set; }
    }
}
