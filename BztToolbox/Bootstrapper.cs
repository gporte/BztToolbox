using System.Configuration;
using System.Windows;
using BztToolbox.Common.Utility;
using BztToolbox.Utility;
using BztToolbox.ViewModels;
using BztToolbox.Views;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.UnityExtensions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BztToolbox
{
	public class Bootstrapper : UnityBootstrapper
	{
		protected override DependencyObject CreateShell() {
			var vm = new ShellViewModel();
			Shell shell = new Shell(vm);
			shell.Show();

			// Enregistrement de l'event aggregator et de la chaîne de connexion à la base BizTalk
			var container = ServiceLocator.Current.GetInstance<IUnityContainer>();
			container.RegisterType<IEventAggregator, EventAggregator>();
			container.RegisterInstance<string>("BztMgmtDb", ConfigurationManager.ConnectionStrings["BztMgmtDb"].ConnectionString);

			// trace
			NotificationHelper.WriteNotification("Initialisation du Shell.");

			return shell;
		}

		protected override IModuleCatalog GetModuleCatalog() {
			var moduleCatalog = new PrioritizedDirectoryModuleCatalog()
			{
				ModulePath = @".\Modules"
			};

			return moduleCatalog;
		}
	}
}
