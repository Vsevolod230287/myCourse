﻿using System;
using System.Collections.Generic;
using myCourse.Models.ValueTypes;

namespace myCourse.Models.Entities
{
   public partial class Course
   {
      public Course(string title, string author)
      {
         if (string.IsNullOrWhiteSpace(title))
         {
            throw new ArgumentException("The course must have a Title");
         }
         if (string.IsNullOrWhiteSpace(author))
         {
            throw new ArgumentException("The course must have a Author");
         }
         Title = title;
         Author = author;
         Lessons = new HashSet<Lesson>();
      }

      public long Id { get; private set; }
      public string Title { get; private set; }
      public string Description { get; private set; }
      public string ImagePath { get; private set; }
      public string Author { get; private set; }
      public string Email { get; private set; }
      public double Rating { get; private set; }
      public Money FullPrice { get; private set; }
      public Money CurrentPrice { get; private set; }
      public virtual ICollection<Lesson> Lessons { get; private set; }


      public void ChangeTitle(string newTitle)
      {
         if (string.IsNullOrWhiteSpace(newTitle))
         {

            throw new ArgumentException("The course must heve a Title");
         }
      }
      public void ChangePrice(Money newFullPrice, Money newDiscountPrice)
      {
         if (newFullPrice == null || newDiscountPrice == null)
         {
            throw new ArgumentException("Prices cannot be null");
         }

         if (newFullPrice.Currency != newDiscountPrice.Currency)
         {
            throw new ArgumentException("Currencies don't match");
         }
         if (newFullPrice.Amount < newDiscountPrice.Amount)
         {
            throw new ArgumentException("Full price can't be less than the current price");
         }
         FullPrice = newDiscountPrice;
         CurrentPrice = newDiscountPrice;
      }
   }
}