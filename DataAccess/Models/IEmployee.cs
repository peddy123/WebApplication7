using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
	public interface IEmployee
	{
		Employee GetEmployee(int Id);
		List<Employee> GetEmployees(int page, int pageSize);

		Employee ADD(Employee employee);
		Employee Update(Employee changes);
		Employee Delete(int Id);
	}
}
