using System.ComponentModel.DataAnnotations;

namespace VincentTaskManagementAPI.Attributes
{
		public class DuedateValidate: ValidationAttribute
		{
				protected override ValidationResult IsValid(object value, ValidationContext validationContext)
				{
						DateTime dateTime = Convert.ToDateTime(value);
						if(dateTime < DateTime.Now)
						{
								return new ValidationResult("Date must be greater than current date");
						}
						return ValidationResult.Success;
				}
		}
}
