using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BUDLP.Models.TopicModels
{
    public enum TopicModuleLanguage
    {
        Concept,
        C,
        CPP,
        MATLAB
    }

    public class TopicModule
    {
        public int TopicModuleId { get; set; }
        public int TopicId { get; set; }
        public string TopicModuleTitle { get; set; }
        public string NextTopicModuleUrl { get; set; }
        public string PrevTopicModuleUrl { get; set; }
        public List<TopicModuleContent> TopicModuleContent { get; set; }
        public TopicModuleLanguage Language { get; set; }
    }
}
