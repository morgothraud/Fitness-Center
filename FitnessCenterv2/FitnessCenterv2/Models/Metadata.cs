using System;
using System.ComponentModel.DataAnnotations;


namespace FitnessCenterv2.Models
{
 
   /* public class CustomerMeta
    {
        [Required, Display(Name = "First Name")]
        public string FirstName;
        [Required, Display(Name = "Last Name")]
        public string LastName;
        [Display(Name = "Credit Card Number")]
        public string CreditCardNumber;
        [Required, EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EMail;
        [Required]
        public string Password;
        [Display(Name = "Birth Date"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> BirthDate;

    }

    public class EquipmentMeta
    {
        public int ID;
        public string Name;
        [Range(0, double.MaxValue)]
        public Nullable<int> Quantity;
        [Required, Display(Name = "Is Avaliable")]
        public Nullable<bool> IsAvaliable;
        [Required, Display(Name = "Unit Price"), Range(0, double.MaxValue)]
        public Nullable<int> UnitPrice;
    }

    public class GuestListMeta
    {
        public int GuestID;
        public Nullable<int> CustomerID;
        [Required, Display(Name = "Guest Name")]
        public string GuestName;
        [Display(Name = "Guest Phone")]
        public string GuestPhone;
    }

    public class ManagerMeta
    {
        public int ID;
        [Required, Display(Name = "First Name")]
        public string FirstName;
        [Required, Display(Name = "Last Name")]
        public string LastName;
        [Display(Name = "Birth Date"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> BirthDate;
        public string Address;
        public string City;
        public string Phone;
        [Required]
        public string EMail;
        public Nullable<bool> Gender;
        public Nullable<decimal> Salary;
        [Required]
        public string Password;
    }

    public class PassResetMeta
    {
        public int ID;
        public Nullable<int> UserID;
        public string EMail;
        public string AutID;
        public Nullable<bool> isAvaliable;
        public string oldPass;
 
    }

    public class ReportMeta
    {
        [Display(Name = "Sender")]
        public int SenderID;
        public int ReceiverID;
        public string Subject;
        public string Body;
        [Display(Name = "Send Date")]
        public Nullable<System.DateTime> SendDate;
        public int ID;

        public virtual Staff Staff
        {
            get;
            set;
        }
    }

    public class StaffMeta
    {
        public int ID;
        [Display(Name = "First Name")]
        public string FirstName;
        [Display(Name = "Last Name")]
        public string LastName;
        public string Title;
        [Display(Name = "Birth Date"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> BirthDate;
        public string Address;
        public string Phone;
        [Required]
        public string EMail;
        public Nullable<bool> Gender;
        [Display(Name = "Hire Date"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> HireDate;
        [Range(0, Double.MaxValue)]
        public Nullable<decimal> Salary;
        [Required]
        public string Password;

 
    }

    public class TrainerMeta
    {
        public int ID;
        [Display(Name = "First Name")]
        public string FirstName;
        [Display(Name = "Last Name")]
        public string LastName;
        public string Address;
        public string Phone;
        [Required]
        public string EMail;
        [Display(Name = "Birth Date"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> BirthDate;
        public Nullable<bool> Gender;
        public Nullable<System.DateTime> HireDate;
        public Nullable<decimal> Salary;
        [Required]
        public string Password;

    
    }

    public class TrainerCustomerATableMeta
    {
        public Nullable<int> TrainerID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public string ID { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Trainer Trainer { get; set; }
    }

    public class TrainerScheduleMeta
    {
        public int ID { get; set; }
        public int TrainerID { get; set; }
        public Nullable<System.DateTime> Time { get; set; }
        public Nullable<int> Type { get; set; }

        public virtual Trainer Trainer { get; set; }
        public virtual TypeTable TypeTable { get; set; }
    }

    public class TypeTableMeta
    {
        public int ID { get; set; }
        public string Type { get; set; }

      
    }

    public class UserMeta
    {
        public int UserID { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required, EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EMail { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; }

     
    }

    public class WorkoutProgramMeta
    {
        public int ID { get; set; }
        public string Schedule { get; set; }
        public Nullable<int> TrainerID { get; set; }

        public virtual Trainer Trainer { get; set; }
    }*/
 
}
