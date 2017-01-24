using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BUDLP.Models.TopicQuizzes
{
    public class UserQuizResponse
    {
        public int UserQuizResponseId { get; set; }

        // FK to Quiz Instance
        public int QuizId { get; set; }
        public string Response { get; set; }
        public string UserProfileId { get; set; }
        public bool Correct { get; set; }
        public DateTime DateSubmitted { get; set; }
    }
}
