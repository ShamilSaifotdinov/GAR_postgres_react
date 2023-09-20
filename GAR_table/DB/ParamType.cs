using System;
using System.Collections.Generic;

namespace GAR_table.DB;

public partial class ParamType
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public string Desc { get; set; } = null!;

    public string? Code { get; set; }
}
