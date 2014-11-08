using MotivateMe.Data.Models;
using MotivateMe.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotivateMe.Web.ViewModels.Home
{
    public class IndexStoryViewModel: IMapFrom<Story>
    {
        public string Title { get; set; }

        public string Content { get; set; }
    }
}