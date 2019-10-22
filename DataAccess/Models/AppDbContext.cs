﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models
{
	public class AppDbContext: IdentityDbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			:base(options)
		{
				
		}
		public DbSet<Employee> Employees { get; set; }
	}
}
