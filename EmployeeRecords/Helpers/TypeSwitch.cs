using EmployeeRecords.API.Models;
using System;

namespace EmployeeRecords.API.Helpers
{
    public static class TypeSwitch
	{
		/// <summary>
		/// Do the action
		/// </summary>
		/// <param name="source">Source of the action</param>
		/// <param name="cases">Cases</param>
		public static void Do(object source, params CaseInfo[] cases)
		{
			if (source == null)
			{
				return;
			}

			var type = source.GetType();
			foreach (var entry in cases)
			{
				if (entry.IsDefault || entry.Target.IsAssignableFrom(type))
				{
					entry.Action(source);
					break;
				}
			}
		}

		/// <summary>
		/// Case Block
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <param name="action">Action</param>
		/// <returns>Case Information</returns>
		public static CaseInfo Case<T>(Action action) => new CaseInfo()
		{
			Action = x => action(),
			Target = typeof(T),
		};

		/// <summary>
		/// Case Block (Generic)
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <param name="action">Action</param>
		/// <returns>Case Information</returns>
		public static CaseInfo Case<T>(Action<T> action) => new CaseInfo()
		{
			Action = (x) => action((T)x),
			Target = typeof(T),
		};

		/// <summary>
		/// Default Case Block
		/// </summary>
		/// <param name="action">Action</param>
		/// <returns>Case Information</returns>
		public static CaseInfo Default(Action action) => new CaseInfo()
		{
			Action = x => action(),
			IsDefault = true,
		};
	}
}
