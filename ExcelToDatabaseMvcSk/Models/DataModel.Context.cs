﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExcelToDatabaseMvcSk.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ExcelImportDBEntities : DbContext
    {
        public ExcelImportDBEntities()
            : base("name=ExcelImportDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DosyaVerileri> DosyaVerileri { get; set; }
        public virtual DbSet<HesaplananVeri> HesaplananVeri { get; set; }
        public virtual DbSet<Users1> Users1 { get; set; }
        public virtual DbSet<Users2> Users2 { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }
    }
}
