using System.Windows;

namespace BztToolbox
{
	/// <summary>
	/// Logique d'interaction pour App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App() {
			var bootstrapper = new Bootstrapper();
			bootstrapper.Run();
		}
	}
}
