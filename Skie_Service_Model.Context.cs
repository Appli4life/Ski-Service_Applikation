﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ski_Service_Applikation
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ski_serviceEntities : DbContext
    {
        public ski_serviceEntities()
            : base("name=ski_serviceEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<altersgruppe> altersgruppe { get; set; }
        public virtual DbSet<angebot> angebot { get; set; }
        public virtual DbSet<berechtigungsstufe> berechtigungsstufe { get; set; }
        public virtual DbSet<geschlecht> geschlecht { get; set; }
        public virtual DbSet<kategorie> kategorie { get; set; }
        public virtual DbSet<kunde> kunde { get; set; }
        public virtual DbSet<marke> marke { get; set; }
        public virtual DbSet<miete> miete { get; set; }
        public virtual DbSet<mitarbeiter> mitarbeiter { get; set; }
        public virtual DbSet<status> status { get; set; }
        public virtual DbSet<alle_angebote_view> alle_angebote_view { get; set; }
        public virtual DbSet<alle_miete_view> alle_miete_view { get; set; }
        public virtual DbSet<alle_mitarbeiter_view> alle_mitarbeiter_view { get; set; }
        public virtual DbSet<detail_miete_view> detail_miete_view { get; set; }
    }
}
