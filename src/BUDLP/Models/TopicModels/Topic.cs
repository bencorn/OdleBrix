using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BUDLP.Models.TopicModels
{
    public enum Language
    {
        C,
        CPLUS,
        MATLAB
    }

    public class Topic
    {
        public int TopicId { get; set; }
        public int CourseId { get; set; }
        public int Language { get; set; }
        public string TopicName { get; set; }
        public string PreReq { get; set; }
    }
}
