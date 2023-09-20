using System;
using System.Collections.Generic;

namespace GAR_table.DB.Region;

public partial class AdmHierarchy
{
    public int Id { get; set; }

    public int? Objectid { get; set; }

    public int? Parentobjid { get; set; }

    public bool? Isactive { get; set; }

    public string? Path { get; set; }
}
