using System;
using System.Collections.ObjectModel;
using System.Linq;
using BztToolbox.Common.Utility;
using Microsoft.BizTalk.ExplorerOM;

namespace BztToolbox.Modules.FilterEditor.Services
{
	public class FilterEditorServices : IFilterEditorServices
	{
		private BtsCatalogExplorer _catalog;

		public FilterEditorServices(string connectionString) {
			this._catalog = new BtsCatalogExplorer();
			this._catalog.ConnectionString = connectionString;
		}
		
		#region IFilterEditorServices Membres

		public ObservableCollection<SendPort> GetAllSendPorts() {
			return new ObservableCollection<SendPort>(
				this._catalog.SendPorts
					.Cast<SendPort>()
					.OrderBy(x => x.Application.Name)
					.ThenBy(x => x.Name)
				);
		}

		public void SaveChanges() {
			try {
				this._catalog.SaveChanges();
			}
			catch (BtsException btex) {
				NotificationHelper.WriteNotification("FilterEdior - Unable to save, invalid filter. Error : " + btex.Message);
			}
			catch (Exception ex) {
				NotificationHelper.WriteNotification("FilterEdior - SaveChanges exception. Error : " + ex.Message);
				throw;
			}
		}

		public void RevertChanges() {
			try {
				this._catalog.DiscardChanges();
			}
			catch (Exception ex) {
				NotificationHelper.WriteNotification("FilterEdior - RevertChanges exception. Error : " + ex.Message);
				throw;
			}
		}

		#endregion
	}
}
