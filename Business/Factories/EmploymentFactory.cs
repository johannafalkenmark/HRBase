using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class EmploymentFactory
{
    public static EmploymentEntity? Create(EmploymentRegistrationForm form) => form == null ? null : new EmploymentEntity
    {
        EmploymentType = form.EmploymentType
    };

    public static Employment? Create(EmploymentEntity entity)
    {
        if (entity == null) return null;

        var employment = new Employment
        {
            Id = entity.Id,
            EmploymentType = entity.EmploymentType
        };
        return employment;
    }

    public static EmploymentEntity? Update(EmploymentEntity entity, EmploymentRegistrationForm form)
    {
        if (entity == null || form == null) return null;
        entity.EmploymentType = form.EmploymentType;
        return entity;
    }

}
