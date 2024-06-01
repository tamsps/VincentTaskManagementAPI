using System.ComponentModel.DataAnnotations;

namespace VincentTaskManagementAPI.Attributes
{
		public class PriorityValidate : ValidationAttribute
		{
				protected override ValidationResult IsValid(object value, ValidationContext validationContext)
				{
						int priority = (int)value;
						if(priority < 1 || priority > 3)
						{
								return new ValidationResult("Priority must be between 1 and 3");
						}
						return ValidationResult.Success;
				}
		}
}
