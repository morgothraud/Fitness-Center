//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FitnessCenterv2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class GuestList
    {
        public int GuestID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        [Required,Display(Name="Guest Name")]
        public string GuestName { get; set; }
        [Display(Name="Guest Phone")]
        public string GuestPhone { get; set; }
    }
}