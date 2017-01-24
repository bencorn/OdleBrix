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
        public string QuestionText { get; set; }
        public string QuizAnswer { get; set; }
        public string Remark { get; set; }
        public List<QuizOption> QuizOptions { get; set; }
    }
}
