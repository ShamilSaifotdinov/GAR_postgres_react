using System;
using System.Collections.Generic;
using GAR_table.DB.Region;
using Microsoft.EntityFrameworkCore;

namespace GAR_table.DB;

public partial class GarContext_2 : DbContext
{
    public GarContext_2()
    {
    }

    public GarContext_2(DbContextOptions<GarContext_2> options)
        : base(options)
    {
    }

    public virtual DbSet<AddrObj> AddrObjs { get; set; }

    public virtual DbSet<AdmHierarchy> AdmHierarchies { get; set; }

    public virtual DbSet<RegionsRf> RegionsRves { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=GAR;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");

        modelBuilder.Entity<AddrObj>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ADDR_OBJ_pkey");

            entity.ToTable("ADDR_OBJ", "_50");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Isactive).HasColumnName("ISACTIVE");
            entity.Property(e => e.Isactual).HasColumnName("ISACTUAL");
            entity.Property(e => e.Level).HasColumnName("LEVEL");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("NAME");
            entity.Property(e => e.Objectid).HasColumnName("OBJECTID");
            entity.Property(e => e.Typename)
                .HasColumnType("character varying")
                .HasColumnName("TYPENAME");
        });

        modelBuilder.Entity<AdmHierarchy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ADM_HIERARCHY_pkey");

            entity.ToTable("ADM_HIERARCHY", "_50");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Isactive).HasColumnName("ISACTIVE");
            entity.Property(e => e.Objectid).HasColumnName("OBJECTID");
            entity.Property(e => e.Parentobjid).HasColumnName("PARENTOBJID");
            entity.Property(e => e.Path)
                .HasColumnType("character varying")
                .HasColumnName("PATH");
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
