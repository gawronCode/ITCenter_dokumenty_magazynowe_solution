using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITCenter_dokumenty_magazynowe.Models.ApiModel
{
    public class GitHubRepoData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreationDate { get; set; }
        public string SubscribersCount { get; set; }
        public string OwnerName { get; set; }


    }
}
