using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using BztToolbox.Common.BaseClasses;
using BztToolbox.Common.Commands;
using BztToolbox.Common.Utility;
using BztToolbox.Modules.Backup.Services;
using Microsoft.BizTalk.ExplorerOM;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BztToolbox.Modules.Backup.ViewModels
{
	public class BackupViewModel : ViewModelBase
	{
		private IBackupServices _services;

		#region Applications Property
		private ObservableCollection<Application> _applications;
		public ObservableCollection<Application> Applications {
			get { return this._applications; }
			set {
				if (value != this._applications) {
					this._applications = value;
					this.RaisePropertyChangedEvent("Applications");
				}
			}
		}
		#endregion

		#region AllSelected Property
		private bool _allSelected;
		public bool AllSelected {
			get { return this._allSelected; }
			set {
				if (value != this._allSelected) {
					this._allSelected = value;
					this.RaisePropertyChangedEvent("AllSelected");
				}
			}
		}
		#endregion

		#region ResourcesAssemblies Property
		private bool _resourcesAssemblies;
		public bool ResourcesAssemblies {
			get { return this._resourcesAssemblies; }
			set {
				if (value != this._resourcesAssemblies) {
					this._resourcesAssemblies = value;
					this.RaisePropertyChangedEvent("ResourcesAssemblies");
				}
			}
		}
		#endregion

		#region ResourcesBindings Property
		private bool _resourcesBindings;
		public bool ResourcesBindings {
			get { return this._resourcesBindings; }
			set {
				if (value != this._resourcesBindings) {
					this._resourcesBindings = value;
					this.RaisePropertyChangedEvent("ResourcesBindings");
				}
			}
		}
		#endregion

		#region ResourcesWebDirectories Property
		private bool _resourcesWebDirectories;
		public bool ResourcesWebDirectories {
			get { return this._resourcesWebDirectories; }
			set {
				if (value != this._resourcesWebDirectories) {
					this._resourcesWebDirectories = value;
					this.RaisePropertyChangedEvent("ResourcesWebDirectories");
				}
			}
		}
		#endregion

		#region BindingsBackupPath Property
		private string _bindingsBackupPath;
		public string BindingsBackupPath {
			get { return this._bindingsBackupPath; }
			set {
				if (value != this._bindingsBackupPath) {
					this._bindingsBackupPath = value;
					this.RaisePropertyChangedEvent("BindingsBackupPath");
				}
			}
		}
		#endregion

		#region MsiBackupPath Property
		private string _msiBackupPath;
		public string MsiBackupPath {
			get { return this._msiBackupPath; }
			set {
				if (value != this._msiBackupPath) {
					this._msiBackupPath = value;
					this.RaisePropertyChangedEvent("MsiBackupPath");
				}
			}
		}
		#endregion

		public BackupViewModel() {
			var container = ServiceLocator.Current.GetInstance<IUnityContainer>();

			// Initilisation et enregistrement du service
			var connectionString = container.Resolve<string>("BztMgmtDb");
			container.RegisterInstance<IBackupServices>(
				new BackupServices(connectionString)
			);

			this._services = ServiceLocator
				.Current.GetInstance<IUnityContainer>()
				.Resolve<IBackupServices>();

			try {
				this.Applications = new ObservableCollection<Application>(this._services.GetAllApplications());

				this.BackupBindingsCmd = new RelayCommand<object>(this.ExecuteBackupBindingsCmd, this.CanExecuteBackupBindingsCmd);
				this.BackupMsiCmd = new RelayCommand<object>(this.ExecuteBackupMsiCmd, this.CanExecuteBackupMsiCmd);

				// récupération des valeurs par défaut dans le fichier de config de l'application
				this.BindingsBackupPath = ConfigurationManager.AppSettings.Get("Backup.DefaultBackupPath");
				this.MsiBackupPath = ConfigurationManager.AppSettings.Get("Backup.DefaultBackupPath");

				this.ResourcesBindings = bool.Parse(ConfigurationManager.AppSettings.Get("Backup.DefaultExportBindings"));
				this.ResourcesAssemblies = bool.Parse(ConfigurationManager.AppSettings.Get("Backup.DefaultExportAssemblies"));
				this.ResourcesWebDirectories = bool.Parse(ConfigurationManager.AppSettings.Get("Backup.DefaultExportWebDirectories"));
			}
			catch (Exception ex) {
				NotificationHelper.ShowError(ex);
			}
		}

		#region BackupBindingsCmd
		public RelayCommand<object> BackupBindingsCmd { get; set; }

		private bool CanExecuteBackupBindingsCmd(object param) {
			return param != null || this.AllSelected;
		}

		private void ExecuteBackupBindingsCmd(object param) {
			if (string.IsNullOrEmpty(this.BindingsBackupPath) || !Directory.Exists(this.BindingsBackupPath)) {
				NotificationHelper.WriteNotification("Chemin de backup invalide.");
			}
			else {
				var appList = this.AllSelected ? this._services.GetAllApplications() : (param as IEnumerable).Cast<Application>();
				NotificationHelper.WriteNotification(
					string.Format("Début du backup de {0} application(s).", appList.Count())
				);

				UIServices.SetBusyState();

				foreach (var app in appList) {
					NotificationHelper.WriteNotification("Export du binding pour " + app.Name + " dans " + this.BindingsBackupPath + "...");

					var trace = this._services.ExportBinding(app.Name, this.BindingsBackupPath);

					NotificationHelper.WriteNotification(trace);
				}
			}
		}
		#endregion

		#region BackupMsiCmd
		public RelayCommand<object> BackupMsiCmd { get; set; }

		private bool CanExecuteBackupMsiCmd(object param) {
			return param != null || this.AllSelected;
		}

		private void ExecuteBackupMsiCmd(object param) {
			if (string.IsNullOrEmpty(this.MsiBackupPath) || !Directory.Exists(this.MsiBackupPath)) {
				NotificationHelper.WriteNotification("Chemin de backup invalide.");
			}
			else {
				var appList = this.AllSelected ? this._services.GetAllApplications() : (param as IEnumerable).Cast<Application>();
				NotificationHelper.WriteNotification(
					string.Format("Début du backup de {0} application(s).", appList.Count())
				);

				UIServices.SetBusyState();

				foreach (var app in appList) {
					NotificationHelper.WriteNotification("Export de la liste des ressources pour " + app.Name + " dans " + this.MsiBackupPath + "...");

					// TODO export MSI avec le resourcespec

					string resourcesFileName;
					var trace = this._services.ExportResourceSpecFile(app.Name, this.MsiBackupPath, out resourcesFileName);
					NotificationHelper.WriteNotification(trace);

					this._services.ConfigResourcesSpecFile(
						Path.Combine(this.MsiBackupPath, resourcesFileName), 
						this.ResourcesBindings, 
						this.ResourcesAssemblies, 
						this.ResourcesWebDirectories
					);

					trace = this._services.ExportMsiWithResourcesFilter(app.Name, this.MsiBackupPath, Path.Combine(this.MsiBackupPath, resourcesFileName));
					NotificationHelper.WriteNotification(trace);
				}
			}
		}
		#endregion
	}
}
