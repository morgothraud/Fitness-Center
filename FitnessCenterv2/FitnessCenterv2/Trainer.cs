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
    
    public partial class Trainer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Trainer()
        {
<<<<<<< HEAD
            this.TrainerCustomerATables = new HashSet<TrainerCustomerATable>();
=======
>>>>>>> master
            this.TrainerSchedules = new HashSet<TrainerSchedule>();
            this.WorkoutPrograms = new HashSet<WorkoutProgram>();
        }
    
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string EMail { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<bool> Gender { get; set; }
        public Nullable<System.DateTime> HireDate { get; set; }
        public Nullable<decimal> Salary { get; set; }
        public string Password { get; set; }
    
<<<<<<< HEAD
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrainerCustomerATable> TrainerCustomerATables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrainerSchedule> TrainerSchedules { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
=======
        public virtual ICollection<TrainerSchedule> TrainerSchedules { get; set; }
>>>>>>> master
        public virtual ICollection<WorkoutProgram> WorkoutPrograms { get; set; }
    }
}
