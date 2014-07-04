using System.Collections.ObjectModel;
using Microsoft.BizTalk.ExplorerOM;

namespace BztToolbox.Modules.PortDuplicator.Services
{
	public interface IPortDuplicatorServices
	{
		ObservableCollection<SendPort> GetAllSendPorts();
		ObservableCollection<ReceiveLocation> GetAllreceiveLocations();

		/// <summary>
		/// Duplique le SendPort passé en paramètre.
		/// </summary>
		/// <param name="originalSendPort">SendPort à dupliquer.</param>
		/// <returns>Nom de la copie.</returns>
		string DuplicateSendPort(SendPort originalSendPort);

		/// <summary>
		/// Duplique la receive location passée en paramètre.
		/// </summary>
		/// <param name="originalReceiveLocation">Receive location à dupliquer</param>
		/// <returns>Nom de la copie.</returns>
		string DuplicateReceiveLocation(ReceiveLocation originalReceiveLocation);
	}
}
