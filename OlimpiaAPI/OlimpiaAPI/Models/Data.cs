using System;
using System.Collections.Generic;

namespace OlimpiaAPI.Models;

public partial class Data
{
    public Guid Id { get; set; }

    public string? Country { get; set; }

    public string? County { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedTime { get; set; }

    public DateTime? UpdatedTime { get; set; }

    public Guid? PlayerId { get; set; }

    public virtual Player? Player { get; set; }
}
