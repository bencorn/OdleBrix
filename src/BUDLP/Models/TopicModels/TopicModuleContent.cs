using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BUDLP.Models.TopicModels
{
    public class TopicModuleContent
    {
        public int TopicModuleContentId { get; set; }
        public int TopicModuleContentType { get; set; }
        public string ModuleTitle { get; set; }
        public string ModuleContent { get; set; }
        public string ModuleDescription { get; set; }
        public float ModuleTime { get; set; }
        public int TopicModuleId { get; set; }
        public int? NextTopicModuleContentId { get; set; }
        public int PrevTopicModuleContentId { get; set; }
        public int QuizId { get; set; }
        [JsonIgnore]
        public TopicModule TopicModule { get; set; }
    }
}
