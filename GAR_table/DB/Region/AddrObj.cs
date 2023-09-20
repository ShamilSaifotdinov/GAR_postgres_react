using System;
using System.Collections.Generic;

namespace GAR_table.DB.Region;

public partial class AddrObj
{
    public int Id { get; set; }

    public int? Objectid { get; set; }

    public string? Name { get; set; }

    public string? Typename { get; set; }

    public short? Level { get; set; }

    public bool? Isactual { get; set; }

    public bool? Isactive { get; set; }
}
