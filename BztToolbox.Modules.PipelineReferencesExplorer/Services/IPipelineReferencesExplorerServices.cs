using System.Collections.ObjectModel;
using Microsoft.BizTalk.ExplorerOM;

namespace BztToolbox.Modules.PipelineReferencesExplorer.Services
{
	public interface IPipelineReferencesExplorerServices
	{
		ObservableCollection<Pipeline> GetAllReceivePipelines();
		ObservableCollection<Pipeline> GetAllSendPipelines();
		ObservableCollection<Pipeline> GetAllPipelinesByType(PipelineType type);
		ObservableCollection<SendPort> GetSndPortByPipeline(Pipeline pipeline);
		ObservableCollection<ReceiveLocation> GetRcvLocByPipeline(Pipeline pipeline);
	}
}
