using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BUDLP.Models
{

    public class TopicModuleState
    {
        public int TopicModuleStateId { get; set; }
        public string AuthenticatedUserId { get; set; }
        public int TopicModuleId { get; set; }
        public int TopicId { get; set; }
        public LearningState LearningState { get; set; }
        public DateTime? FirstVisited { get; set; }
        public DateTime? LastVisited { get; set; }
        public int TimeSpent { get; set; }
    }
}
