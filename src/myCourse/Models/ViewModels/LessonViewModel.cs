using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace myCourse.Models.ViewModels
{
    public class LessonViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //   public TimeSpan Duration { get; set; }

        internal static LessonViewModel FromDataRow(DataRow lessonRow)
        {
            var courseViewModel = new LessonViewModel
            {
                Title = Convert.ToString(lessonRow["Title"]),
                // Duration = TimeSpan.Parse(Convert.ToString(lessonRow["Duration"]))
            };
            return courseViewModel;
        }
    }
}