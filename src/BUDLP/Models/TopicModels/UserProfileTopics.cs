using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BUDLP.Models.TopicModels
{
    public enum UPTStatus
    {
        Untouched,
        Started,
        Completed
    }

    public class UserProfileTopic
    {
        public int UserProfileTopicId { get; set; }
        public int TopicId { get; set; }
        public string UserProfileId { get; set; }
        public bool ToLearn { get; set; }
        public UPTStatus Status { get; set; }
        public PriorLearned PastExperience { get; set; }
        public Topic Topic { get; set; }
        public string ListOrder { get; set; }
        [NotMapped]
        public string ProgressCount { get; set; }
    }
}
