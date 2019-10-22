using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
	public class EmployeeEditViewModel : CreateViewModel
	{
		public int Id { get; set; }
		public string ExistingPhotoPath { get; set; }
	}
}
