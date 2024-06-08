using System.ComponentModel.DataAnnotations;

namespace Shared.validation;

public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value == null || !(value is DateTime)) return false;

        var dateValue = (DateTime)value;

        return dateValue.Date >= DateTime.Now.Date.AddHours(24);
    }
}