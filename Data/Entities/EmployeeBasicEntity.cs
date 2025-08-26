using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities;

public class EmployeeBasicEntity
{

    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }

    public int EmploymentId { get; set; }  // Främmande nyckel för att koppla till anställning
    //public EmploymentEntity Employment { get; set; } = null!;  // Navigeringsegenskap för att komma åt anställningsdetaljer
    //public ICollection<EmploymentEntity> Employments { get; set; } = null!;  // En anställd kan ha flera anställningar. 1 till många relation.

}


