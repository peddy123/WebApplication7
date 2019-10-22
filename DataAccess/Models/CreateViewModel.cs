using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
	public class CreateViewModel
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public Department? Department { get; set; }
		public IFormFile Photo { get; set; }
	}
}
