using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BUDLP.Models.TopicModels
{
    public class UserProfileTopic
    {
        public int UserProfileTopicId { get; set; }
        public int TopicId { get; set; }
        public string UserProfileId { get; set; }
        public bool ToLearn { get; set; }
    }
}
