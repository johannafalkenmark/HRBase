

using Business.Services;

namespace Business.Models;

public class Employee
{
    public int Id { get; set; }
        public string? FirstName { get; set; }
    public string? LastName { get; set; }
 
    public Employment? Employment { get; set; }
}
