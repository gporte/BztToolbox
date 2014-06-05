using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using BztToolbox.Common.BaseClasses;
using BztToolbox.Common.Utility;
using BztToolbox.Modules.AssembliesExplorer.Services;
using Microsoft.BizTalk.ExplorerOM;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BztToolbox.Modules.AssembliesExplorer.ViewModels
{
	public class AssembliesExplorerViewModel : ViewModelBase
	{
		private IAssembliesExplorerServices _services;

		#region Items property
		private ICollectionView _items;
		public ObservableCollection<BtsAssembly> Items { get; private set; }
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

		public AssembliesExplorerViewModel() {
			var container = ServiceLocator.Current.GetInstance<IUnityContainer>();
			
			// Initialisation et enregistrement du service
			var connectionString = container.Resolve<string>("BztMgmtDb");
			container.RegisterInstance<IAssembliesExplorerServices>(
				new AssembliesExplorerServices(connectionString)
			);
			
			this._services = ServiceLocator
				.Current.GetInstance<IUnityContainer>()
				.Resolve<IAssembliesExplorerServices>();

			try {
				this.Items = new ObservableCollection<BtsAssembly>(this._services.GetAllAssemblies());
				this._items = CollectionViewSource.GetDefaultView(this.Items);
				this._items.Filter = x => string.IsNullOrEmpty(this.ItemsFilter) ? true : (((BtsAssembly)x).Name.ToUpper()).Contains(this.ItemsFilter.ToUpper());
			}
			catch (Exception ex) {
				NotificationHelper.ShowError(ex);
			}
		}
	}
}
