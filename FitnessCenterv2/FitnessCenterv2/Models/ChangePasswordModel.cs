using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessCenterv2.Models
{
    public class ChangePasswordModel
    {
        public User user { get; set; }
        public PassReset passReset { get; set; }
    }
}