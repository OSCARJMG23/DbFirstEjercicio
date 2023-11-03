using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class Team
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();
    public ICollection<team_drivers> Team_Drivers { get; set; }
}
