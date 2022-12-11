using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myCourse.Models.Exceptions
{
    public class CourseNotFoundException : Exception
    {
        public CourseNotFoundException(int courseId) : base($"Course {courseId} not found")
        {
        }
    }
}