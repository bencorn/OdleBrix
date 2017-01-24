using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BUDLP.Models.TopicModels
{
    public enum ContentModuleType
    {
        Video = 1,
        Text = 2,
        Slide = 3
    }

    public enum ContentModuleClass
    {
        Concept = 1,
        Implementation = 2
    }

    public enum PriorLearned
    {
        None = 0, // No Prior Learned Language
        C2M = 1, // C to MATLAB
        C2CPP = 2, // C to C++
        M2C = 3, // MATLAB to C
        M2CPP = 4, // MATLAB to C++
        CPP2M = 5, // C++ to MATLAB
        CPP2C = 6, // C++ to C
        MC2CPP = 7, // MATLAB and C to C++
        MCPP2C = 8, // MATLAB and C++ to C
        CCPP2M = 9, //C and C++ to MATLAB
    }

    public class TopicModuleContent
    {
        public int TopicModuleContentId { get; set; }
        public int TopicModuleContentType { get; set; }
        public string ModuleTitle { get; set; }
        public string ModuleContent { get; set; }
        public string MetaContent { get; set; }
        public string ModuleDescription { get; set; }
        public float ModuleTime { get; set; }
        public int TopicModuleId { get; set; }
        public int RelativeOrdering { get; set; }
        public int ContentGroup { get; set; }
        public List<UserLearningState> UserLearningState { get; set; }
        public bool Quiz { get; set; }
        public TopicModuleLanguage Language { get; set; }
        public ContentModuleClass Class { get; set; }
        public PriorLearned PriorLearned { get; set; }
        [JsonIgnore]
        public TopicModule TopicModule { get; set; }
    }
}
