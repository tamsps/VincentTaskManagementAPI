using VincentTaskManagementAPI.Models;

namespace VincentTaskManagementAPI.Db
{
		public class VincTasksStore
		{
				public static IEnumerable<VincTask> MyTasks { get; set; }
				public VincTasksStore()
				{
						if (MyTasks == null)
						{
								MyTasks = new List<VincTask>
								{
										new VincTask
										{
												Id = 1,
												Title = "Task 1",
												Priority = PriorityEnums.Low,
												Description = "Task 1 Description",
												DueDate = DateTime.Now.AddDays(1)
										},
										new VincTask
										{
												Id = 2,
												Title = "Task 2",
												Priority = PriorityEnums.Medium,
												Description = "Task 2 Description",
												DueDate = DateTime.Now.AddDays(2)
										},
										new VincTask
										{
												Id = 3,
												Title = "Task 3",
												Priority = PriorityEnums.High,
												Description = "Task 3 Description",
												DueDate = DateTime.Now.AddDays(3)
										}
								};
						}
				}
		}
}
