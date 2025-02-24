using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SummerCampAPI.Models
{
	public class SC_Registration
	{
		[Key]
		public int ID { get; set; }
		public int FK_Student_Registration__Student_ID { get; set; }
		public int FK_Summer_Camps { get; set; }
		public int FK_Summer_Camp_Choice { get; set; }
		public int FK_Status { get; set; }
		[Column(TypeName = "VARCHAR(255)")]
		public string CalendarYR { get; set; }
	}

	public class SC_Status_History
	{
		[Key]
		public int ID { get; set; }
		public int FK_SC_Registration { get; set; }
		[Column(TypeName = "VARCHAR(255)")]
		public string Action { get; set; }
	}

	public class SC_Status_Lookup
	{
		[Key]
		public string ID_Code { get; set; }
		[Column(TypeName = "VARCHAR(255)")]
		public string Desc { get; set; }
	}

	public class SC_Payments
	{
		[Key]
		public int ID { get; set; }
		public int FK_SC_Registration { get; set; }
		[Column(TypeName = "VARCHAR(255)")]
		public string Event_Type { get; set; }
		[Precision(9, 2)]
		public decimal Amount { get; set; }
		[Column(TypeName = "VARCHAR(255)")]
		public string Message { get; set; }
	}
}
