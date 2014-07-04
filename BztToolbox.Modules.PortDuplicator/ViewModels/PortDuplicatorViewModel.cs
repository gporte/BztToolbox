using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using BztToolbox.Common.BaseClasses;
using BztToolbox.Common.Utility;
using BztToolbox.Modules.PortDuplicator.Commands;
using BztToolbox.Modules.PortDuplicator.Services;
using Microsoft.BizTalk.ExplorerOM;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BztToolbox.Modules.PortDuplicator.ViewModels
{
	public class PortDuplicatorViewModel : ViewModelBase
	{
		private IPortDuplicatorServices _services;

		#region SendPorts property
		private ICollectionView _sendPorts;
		public ObservableCollection<SendPort> SendPorts { get; private set; }
		#endregion

		#region SndFilter property
		private string _sndFilter;
		public string SndFilter {
			get { return this._sndFilter; }
			set {
				if (this._sndFilter != value) {
					this._sndFilter = value;
					this._sendPorts.Refresh();
					this.RaisePropertyChangedEvent("SndFilter");
				}
			}
		}
		#endregion

		#region ReceiveLocations property
		private ICollectionView _receiveLocations;
		public ObservableCollection<ReceiveLocation> ReceiveLocations { get; private set; }
		#endregion

		#region RcvLocFilter property
		private string _rcvLocFilter;
		public string RcvLocFilter {
			get { return this._rcvLocFilter; }
			set {
				if (this._rcvLocFilter != value) {
					this._rcvLocFilter = value;
					this._receiveLocations.Refresh();
					this.RaisePropertyChangedEvent("RcvLocFilter");
				}
			}
		}
		#endregion

		#region Commands
		public DuplicateSendPortCommand DuplicateSendPortCmd { get; set; }
		public DuplicateReceiveLocationCommand DuplicateRcvLocCmd { get; set; }
		#endregion

		public PortDuplicatorViewModel() {
			var container = ServiceLocator.Current.GetInstance<IUnityContainer>();

			// Initilisation et enregistrement du service
			var connectionString = container.Resolve<string>("BztMgmtDb");
			container.RegisterInstance<IPortDuplicatorServices>(
				new PortDuplicatorServices(connectionString)
			);

			this._services = ServiceLocator
				.Current.GetInstance<IUnityContainer>()
				.Resolve<IPortDuplicatorServices>();

			try {
				// initialisation de la liste de send ports
				this.SendPorts = new ObservableCollection<SendPort>(this._services.GetAllSendPorts());
				this._sendPorts = CollectionViewSource.GetDefaultView(this.SendPorts);
				this._sendPorts.Filter = x => string.IsNullOrEmpty(this.SndFilter) ? true : (((SendPort)x).Application.Name + ((SendPort)x).Name.ToUpper()).Contains(this.SndFilter.ToUpper());

				// initialisation de la liste de receive locations
				this.ReceiveLocations = new ObservableCollection<ReceiveLocation>(this._services.GetAllreceiveLocations());
				this._receiveLocations = CollectionViewSource.GetDefaultView(this.ReceiveLocations);
				this._receiveLocations.Filter =
					x => string.IsNullOrEmpty(this.RcvLocFilter)
					? true
					: (((ReceiveLocation)x).ReceivePort.Application.Name + ((ReceiveLocation)x).ReceivePort.Name + ((ReceiveLocation)x).Name.ToUpper()).Contains(this.RcvLocFilter.ToUpper());

				// commandes
				this.DuplicateSendPortCmd = new DuplicateSendPortCommand(this.DuplicateCallBack);
				this.DuplicateRcvLocCmd = new DuplicateReceiveLocationCommand(this.DuplicateCallBack);
			}
			catch (Exception ex) {
				NotificationHelper.ShowError(ex);
			}
		}

		private void DuplicateCallBack() {
			// On efface la liste de send port et on la réalimente avec la liste mise à jour
			this.SendPorts.Clear();
			foreach (var item in this._services.GetAllSendPorts()) {
				this.SendPorts.Add(item);
			}

			// idem pour les receivelocations
			this.ReceiveLocations.Clear();
			foreach (var item in this._services.GetAllreceiveLocations()) {
				this.ReceiveLocations.Add(item);
			}
		}
	}
}
