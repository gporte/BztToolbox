using System.Collections.ObjectModel;
using System.Linq;
using BztToolbox.Modules.PortDuplicator.Properties;
using Microsoft.BizTalk.ExplorerOM;
using System.Collections.Generic;

namespace BztToolbox.Modules.PortDuplicator.Services
{
	public class PortDuplicatorServices : IPortDuplicatorServices
	{
		private BtsCatalogExplorer _catalog;

		public PortDuplicatorServices(string connectionString) {
			this._catalog = new BtsCatalogExplorer();
			this._catalog.ConnectionString = connectionString;
		}
		
		#region IPortDuplicatorServices Membres
		public ObservableCollection<SendPort> GetAllSendPorts() {
			return new ObservableCollection<SendPort>(
				this._catalog.SendPorts
					.Cast<SendPort>()
					.OrderBy(x => x.Application.Name)
					.ThenBy(x => x.Name)
				);
		}

		public ObservableCollection<ReceiveLocation> GetAllreceiveLocations() {
			var rcvLocList = new List<ReceiveLocation>();

			foreach (var rcvPort in this._catalog.ReceivePorts.Cast<ReceivePort>()) {
				rcvLocList.AddRange(rcvPort.ReceiveLocations.Cast<ReceiveLocation>());
			}

			return new ObservableCollection<ReceiveLocation>(
				rcvLocList
					.OrderBy(x => x.ReceivePort.Application.Name)
					.ThenBy(x => x.ReceivePort.Name)
					.ThenBy(x => x.Name)
			);
		}

		public string DuplicateSendPort(SendPort originalSendPort) {
			var app = this._catalog.Applications.Cast<Application>().FirstOrDefault(x => x.Name == originalSendPort.Application.Name);

			// génération du nouveau nom de port
			var baseNewName = Resources.copyPrefix + originalSendPort.Name;
			var newName = baseNewName;
			var i = 1;
			while (app.SendPorts.Cast<SendPort>().Count(x => x.Name == newName) > 0) {
				newName = baseNewName + i;
				i++;
			}

			//create new send port
			var newSendPort = app.AddNewSendPort(false, originalSendPort.IsTwoWay);			
			newSendPort.Name = newName;
			newSendPort.Filter = originalSendPort.Filter;

			// maps
			foreach (Transform outboundTrans in originalSendPort.OutboundTransforms) {
				newSendPort.OutboundTransforms.Add(outboundTrans);
			}

			// config divers
			newSendPort.Priority = originalSendPort.Priority;
			newSendPort.OrderedDelivery = originalSendPort.OrderedDelivery;
			newSendPort.SendPipeline = originalSendPort.SendPipeline;
			newSendPort.Description = string.Format(Resources.TemplateDescription, originalSendPort.Name);

			// transport
			newSendPort.PrimaryTransport.TransportType = originalSendPort.PrimaryTransport.TransportType;
			newSendPort.PrimaryTransport.Address = originalSendPort.PrimaryTransport.Address;
			newSendPort.PrimaryTransport.OrderedDelivery = originalSendPort.PrimaryTransport.OrderedDelivery;
			newSendPort.PrimaryTransport.RetryCount = originalSendPort.PrimaryTransport.RetryCount;
			newSendPort.PrimaryTransport.RetryInterval = originalSendPort.PrimaryTransport.RetryInterval;
			newSendPort.PrimaryTransport.SendHandler = originalSendPort.PrimaryTransport.SendHandler;
			newSendPort.PrimaryTransport.TransportTypeData = originalSendPort.PrimaryTransport.TransportTypeData;

			// partie spécifique aux ports de sollicitation.réponse
			if (originalSendPort.IsTwoWay) {
				// maps inbound
				foreach (Transform inboundTrans in originalSendPort.InboundTransforms) {
					newSendPort.InboundTransforms.Add(inboundTrans);
				}

				// receive pipeline
				newSendPort.ReceivePipeline = originalSendPort.ReceivePipeline;
			}

			// enregistrement des modifications
			this._catalog.SaveChanges();

			return newSendPort.Name;
		}

		public string DuplicateReceiveLocation(ReceiveLocation originalReceiveLocation) {
			var rcvPort = this._catalog.ReceivePorts.Cast<ReceivePort>().FirstOrDefault(x => x.Name == originalReceiveLocation.ReceivePort.Name);

			// génération du nouveau nom de port
			var baseNewName = Resources.copyPrefix + originalReceiveLocation.Name;
			var newName = baseNewName;
			var i = 1;
			while (rcvPort.ReceiveLocations.Cast<ReceiveLocation>().Count(x => x.Name == newName) > 0) {
				newName = baseNewName + i;
				i++;
			}

			// création
			var newRcvLoc = rcvPort.AddNewReceiveLocation();
			newRcvLoc.Name = newName;

			// config
			newRcvLoc.ReceiveHandler = originalReceiveLocation.ReceiveHandler;
			newRcvLoc.ReceivePipeline = originalReceiveLocation.ReceivePipeline;
			newRcvLoc.Description = string.Format(Resources.TemplateDescription, originalReceiveLocation.Name);
			newRcvLoc.Address = originalReceiveLocation.Address + Resources.rcvAdressSuffix;

			// transport
			newRcvLoc.TransportType = originalReceiveLocation.TransportType;
			newRcvLoc.TransportTypeData = originalReceiveLocation.TransportTypeData;

			// enregistrement des modifications
			this._catalog.SaveChanges();

			return newRcvLoc.Name;
		}

		#endregion
	}
}
