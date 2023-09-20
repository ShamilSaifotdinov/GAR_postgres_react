using System;
using System.Collections.Generic;

namespace GAR_table.DB.Region;

public partial class AddrObjParam
{
    public long Id { get; set; }

    public long Objectid { get; set; }

    public long? Changeid { get; set; }

    public long Changeidend { get; set; }

    public short Typeid { get; set; }

    public string Value { get; set; } = null!;

    public DateOnly Updatedate { get; set; }

    public DateOnly Startdate { get; set; }

    public DateOnly Enddate { get; set; }
}
