using EduHome2.Models;
using System;
using System.Collections.Generic;

namespace EduHome2.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Service> Services { get; set; }
        public About Abouts { get; set; }
        public List<Course> Courses { get; set; }
        public FeedBack FeedBacks { get; set; }
    }
}
