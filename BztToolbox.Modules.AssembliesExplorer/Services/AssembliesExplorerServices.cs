using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.BizTalk.ExplorerOM;
using System.Collections.ObjectModel;

namespace BztToolbox.Modules.AssembliesExplorer.Services
{
	public class AssembliesExplorerServices : IAssembliesExplorerServices
	{
		private BtsCatalogExplorer _catalog;

		public AssembliesExplorerServices(string connectionString) {
			this._catalog = new BtsCatalogExplorer();
			this._catalog.ConnectionString = connectionString;
		}
		
		#region IAssembliesExplorerServices Membres

		public System.Collections.ObjectModel.ObservableCollection<Microsoft.BizTalk.ExplorerOM.BtsAssembly> GetAllAssemblies() {
			return new ObservableCollection<BtsAssembly>(
				this._catalog.Assemblies
					.Cast<BtsAssembly>()
					.OrderBy(x => x.Name)
					.ThenBy(x => x.Version)
			);
		}

		#endregion
	}
}
