﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FitnessCenterEntities : DbContext
    {
        public FitnessCenterEntities()
            : base("name=FitnessCenterEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Equipment> Equipments { get; set; }
        public virtual DbSet<GuestList> GuestLists { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<PassReset> PassResets { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Trainer> Trainers { get; set; }
<<<<<<< HEAD
        public virtual DbSet<TrainerCustomerATable> TrainerCustomerATables { get; set; }
        public virtual DbSet<TrainerSchedule> TrainerSchedules { get; set; }
        public virtual DbSet<TypeTable> TypeTables { get; set; }
=======
        public virtual DbSet<TrainerSchedule> TrainerSchedules { get; set; }
>>>>>>> master
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WorkoutProgram> WorkoutPrograms { get; set; }
    }
}
