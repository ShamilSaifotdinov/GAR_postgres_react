using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using GAR_table.DB.Region;

namespace GAR_table.DB;

public partial class GarContextRegion50 : DbContext, IGarContextRegion
{
    public GarContextRegion50()
    {
    }

    public GarContextRegion50(DbContextOptions<GarContextRegion50> options)
        : base(options)
    {
    }

    public virtual DbSet<AddrObj> AddrObjs { get; set; }

    public virtual DbSet<AddrObjParam> AddrObjParams { get; set; }

    public virtual DbSet<AdmHierarchy> AdmHierarchies { get; set; }

    public virtual DbSet<MunHierarchy> MunHierarchies { get; set; }

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

        modelBuilder.Entity<AddrObjParam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ADDR_OBJ_PARAMS_pkey");

            entity.ToTable("ADDR_OBJ_PARAMS", "_50");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Changeid)
                .ValueGeneratedOnAdd()
                .HasColumnName("CHANGEID");
            entity.Property(e => e.Changeidend)
                .ValueGeneratedOnAdd()
                .HasColumnName("CHANGEIDEND");
            entity.Property(e => e.Enddate).HasColumnName("ENDDATE");
            entity.Property(e => e.Objectid)
                .ValueGeneratedOnAdd()
                .HasColumnName("OBJECTID");
            entity.Property(e => e.Startdate).HasColumnName("STARTDATE");
            entity.Property(e => e.Typeid)
                .ValueGeneratedOnAdd()
                .HasColumnName("TYPEID");
            entity.Property(e => e.Updatedate).HasColumnName("UPDATEDATE");
            entity.Property(e => e.Value).HasColumnName("VALUE");
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

        modelBuilder.Entity<MunHierarchy>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MUN_HIERARCHY", "_50");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Isactive).HasColumnName("ISACTIVE");
            entity.Property(e => e.Objectid).HasColumnName("OBJECTID");
            entity.Property(e => e.Parentobjid).HasColumnName("PARENTOBJID");
            entity.Property(e => e.Path)
                .HasColumnType("character varying")
                .HasColumnName("PATH");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
