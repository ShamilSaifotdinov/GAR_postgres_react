using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GAR_table.DB;

public partial class GarContext : DbContext
{
    public GarContext()
    {
    }

    public GarContext(DbContextOptions<GarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ObjectLevel> ObjectLevels { get; set; }

    public virtual DbSet<ParamType> ParamTypes { get; set; }

    public virtual DbSet<RegionsRf> RegionsRves { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=GAR;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");

        modelBuilder.Entity<ObjectLevel>(entity =>
        {
            entity.HasKey(e => e.Level).HasName("OBJECT_LEVELS_pkey");

            entity.ToTable("OBJECT_LEVELS");

            entity.Property(e => e.Level).HasColumnName("LEVEL");
            entity.Property(e => e.Name).HasColumnName("NAME");
        });

        modelBuilder.Entity<ParamType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PARAM_TYPES_pkey");

            entity.ToTable("PARAM_TYPES");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code).HasColumnName("CODE");
            entity.Property(e => e.Desc).HasColumnName("DESC");
            entity.Property(e => e.Name).HasColumnName("NAME");
        });

        modelBuilder.Entity<RegionsRf>(entity =>
        {
            //entity.HasKey(e => e.Id).HasName("regions_rf_pkey");
            entity.HasNoKey();

            entity.ToTable("regions_rf");
            entity.Property(e => e.geom).HasColumnName("geom");
            //entity.Property(e => e.Id).HasColumnName("id");
            //entity.Property(e => e.AddrCount)
            //    .HasMaxLength(254)
            //    .HasColumnName("addr_count");
            //entity.Property(e => e.AdminLeve)
            //    .HasMaxLength(254)
            //    .HasColumnName("admin_leve");
            //entity.Property(e => e.BorderTyp)
            //    .HasMaxLength(254)
            //    .HasColumnName("border_typ");
            //entity.Property(e => e.Boundary)
            //    .HasMaxLength(254)
            //    .HasColumnName("boundary");
            //entity.Property(e => e.CladrCode)
            //    .HasMaxLength(254)
            //    .HasColumnName("cladr_code");
            //entity.Property(e => e.Gost767)
            //    .HasMaxLength(254)
            //    .HasColumnName("gost_7.67-");
            //entity.Property(e => e.IntName)
            //    .HasMaxLength(254)
            //    .HasColumnName("int_name");
            //entity.Property(e => e.IntRef)
            //    .HasMaxLength(254)
            //    .HasColumnName("int_ref");
            //entity.Property(e => e.IsInCoun)
            //    .HasMaxLength(254)
            //    .HasColumnName("is_in_coun");
            //entity.Property(e => e.Iso31662)
            //    .HasMaxLength(254)
            //    .HasColumnName("iso3166-2");
            //entity.Property(e => e.Koatuu)
            //    .HasMaxLength(254)
            //    .HasColumnName("koatuu");
            //entity.Property(e => e.Name)
            //    .HasMaxLength(254)
            //    .HasColumnName("name");
            //entity.Property(e => e.NameRu)
            //    .HasMaxLength(254)
            //    .HasColumnName("name_ru");
            //entity.Property(e => e.OktmoUser)
            //    .HasMaxLength(254)
            //    .HasColumnName("oktmo_user");
            //entity.Property(e => e.Populati1)
            //    .HasMaxLength(254)
            //    .HasColumnName("populati_1");
            //entity.Property(e => e.Population)
            //    .HasMaxLength(254)
            //    .HasColumnName("population");
            //entity.Property(e => e.Ref)
            //    .HasMaxLength(254)
            //    .HasColumnName("ref");
            //entity.Property(e => e.SsrfCode)
            //    .HasMaxLength(254)
            //    .HasColumnName("ssrf_code");
            //entity.Property(e => e.Timezone)
            //    .HasMaxLength(254)
            //    .HasColumnName("timezone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
