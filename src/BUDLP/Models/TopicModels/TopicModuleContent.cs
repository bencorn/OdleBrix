using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BUDLP.Models.TopicModels
{
    public class TopicModuleContent
    {
        public int TopicModuleContentId { get; set; }
        public int TopicModuleId { get; set; }
        public int TopicModuleContentType { get; set; }
        public string ModuleContent { get; set; }
    }
}
