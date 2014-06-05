using System;
using BztToolbox.Common.BaseClasses;
using BztToolbox.Common.Commands;
using BztToolbox.Common.Events;
using BztToolbox.Common.Utility;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BztToolbox.ViewModels
{
	public class ShellViewModel : ViewModelBase
	{
		#region Commands
		public ExitCommand ExitCmd { get; set; }
		#endregion

		#region NotificationsProperty
		private string _notifications;
		public string Notifications {
			get { return this._notifications; }
			set {
				if (this._notifications != value) {
					this._notifications = value;
					this.RaisePropertyChangedEvent("Notifications");
				}
			}
		}
		#endregion

		private IEventAggregator _aggregator;

		public ShellViewModel() {
			this._aggregator = ServiceLocator
				.Current.GetInstance<IUnityContainer>()
				.Resolve<IEventAggregator>();

			this._aggregator.GetEvent<NotificationEvent>().Subscribe(this.WriteNotification);

			this.ExitCmd = new ExitCommand();
		}

		private void WriteNotification(UserNotification notification) {
			if (notification.ClearBefore) {
				this.Notifications = string.Empty;
			}

			//this.Notifications = notification.FormattedMessage + Environment.NewLine + this.Notifications;
			this.Notifications = this.Notifications + Environment.NewLine + notification.FormattedMessage;
		}
	}
}
