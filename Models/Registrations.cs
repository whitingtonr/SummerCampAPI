using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SummerCampAPI.Models
{
	public class Summer_Camp_Registration
	{
		[Key]
		public int ID { get; set; }
		public string FK_PCS_StudentLookup__Student_ID { get; set; }
		public int FK_Summer_Camp_Choice { get; set; }
		public string FK_Status { get; set; }
		public string CalendarYR { get; set; }
		public string WeekNbr { get; set; }
		public string Summer_Camp_Title { get; set; }
		public string SchoolName { get; set; }
		public string School_ID { get; set; }
		public DateTime UpdateDate { get; set; }
		public string UpdateUser { get; set; }
	}

	public class Summer_Camp_Status_History
	{
		[Key]
		public int ID { get; set; }
		public int FK_Summer_Camp_Registration { get; set; }
		[Column(TypeName = "VARCHAR(255)")]
		public string FK_NewStatus { get; set; }
		public DateTime Updated_Date { get; set; }
		public string Updated_User { get; set; }
	}

	public class Summer_Camp_Status_Lookup
	{
		[Key]
		public string ID_Code { get; set; }
		[Column(TypeName = "VARCHAR(255)")]
		public string Desc { get; set; }
	}

	public class Summer_Camp_Payments
	{
		[Key]
		public int ID { get; set; }
		public int FK_Summer_Camp_Registration { get; set; }
		[Column(TypeName = "VARCHAR(255)")]
		public string Event_Type { get; set; }
		[Precision(9, 2)]
		public decimal Amount { get; set; }
		[Column(TypeName = "VARCHAR(255)")]
		public string Message { get; set; }
	}
}
