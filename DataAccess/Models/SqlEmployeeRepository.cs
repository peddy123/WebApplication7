using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Models
{
	public class SqlEmployeeRepository :IEmployee
	{
		private readonly AppDbContext context;
		public SqlEmployeeRepository(AppDbContext context)
		{
			this.context = context;
		}

		public Employee ADD(Employee employee)
		{
			context.Employees.Add(employee);
			context.SaveChanges();
			return employee;
		}

		public Employee Delete(int Id)
		{
			Employee employee = context.Employees.Find(Id);
			if (employee != null)
			{
				context.Employees.Remove(employee);
				context.SaveChanges();
			}
			return employee;
		}

		public Employee GetEmployee(int Id)
		{
			return context.Employees.Find(Id);
		}

		public List<Employee> GetEmployees(int page, int pageSize)
		{
			return context.Employees.Skip(page-1).Take(pageSize).ToList();
		}

		public Employee Update(Employee employeeChanges)
		{
			var employee = context.Employees.Attach(employeeChanges);
			employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			context.SaveChanges();
			return employeeChanges;
		}


	}
}
