using System.ComponentModel.DataAnnotations;

namespace ModelValidationsExample.CustomValidators
{
    public class MinimumYearValidatorAttribute : ValidationAttribute
    {
        public MinimumYearValidatorAttribute()
        {
        }

        public MinimumYearValidatorAttribute(int minimumYear)
        {
            MinimumYear = minimumYear;
        }

        public int MinimumYear { get; set; } = 2000;
        public string DefaultErrorMessage { get; set; } = "年份必须小于{0}";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime date = (DateTime)value;
                if (date.Year >= MinimumYear)
                {
                    return new ValidationResult(string.Format((ErrorMessage ?? DefaultErrorMessage), MinimumYear));
                }
                else
                {
                    return ValidationResult.Success;
                }

            }

            return null;
        }
    }
}
