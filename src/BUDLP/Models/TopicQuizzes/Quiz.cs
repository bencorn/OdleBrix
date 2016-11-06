using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BUDLP.Models.TopicQuizzes
{
    public enum QuizType
    {
        Checkbox,
        Radio,
        FillIn
    }

    public class Quiz
    {
        public int QuizId { get; set; }
        public int TopicModuleContentId { get; set; }
        public QuizType QuizType { get; set; }
        public int QuizAnswerId { get; set; }
        public List<QuizOption> QuizOptions { get; set; }
    }
}
