using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Moq;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using VincentTaskManagementAPI.Controllers;
using VincentTaskManagementAPI.Db;
using VincentTaskManagementAPI.Models;
using VincentTaskManagementAPI.Services;

namespace NunitTestProject
{
		[TestFixture]
		public class UnitTestGetApi
		{
				private Mock<ITaskService> _mockTaskService;
				private List<VincTaskModel> _tasksList;
				[SetUp]
				public void Setup()
				{
						_mockTaskService = new Mock<ITaskService>();
						var dbOptionsBuilder = new DbContextOptionsBuilder<VincDbContext>().UseInMemoryDatabase("vincetask");

					//	//_mockTaskService.Setup(x => x.GetTasks()).Returns(_tasksList.ToList());
					//	VincTaskController controller = new VincTaskController(_mockTaskService.Object);
						//var result = controller.GetTasks();
						using (var db = new VincDbContext(dbOptionsBuilder.Options))
						{
								var taskService = new TaskService(db);
								// fix up some data
								db.Set<VincTaskModel>().Add(new VincTaskModel()
								{
										Id = 1,
										Title = "Task 1",
										Priority = PriorityEnums.High,
										Description = "Task 1 Description",
										DueDate = DateTime.Now
								});
								

								db.SaveChangesAsync();
								db.Set<VincTaskModel>().Add(new VincTaskModel()
								{
										Id = 2,
										Title = "Task 2",
										Priority = PriorityEnums.Medium,
										Description = "Task 2 Description",
										DueDate = DateTime.Now
								});
								db.SaveChangesAsync();
								db.Set<VincTaskModel>().Add(new VincTaskModel()
								{
										Id = 3,
										Title = "Task 3",
										Priority = PriorityEnums.Low,
										Description = "Task 3 Description",
										DueDate = DateTime.Now
								});
								db.SaveChangesAsync();
						}
				}

				[Test]
				public void Test1_exist_task()
				{
						var dbOptionsBuilder = new DbContextOptionsBuilder<VincDbContext>().UseInMemoryDatabase("vincetask");
						using (var db = new VincDbContext(dbOptionsBuilder.Options))
						{
								// create the service
								var taskService = new TaskService(db);
								// act
								var findTask = taskService.GetTaskById(1);
								//// assert
								Assert.NotNull(findTask);
						}
				}
				[Test]
				public void Test_correct_priority()
				{
						var dbOptionsBuilder = new DbContextOptionsBuilder<VincDbContext>().UseInMemoryDatabase("vincetask");
						using (var db = new VincDbContext(dbOptionsBuilder.Options))
						{
								// create the service
								var taskService = new TaskService(db);
								// act
								var findTask = taskService.GetTaskById(2);
								//// assert
								Assert.NotNull(findTask);
								Assert.AreEqual(2, (int)findTask.Priority);
						}
				}
				[Test]
				public void Test_correct_description()
				{
						var dbOptionsBuilder = new DbContextOptionsBuilder<VincDbContext>().UseInMemoryDatabase("vincetask");
						using (var db = new VincDbContext(dbOptionsBuilder.Options))
						{
								// create the service
								var taskService = new TaskService(db);
								// act
								var findTask = taskService.GetTaskById(3);
								//// assert
								Assert.NotNull(findTask);
								Assert.AreEqual("Task 3 Description", findTask.Description);
						}
				}
				[Test]
				public void Test_correct_message_attribute()
				{
						var dbOptionsBuilder = new DbContextOptionsBuilder<VincDbContext>().UseInMemoryDatabase("vincetask");
						using (var db = new VincDbContext(dbOptionsBuilder.Options))
						{
								// create the service
								var taskService = new TaskService(db);
								// act
								Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
								var item = new VincTaskModel() 
								{
										Id = 4,
										Title = "",
										Description = "Task 4 Description",
										DueDate = DateTime.Now
								};
								db.Set<VincTaskModel>().Add(item);
								db.SaveChangesAsync();

								var validationResultList = new List<ValidationResult>();
								Validator.TryValidateObject(item, new ValidationContext(item), validationResultList);
								var message = validationResultList[0].ErrorMessage;
								Assert.AreEqual("Title is required.", message);
						}
				}

		}
}