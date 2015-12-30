using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitnessCenterv2.Models
{
    public class CustomDateRangeAttribute : RangeAttribute
    {
        public CustomDateRangeAttribute()
            : base(typeof(DateTime), DateTime.Now.ToString(), DateTime.Now.AddYears(20).ToString())
        { }
    }
}