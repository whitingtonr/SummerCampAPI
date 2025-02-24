using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SummerCampAPI.Models;
using System.Text.Json;

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

    [HttpGet("summercampchoices")]
    public async Task<IActionResult> GetSummerCampChoices()
    {
      var sql = "SELECT TOP 1 ID, CalendarYR, Summer_Camp_Title, WeekNbr, Status FROM Summer_Camp_Choice WHERE CalendarYR = '2023/2024'";
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

  }
}
