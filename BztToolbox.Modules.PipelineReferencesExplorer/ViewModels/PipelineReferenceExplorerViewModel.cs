using System.Collections.ObjectModel;
using BztToolbox.Common.BaseClasses;
using BztToolbox.Common.Commands;
using BztToolbox.Modules.PipelineReferencesExplorer.Services;
using Microsoft.BizTalk.ExplorerOM;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BztToolbox.Modules.PipelineReferencesExplorer.ViewModels
{
	public class PipelineReferenceExplorerViewModel : ViewModelBase
	{
		private IPipelineReferencesExplorerServices _service;

		#region Pipelines
		private ObservableCollection<Pipeline> _pipelines;
		public ObservableCollection<Pipeline> Pipelines {
			get { return this._pipelines; }
			set {
				if (value != this._pipelines) {
					this._pipelines = value;
					this.RaisePropertyChangedEvent("Pipelines");
				}
			}
		}
		#endregion

		#region ReceiveLocations
		private ObservableCollection<ReceiveLocation> _receiveLocations;
		public ObservableCollection<ReceiveLocation> ReceiveLocations {
			get { return this._receiveLocations; }
			set {
				if (value != this._receiveLocations) {
					this._receiveLocations = value;
					this.RaisePropertyChangedEvent("ReceiveLocations");
				}
			}
		}
		#endregion

		#region SendPorts
		private ObservableCollection<SendPort> _sendPorts;
		public ObservableCollection<SendPort> SendPorts {
			get { return this._sendPorts; }
			set {
				if (value != this._sendPorts) {
					this._sendPorts = value;
					this.RaisePropertyChangedEvent("SendPorts");
				}
			}
		}
		#endregion

		public PipelineReferenceExplorerViewModel() {
			var container = ServiceLocator.Current.GetInstance<IUnityContainer>();

			// Initilisation et enregistrement du service
			var connectionString = container.Resolve<string>("BztMgmtDb");
			container.RegisterInstance<IPipelineReferencesExplorerServices>(
				new PipelineReferencesExplorerServices(connectionString)
			);
			
			this._service = ServiceLocator
				.Current.GetInstance<IUnityContainer>()
				.Resolve<IPipelineReferencesExplorerServices>();

			this.GetPipelinesCmd = new RelayCommand<int>(this.ExecuteGetPipelineCmd);
			this.SearchReferencesCommand = new RelayCommand<Pipeline>(this.ExecuteSearchReferencesCommand);
		}

		#region GetPipelinesCmd
		public RelayCommand<int> GetPipelinesCmd { get; set; }

		private void ExecuteGetPipelineCmd(int parameter) {
			this.Pipelines = this._service.GetAllPipelinesByType((PipelineType)parameter);
		}
		#endregion

		#region SearchReferencesCommand
		public RelayCommand<Pipeline> SearchReferencesCommand { get; set; }

		public void ExecuteSearchReferencesCommand(Pipeline pipeline) {
			this.ReceiveLocations = this._service.GetRcvLocByPipeline(pipeline);
			this.SendPorts = this._service.GetSndPortByPipeline(pipeline);
		}
		#endregion
	}
}
