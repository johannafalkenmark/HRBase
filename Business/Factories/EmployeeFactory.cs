using Azure.Core;
using Business.Models;
using Data.Entities;
using Data.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Business.Factories;

public class EmployeeFactory
{

    public static EmployeeBasicEntity? Create(EmployeeRegistrationForm form) => form == null ? null : new EmployeeBasicEntity
        {
    FirstName = form.FirstName,
    LastName = form.LastName,
    EmploymentId = form.EmploymentId
    };

    //Nedan - konvertera en EmployeeBasicEntity till en Employee:

    public static Employee? Create(EmployeeBasicEntity entity)
    {
        if (entity == null) return null;

        var employee = new Employee
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,

        };

if (entity.Employment != null)
        {
            employee.Employment = new Employment
            {
                Id = entity.Employment.Id,
                EmploymentType = entity.Employment.EmploymentType
            };

        }

return employee;

    }


    //Uppdatera en EmployeeBasicEntity med data från en EmployeeRegistrationForm:


    public static EmployeeBasicEntity? Update(EmployeeBasicEntity entity, EmployeeRegistrationForm form)
    {
               if (entity == null || form == null) return null;
        entity.FirstName = form.FirstName;
        entity.LastName = form.LastName;
        entity.EmploymentId = form.EmploymentId;
        return entity;
    }


}
