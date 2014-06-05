using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using BztToolbox.Common.BaseClasses;
using BztToolbox.Common.Utility;
using BztToolbox.Modules.FilterEditor.Commands;
using BztToolbox.Modules.FilterEditor.Services;
using Microsoft.BizTalk.ExplorerOM;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BztToolbox.Modules.FilterEditor.ViewModels
{
	public class FilterEditorViewModel : ViewModelBase
	{
		private IFilterEditorServices _services;
		
		#region Items property
		private ICollectionView _items;
		public ObservableCollection<SendPort> Items { get; private set; }
		#endregion

		#region ItemFilter property
		private string _itemsFilter;
		public string ItemsFilter {
			get { return this._itemsFilter; }
			set {
				if (this._itemsFilter != value) {
					this._itemsFilter = value;
					this._items.Refresh();
					this.RaisePropertyChangedEvent("ItemsFilter");
				}
			}
		}
		#endregion

		#region Commands
		public CopyFilterCommand CopyFilterCmd { get; set; }
		public ICommand SaveChangesCmd { get; set; }
		public ICommand RevertChangesCmd { get; set; }
		#endregion

		public FilterEditorViewModel() {
			var container = ServiceLocator.Current.GetInstance<IUnityContainer>();

			// Initilisation et enregistrement du service
			var connectionString = container.Resolve<string>("BztMgmtDb");
			container.RegisterInstance<IFilterEditorServices>(
				new FilterEditorServices(connectionString)
			);

			this._services = ServiceLocator
				.Current.GetInstance<IUnityContainer>()
				.Resolve<IFilterEditorServices>();
			
			try {
				this.Items = new ObservableCollection<SendPort>(this._services.GetAllSendPorts());
				this._items = CollectionViewSource.GetDefaultView(this.Items);
				this._items.Filter = x => string.IsNullOrEmpty(this.ItemsFilter) ? true : (((SendPort)x).Application.Name + ((SendPort)x).Name.ToUpper()).Contains(this.ItemsFilter.ToUpper());

				// commandes
				this.CopyFilterCmd = new CopyFilterCommand();
				this.SaveChangesCmd = new SaveChangesCommand();
				this.RevertChangesCmd = new RevertChangesCommand();
			}
			catch (Exception ex) {
				NotificationHelper.ShowError(ex);
			}
		}
	}
}
