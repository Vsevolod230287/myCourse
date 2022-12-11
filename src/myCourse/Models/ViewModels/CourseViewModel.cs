using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using myCourse.Models.Enums;
using myCourse.Models.ValueTypes;

namespace myCourse.Models.ViewModels
{
   public class CourseViewModel
   {
      public long Id { get; set; }
      public string Title { get; set; }
      public string ImagePath { get; set; }
      public string Author { get; set; }
      public double Rating { get; set; }
      public Money FullPrice { get; set; }
      public Money CurrentPrice { get; set; }

      public static CourseViewModel FromDataRow(DataRow courseRow)
      {
         var courseViewModel = new CourseViewModel
         {
            Title = Convert.ToString(courseRow["Title"]),
            ImagePath = Convert.ToString(courseRow["ImagePath"]),
            Author = Convert.ToString(courseRow["Author"]),
            Rating = Convert.ToDouble(courseRow["Rating"]),
            FullPrice = new Money(
               Enum.Parse<Currency>(Convert.ToString(courseRow["FullPrice_Currency"])),
               Convert.ToDecimal(courseRow["FullPrice_Amount"])
            ),
            CurrentPrice = new Money(
               Enum.Parse<Currency>(Convert.ToString(courseRow["CurrentPrice_Currency"])),
               Convert.ToDecimal(courseRow["CurrentPrice_Amount"])
            ),
            Id = Convert.ToInt32(courseRow["Id"])
         };
         return courseViewModel;
      }
   }
}