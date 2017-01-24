using BUDLP.Models.TopicModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BUDLP.Models
{
    public enum LearningState
    {
        Untouched,
        Started,
        Attempted,
        Completed
    }

    public class UserLearningState
    {
        public int UserLearningStateId { get; set; }
        public string AuthenticatedUserId { get; set; }
        public int TopicModuleId { get; set; }
        public int TopicId { get; set; }
        public int TopicModuleContentId { get; set; }
        public LearningState LearningState { get; set; }
        public DateTime? FirstVisited { get; set; }
        public DateTime? LastVisited { get; set; }
        public int TimeSpent { get; set; }
    }
}
