using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace projectCrudwhiteoutEf.Models
{
    public partial class MasterPosition
    {
        public MasterPosition()
        {
            Employees = new HashSet<Employees>();
        }

        public int PositionId { get; set; }
        public string PositionName { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
    }
}
