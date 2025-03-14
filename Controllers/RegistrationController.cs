using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SummerCampAPI.Models;
using System.Globalization;
using System.Net.Http;
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
		private readonly IHttpClientFactory _httpClientFactory;
		public readonly HttpClient? httpClient = null;
		private string repayBaseUrl = "https://pcsb.sandbox.repay.io/checkout/merchant/api/v1/";
		private string repayAppToken = "MTNmOTM0OWUtOTNmNy00OGRjLWE1Y2EtNGJjYmE5ZGNiOTYxOmM5MGYwZGU1LTM1OWItNGExMi05MmM3LTdkYmU4OGI3MzNhMA==";

		public RegistrationsController(RegistrationsContext context, IHttpClientFactory httpClientFactory)
		{
			_context = context;
			_httpClientFactory = httpClientFactory;
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
										s.SchoolName,
										s.Grade,
										s.EthnicCode,
										s.FreeMeals,										
										r.ID,
										r.FK_PCS_StudentLookup__Student_ID, 
										r.Summer_Camp_Title, 
										r.WeekNbr,
										r.SchoolName as CampName,
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

		[HttpGet("summercampstudentinvitations/{id}")]
		public async Task<IActionResult> GetSummerCampStudentInvitations(string id)
		{
			var registrations = await _context.Summer_Camp_Registration.Where(r => r.FK_PCS_StudentLookup__Student_ID == id).ToListAsync();
			var random = new Random();

			foreach (var registration in registrations)
			{
				// Set price of camp here based on Free Lunch status
				registration.Price = 75;
			}

			if (registrations == null || !registrations.Any())
			{
				// No records found so return empty result instead of 404
				return Ok(registrations);
			}

			return Ok(registrations);
		}

		private class getcheckouturlPayload
		{
			public string username { get; set; }
			public int reg_id { get; set; }
		}

		//public async Task<IActionResult> UpdateSummerCampStatus([FromBody] List<Summer_Camp_Registration> registrations)


		[HttpPost("getcheckouturl")]
		public async Task<IActionResult> GetCheckoutUrl([FromBody] object payload)
		{
			string strOutput = "";
			var jsonPayload = JsonSerializer.Serialize(payload);
			// **************************** debug remove after testing *************************
			//jsonPayload = "[{\"username\":\"p.testp\",\"reg_id\":17902},{\"username\":\"p.testp\",\"reg_id\":17904}]";

			var users = JsonSerializer.Deserialize<List<getcheckouturlPayload>>(jsonPayload);

			var regIds = users.Select(u => u.reg_id).ToList();
			var registrations = await _context.Summer_Camp_Registration
			.Where(r => regIds.Contains(r.ID))
			.ToListAsync();

			if (registrations.Count != regIds.Count)
			{
				// Do not contunie if not all registrations are found
				return NotFound("One or more registrations not found.");
			}

			var checkoutObject = new
			{
				amount = registrations.Sum(r => r.Price),
				convenience_fee = "0",
				customer_id = users.FirstOrDefault()?.username,
				transaction_type = "sale",
				ChannelUser = "whitingtonr@pcsb.org",
				Source = "Implementations"
			};

			var jsonCheckout = JsonSerializer.Serialize(checkoutObject);

			var httpClient = _httpClientFactory.CreateClient();
			var request = new HttpRequestMessage(HttpMethod.Post, "https://pcsb.sandbox.repay.io/checkout/merchant/api/v1/checkout");
			request.Headers.Add("Authorization", "apptoken MTNmOTM0OWUtOTNmNy00OGRjLWE1Y2EtNGJjYmE5ZGNiOTYxOmM5MGYwZGU1LTM1OWItNGExMi05MmM3LTdkYmU4OGI3MzNhMA==");

			var content = new StringContent("{\r\n  \"payment_method\": \"card\",\r\n  \"Source\": \"Implementations\"\r\n}", System.Text.Encoding.UTF8, "application/json");
			request.Content = content;

			var response = await httpClient.SendAsync(request);
			var responseBody = await response.Content.ReadAsStringAsync();

			var jsonResponse = JsonDocument.Parse(responseBody);
			var checkoutFormId = jsonResponse.RootElement.GetProperty("checkout_form_id").GetString();

			//string jsonCart = "{\"amount\":150.00,\"convenience_fee\":\"0\",\"customer_id\":\"p.testp\",\"transaction_type\":\"sale\",\"ChannelUser\":\"whitingtonr@pcsb.org\",\"Source\":\"Implementations\"}";

			request = new HttpRequestMessage(HttpMethod.Post, "https://pcsb.sandbox.repay.io/checkout/merchant/api/v1/checkout-forms/" + checkoutFormId + "/one-time-use-url");
			request.Headers.Add("Authorization", "apptoken MTNmOTM0OWUtOTNmNy00OGRjLWE1Y2EtNGJjYmE5ZGNiOTYxOmM5MGYwZGU1LTM1OWItNGExMi05MmM3LTdkYmU4OGI3MzNhMA==");

			content = new StringContent(jsonCheckout, System.Text.Encoding.UTF8, "application/json");
			request.Content = content;

			response = await httpClient.SendAsync(request);
			responseBody = await response.Content.ReadAsStringAsync();

			return Ok(responseBody);
		}
	}
}
