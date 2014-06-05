using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace BztToolbox.Common.Utility
{
	/// <summary>
	/// Service permettant de définir un waitcursor lors d'une opération longue.
	/// </summary>
	/// <remarks>Implémentation trouvée sur : "http://stackoverflow.com/questions/7346663/how-to-show-a-waitcursor-when-the-wpf-application-is-busy-databinding"</remarks>
	public static class UIServices
	{
		private static bool isBusy;

		/// <summary>
		/// Méthode à appeler avant toute opération potentiellement longue
		/// </summary>
		public static void SetBusyState() {
			SetBusyState(true);
		}

		private static void SetBusyState(bool busy) {
			if (busy != isBusy) {
				isBusy = busy;
				Mouse.OverrideCursor = busy ? Cursors.Wait : null;

				if (isBusy) {
					new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.ApplicationIdle, dispatcherTimer_Tick, Application.Current.Dispatcher);
				}
			}
		}

		private static void dispatcherTimer_Tick(object sender, EventArgs e) {
			var dispatcherTimer = sender as DispatcherTimer;
			if (dispatcherTimer != null) {
				SetBusyState(false);
				dispatcherTimer.Stop();
			}
		}
	}
}
