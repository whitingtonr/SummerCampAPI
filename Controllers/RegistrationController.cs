using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SummerCampAPI.Models;
using System.Globalization;
using System.Text.Json;

// Prep database using sql stored in file: School choice api joins.sql
// This was done manually to save time and should be incorporated into the application in v2.

namespace SummerCampAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RegistrationsController : Controller
	{
		private readonly RegistrationsContext _context;

		public RegistrationsController(RegistrationsContext context)
		{
			_context = context;
		}

		[HttpGet("test2")]
		public async Task<IActionResult> Test2()
		{
			string cs = _context.Database.GetDbConnection().ConnectionString;
			return new JsonResult(cs);
		}

		[HttpGet("test")]
		public async Task<IActionResult> Test()
		{
			var sql = @"select top 100 * from Summer_Camp_Choice";
			var result = new List<Dictionary<string, object>>();

			using (var connection = _context.Database.GetDbConnection())
			{
				await connection.OpenAsync();
				using (var command = connection.CreateCommand())
				{
					command.CommandText = sql;
					using (var reader = await command.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							var row = new Dictionary<string, object>();
							for (int i = 0; i < reader.FieldCount; i++)
							{
								// Don't write null values because it causes error in the json
								// Probably a better solution but working on a tight deadline.
								if (!(reader.GetValue(i) is DBNull))
								{
									row[reader.GetName(i)] = reader.GetValue(i);
								}
							}
							result.Add(row);
						}
					}
				}
			}

			return new JsonResult(result);
		}

		[HttpGet("summercampstatuslookup")]
		public async Task<IActionResult> GetSummerCampStatuses()
		{
			var sql = "Select * from Summer_Camp_Status_Lookup WHERE ID_Code = 'A' OR ID_Code = 'I'";
			var result = new List<Dictionary<string, object>>();

			using (var connection = _context.Database.GetDbConnection())
			{
				await connection.OpenAsync();
				using (var command = connection.CreateCommand())
				{
					command.CommandText = sql;
					using (var reader = await command.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							var row = new Dictionary<string, object>();
							for (int i = 0; i < reader.FieldCount; i++)
							{
								row[reader.GetName(i)] = reader.GetValue(i);
							}
							result.Add(row);
						}
					}
				}
			}

			return new JsonResult(result);
		}

		[HttpGet("summercampadmingrid")]
		public async Task<IActionResult> GetSummerCampChoices()
		{
			var sql = @"select 
										s.FirstName,
										s.LastName,
										s.EthnicCode,
										r.ID,
										r.FK_PCS_StudentLookup__Student_ID, 
										r.Summer_Camp_Title, 
										r.WeekNbr,
										r.SchoolName,
										r.School_ID,
										s.PrimaryExceptionality,
										s.LEP,
										s.Gender,
										r.FK_Status, 
										r.CalendarYR,
										r.FK_Summer_Camp_Choice,
										r.UpdateUser,
										r.UpdateDate
									from Summer_Camp_Registration as r
									JOIN [Res_DB].[dbo].[PCS_StudentLookup] as s
										on s.LocalStuID = r.FK_PCS_StudentLookup__Student_ID";
			//  where c.WeekNbr = '2' and c.School_ID = '2301'
			var result = new List<Dictionary<string, object>>();

			using (var connection = _context.Database.GetDbConnection())
			{
				await connection.OpenAsync();
				using (var command = connection.CreateCommand())
				{
					command.CommandText = sql;
					using (var reader = await command.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							var row = new Dictionary<string, object>();
							for (int i = 0; i < reader.FieldCount; i++)
							{
								// Don't write null values because it causes error in the json
								// Probably a better solution but working on a tight deadline.
								if (!(reader.GetValue(i) is DBNull))
								{
									row[reader.GetName(i)] = reader.GetValue(i);
								}
							}
							result.Add(row);
						}
					}
				}
			}

			return new JsonResult(result);
		}

		[HttpPost("summercampstatusupdate")]
		public async Task<IActionResult> UpdateSummerCampStatus([FromBody] List<Summer_Camp_Registration> registrations)
		{
			if (registrations == null || !registrations.Any())
			{
				return BadRequest("Invalid registration data.");
			}

			var registrationIds = registrations.Select(r => r.ID).ToList();
			var existingRegistrations = await _context.Summer_Camp_Registration
			.Where(r => registrationIds.Contains(r.ID))
			.ToListAsync();

			if (existingRegistrations.Count != registrations.Count)
			{
				return NotFound("One or more registrations not found.");
			}

			// Update Registration table and add a record to Summer_Camp_Status_History
			foreach (var registration in registrations)
			{
				var existingRegistration = existingRegistrations.FirstOrDefault(r => r.ID == registration.ID);
				if (existingRegistration != null)
				{
					existingRegistration.FK_Status = registration.FK_Status;

					// Add a record to Summer_Camp_Status_History
					var statusHistory = new Summer_Camp_Status_History
					{
						FK_Summer_Camp_Registration = existingRegistration.ID,
						FK_NewStatus = registration.FK_Status,
						Updated_Date = DateTime.Now,
						Updated_User = registration.UpdateUser // Records updateUser in status but not in the registration table
					};
					_context.Summer_Camp_Status_History.Add(statusHistory);
				}
			}


			await _context.SaveChangesAsync();
			return Ok($"{existingRegistrations.Count.ToString()} statuses updated successfully.");
		}

		[HttpPost("webhooksuccess")]
		public async Task<IActionResult> ReceiveWebhook([FromBody] object payload)
		{
			var jsonPayload = JsonSerializer.Serialize(payload, new JsonSerializerOptions
			{
				WriteIndented = true
			});
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "webhook_payload.txt");

			string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string str = $"--------- Begin Payload received and saved at {timestamp}. ---------";
			await System.IO.File.AppendAllTextAsync(filePath, Environment.NewLine + str + Environment.NewLine);

			await System.IO.File.AppendAllTextAsync(filePath, jsonPayload + Environment.NewLine);

			var response = new { message = "Payload received and saved." };
			str = $"--------- End Payload received and saved at {timestamp}. ---------";
			await System.IO.File.AppendAllTextAsync(filePath, Environment.NewLine + str + Environment.NewLine);

			return Ok(response);
		}

		[HttpGet("summercampregistration/{id}")]
		public async Task<IActionResult> GetSummerCampRegistration(int id)
		{
			var registration = await _context.Summer_Camp_Registration.FindAsync(id);
			if (registration == null)
			{
				return NotFound("Registration not found.");
			}
			return Ok(registration);
		}

	}
}
