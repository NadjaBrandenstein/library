using System;
using System.Collections.Generic;

namespace efscaffold;

public partial class Bookimage
{
    public string Id { get; set; } = null!;

    public string? Bookid { get; set; }

    public string Url { get; set; } = null!;

    public DateTime? Createdat { get; set; }

    public virtual Book? Book { get; set; }
}
