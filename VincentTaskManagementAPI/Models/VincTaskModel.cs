using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VincentTaskManagementAPI.Models
{
		public class VincTaskModel
		{
				[Key]
				[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
				public int Id { get; set; }
				public string Title { get; set; }
				public PriorityEnums Priority  { get; set; }
				public string Description { get; set; }
				public DateTime DueDate { get; set; }
		}
}
