using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace projectCrudwhiteoutEf.Models
{
    public partial class Employees
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public DateTime? Birthday { get; set; }
        public int? GenderId { get; set; }
        public int? PositionId { get; set; }

        public virtual MasterGender Gender { get; set; }
        public virtual MasterPosition Position { get; set; }
    }
}
