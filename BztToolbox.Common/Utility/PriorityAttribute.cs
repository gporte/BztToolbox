using System;

namespace BztToolbox.Common.Utility
{
	/// <summary>
	/// Allows the order of module loading to be controlled.  Where dependencies
	/// allow, module loading order will be controlled by relative values of priority
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class PriorityAttribute : Attribute
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="priority">the priority to assign</param>
		public PriorityAttribute(int priority) {
			this.Priority = priority;
		}

		/// <summary>
		/// Gets or sets the priority of the module.
		/// </summary>
		/// <value>The priority of the module.</value>
		public int Priority { get; private set; }
	}
}
