using System.Collections.ObjectModel;
using Microsoft.BizTalk.ExplorerOM;

namespace BztToolbox.Modules.FilterEditor.Services
{
	public interface IFilterEditorServices
	{
		ObservableCollection<SendPort> GetAllSendPorts();
		void SaveChanges();
		void RevertChanges();
	}
}
