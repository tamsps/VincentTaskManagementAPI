using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VincentTaskManagementAPI.Attributes;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace VincentTaskManagementAPI.Models
{
		[Index(nameof(Id), IsUnique = true)]
		public class VincTaskModel
		{
				[Key]
				[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
				public int Id { get; set; }
				[Required(ErrorMessage = "Title is required.")]
				public string Title { get; set; }
				[PriorityValidate]
				public PriorityEnums Priority  { get; set; }
				public string Description { get; set; }
				[DuedateValidate]
				public DateTime DueDate { get; set; }
		}
}
