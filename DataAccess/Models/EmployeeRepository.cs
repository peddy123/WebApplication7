using System;
using System.Collections.Generic;
using System.Linq;


namespace DataAccess.Models
{
	public class EmployeeRepository : IEmployee
	{
		List<Employee> employees = new List<Employee>
		{
			new Employee{Id = 1, Department =Department.HR , Name = "Pardeep", Email = "Pardee@test.com" },
			new Employee{Id = 2, Department =Department.IT , Name = "Test", Email = "Test@test.com" },
			new Employee{Id = 3, Department =Department.IT , Name = "Test2", Email = "Test2@test.com" }
		};

		public Employee ADD(Employee employee)
		{
			employee.Id = employees.Max(e => e.Id) + 1;
			employees.Add(employee);
			return employee;
		}

		public Employee Delete(int Id)
		{
			throw new NotImplementedException();
		}

		public Employee GetEmployee(int Id)
		{
			return employees.FirstOrDefault(e => e.Id == Id);
		}

		public List<Employee> GetEmployees(int page, int pageSize)
		{
			return employees;
		}

		public Employee Update(Employee changes)
		{
			throw new NotImplementedException();
		}
	}
}
