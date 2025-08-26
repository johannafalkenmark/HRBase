using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities;

public class EmploymentEntity
{
    [Key]
    public int Id { get; set; }


    ////Koppling till Employee:
    //public int EmployeeId { get; set; }
    //public EmployeeBasicEntity Employee { get; set; } = null!;


    public string? EmploymentType { get; set; } 
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

}
