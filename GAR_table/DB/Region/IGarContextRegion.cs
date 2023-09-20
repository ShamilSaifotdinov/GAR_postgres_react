using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using GAR_table.DB.Region;

namespace GAR_table.DB;

public interface IGarContextRegion
{
    DbSet<AddrObj> AddrObjs { get; set; }

    DbSet<AddrObjParam> AddrObjParams { get; set; }

    DbSet<AdmHierarchy> AdmHierarchies { get; set; }

    DbSet<MunHierarchy> MunHierarchies { get; set; }

    void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
