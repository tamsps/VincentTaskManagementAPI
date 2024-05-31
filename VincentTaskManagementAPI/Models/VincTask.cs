namespace VincentTaskManagementAPI.Models
{
		public class VincTask
		{
				public int Id { get; set; }
				public string Title { get; set; }
				public PriorityEnums Priority  { get; set; }
				public string Description { get; set; }
				public DateTime DueDate { get; set; }
		}
}
