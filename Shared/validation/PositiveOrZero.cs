using System.ComponentModel.DataAnnotations;

namespace Shared.validation;

public class PositiveOrZero : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value == null || !(value is int))
        {
            return false;
        }

        int number = (int)value;

        return number >=0;
    }
}