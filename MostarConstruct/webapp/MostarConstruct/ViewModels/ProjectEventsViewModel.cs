using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.ViewModels
{
    public class ProjectEventsViewModel
    {
        public List<ProjectEvent> Projects { get; set; }        

        public class ProjectEvent
        {
            public int Sr { get; set; }
            public string Title { get; set; }
            public string Desc { get; set; }
            public string Start_Date { get; set; }
            public string End_Date { get; set; }
            public string Color { get; set; }
        }
    }
}
