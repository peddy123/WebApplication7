using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{

	[Table("Employee")]
	public class Employee
	{
		public int Id { get; set; }

		[Required, MaxLength(50,ErrorMessage = "Please Specify Name")]
		public string Name { get; set; }
		[Required(ErrorMessage ="Please Specify valid Email Format")]
		
		public string Email { get; set; }
		[Required]public Department? Department { get; set; }
		public string PhotoPath { get; set; }
	}
}
