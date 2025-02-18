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

//SC_Registration
//  ID int primary key
//		FK_Student_Registration__Student_ID
//		FK_Summer_Camps
//		FK_Summer_Camp_Choice
//		Status
//		CalendarYR
		
//	SC_Status_Tracking
//		ID int primary key
//		FK_SC_Registration int
//		Action varchar(255)
		
//	SC_Status_Lookup
//		ID int primary key
//		Code varchar(1)
//		Desc varchar(255)
		
//	SC_Payments
//		ID int primary key
//		FK_SC_Registration int
//		Event_Type varchar(255)
//		Amount decimal(9, 2)
//Message varchar(255)