using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.BizTalk.ExplorerOM;
using System.Collections.Generic;

namespace BztToolbox.Modules.PipelineReferencesExplorer.Services
{
	public class PipelineReferencesExplorerServices : IPipelineReferencesExplorerServices
	{
		private BtsCatalogExplorer _catalog;

		public PipelineReferencesExplorerServices(string connectionString) {
			this._catalog = new BtsCatalogExplorer();
			this._catalog.ConnectionString = connectionString;
		}

		#region IPipelineReferencesExplorerServices Membres

		public ObservableCollection<Pipeline> GetAllReceivePipelines() {
			return this.GetAllPipelinesByType(PipelineType.Receive);
		}

		public ObservableCollection<Pipeline> GetAllSendPipelines() {
			return this.GetAllPipelinesByType(PipelineType.Send);
		}

		public ObservableCollection<SendPort> GetSndPortByPipeline(Pipeline pipeline) {
			if (pipeline.Type == PipelineType.Receive) {
				return new ObservableCollection<SendPort>(
					this._catalog.SendPorts
					.Cast<SendPort>()
					.Where(x => x != null && x.ReceivePipeline != null && x.ReceivePipeline.FullName == pipeline.FullName)
					.OrderBy(x => x.Application.Name)
					.ThenBy(x => x.Name)
				);
			}
			else { // send pipeline
				return new ObservableCollection<SendPort>(
					this._catalog.SendPorts
					.Cast<SendPort>()
					.Where(x => x != null && x.ReceivePipeline != null && x.SendPipeline.FullName == pipeline.FullName)
					.OrderBy(x => x.Application.Name)
					.ThenBy(x => x.Name)
				);
			}
		}

		public ObservableCollection<Pipeline> GetAllPipelinesByType(PipelineType type) {
			return new ObservableCollection<Pipeline>(
				this._catalog.Pipelines
				.Cast<Pipeline>()
				.Where(x => x.Type == type)
				.OrderBy(x => x.FullName)
			);
		}

		public ObservableCollection<ReceiveLocation> GetRcvLocByPipeline(Pipeline pipeline) {
			var rcvLocList = new List<ReceiveLocation>();
			
			if (pipeline.Type == PipelineType.Receive) {
				foreach (var rcvPort in this._catalog.ReceivePorts.Cast<ReceivePort>()) {
					foreach (var rcvLoc in rcvPort.ReceiveLocations.Cast<ReceiveLocation>()) {
						if (rcvLoc.ReceivePipeline != null && rcvLoc.ReceivePipeline.FullName == pipeline.FullName) {
							rcvLocList.Add(rcvLoc);
						}
					}
				}
			}
			else { // send pipeline
				foreach (var rcvPort in this._catalog.ReceivePorts.Cast<ReceivePort>()) {
					foreach (var rcvLoc in rcvPort.ReceiveLocations.Cast<ReceiveLocation>()) {
						if (rcvLoc.SendPipeline != null && rcvLoc.SendPipeline.FullName == pipeline.FullName) {
							rcvLocList.Add(rcvLoc);
						}
					}
				}
			}

			return new ObservableCollection<ReceiveLocation>(rcvLocList);
		}		
		#endregion
	}
}
