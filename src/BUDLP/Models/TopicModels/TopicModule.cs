using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BUDLP.Models.TopicModels
{
    public class TopicModule
    {
        public int TopicModuleId { get; set; }
        public int TopicId { get; set; }
        public string TopicModuleTitle { get; set; }
        public List<TopicModuleContent> TopicModules { get; set; }
    }
}
