using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace projectCrudwhiteoutEf.Models
{
    public partial class MasterGender
    {
        public MasterGender()
        {
            Employees = new HashSet<Employees>();
        }

        public int GenderId { get; set; }
        public string GenderName { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
    }
}
