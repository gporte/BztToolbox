using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using BztToolbox.Common.Utility;
using Microsoft.Practices.Composite.Modularity;

namespace BztToolbox.Utility
{
	/// <summary>
	/// ModuleCatalog that respects PriorityAttribute for sorting modules
	/// </summary>
	[SecurityPermission(SecurityAction.InheritanceDemand), SecurityPermission(SecurityAction.LinkDemand)]
	public class PrioritizedDirectoryModuleCatalog : DirectoryModuleCatalog
	{
		/// <summary>
		/// local class to load assemblies into different appdomain which is then discarded
		/// </summary>
		private class ModulePriorityLoader : MarshalByRefObject
		{
			/// <summary>
			/// Get the priorities
			/// </summary>
			/// <param name="modules"></param>
			/// <returns></returns>
			[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods", MessageId = "System.Reflection.Assembly.LoadFrom")]
			public Dictionary<string, int> GetPriorities(IEnumerable<ModuleInfo> modules) {
				//retrieve the priorities of each module, so that we can use them to override the 
				//sorting - but only so far as we don't mess up the dependencies
				var priorities = new Dictionary<string, int>();
				var assemblies = new Dictionary<string, Assembly>();

				foreach (ModuleInfo module in modules) {
					if (!assemblies.ContainsKey(module.Ref)) {
						//LoadFrom should generally be avoided appently due to unexpected side effects,
						//but since we are doing all this in a separate AppDomain which is discarded
						//this needn't worry us
						assemblies.Add(module.Ref, Assembly.LoadFrom(module.Ref));
					}

					Type type = assemblies[module.Ref].GetExportedTypes()
						.Where(t => t.AssemblyQualifiedName.Equals(module.ModuleType, StringComparison.Ordinal))
						.First();

					var priorityAttribute =
						CustomAttributeData.GetCustomAttributes(type).FirstOrDefault(
							cad => cad.Constructor.DeclaringType.FullName == typeof(PriorityAttribute).FullName);

					int priority;
					if (priorityAttribute != null) {
						priority = (int)priorityAttribute.ConstructorArguments[0].Value;
					}
					else {
						priority = 0;
					}

					priorities.Add(module.ModuleName, priority);
				}

				return priorities;
			}
		}

		/// <summary>
		/// Get the priorities that have been assigned to each module.  If a module does not have a priority 
		/// assigned (via the Priority attribute) then it is assigned a priority of 0
		/// </summary>
		/// <param name="modules">modules to retrieve priorities for</param>
		/// <returns></returns>
		private Dictionary<string, int> GetModulePriorities(IEnumerable<ModuleInfo> modules) {
			AppDomain childDomain = BuildChildDomain(AppDomain.CurrentDomain);
			try {
				Type loaderType = typeof(ModulePriorityLoader);
				var loader =
					(ModulePriorityLoader)
					childDomain.CreateInstanceFrom(loaderType.Assembly.Location, loaderType.FullName).Unwrap();

				return loader.GetPriorities(modules);
			}
			finally {
				AppDomain.Unload(childDomain);
			}
		}

		/// <summary>
		/// Sort modules according to dependencies and Priority
		/// </summary>
		/// <param name="modules">modules to sort</param>
		/// <returns>sorted modules</returns>
		protected override IEnumerable<ModuleInfo> Sort(IEnumerable<ModuleInfo> modules) {
			Dictionary<string, int> priorities = GetModulePriorities(modules);
			//call the base sort since it resolves dependencies, then re-sort 
			var result = new List<ModuleInfo>(base.Sort(modules));
			result.Sort((x, y) =>
			{
				string xModuleName = x.ModuleName;
				string yModuleName = y.ModuleName;
				//if one depends on other then non-dependent must come first
				//otherwise base on priority
				if (x.DependsOn.Contains(yModuleName))
					return 1; //x after y
				else if (y.DependsOn.Contains(xModuleName))
					return -1; //y after x
				else
					return priorities[xModuleName].CompareTo(priorities[yModuleName]);
			});

			return result;
		}
	}
}
