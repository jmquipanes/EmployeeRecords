using System;

namespace EmployeeRecords.API.Models
{
    public class CaseInfo
	{
		/// <summary>
		/// Gets or sets a value indicating whether is Default Case
		/// </summary>
		public bool IsDefault { get; set; }

		/// <summary>
		/// Gets or sets type to switch on
		/// </summary>
		public Type Target { get; set; }

		/// <summary>
		/// Gets or sets thing to do if this case selected
		/// </summary>
		public Action<object> Action { get; set; }
	}
}
