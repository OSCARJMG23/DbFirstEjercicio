using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class team_drivers
    {
        public int Team_Id { get; set; }
        public Team Team { get; set; }
        public int Driver_Id { get; set; }
        public Driver Driver { get; set; }
    }
}