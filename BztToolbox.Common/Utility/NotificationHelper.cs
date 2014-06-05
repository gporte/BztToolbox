using System;
using System.Collections.Generic;
using BztToolbox.Common.Events;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BztToolbox.Common.Utility
{
	public static class NotificationHelper
	{
		public static void WriteNotification(string message) {
			NotificationHelper.WriteNotification(message, false);
		}
		
		public static void WriteNotification(string message, bool clearBefore) {
			ServiceLocator
				.Current.GetInstance<IUnityContainer>()
				.Resolve<IEventAggregator>()
				.GetEvent<NotificationEvent>()
				.Publish(new UserNotification(message, clearBefore));
		}

		public static void WriteNotificationList(List<string> messagesList) {
			NotificationHelper.WriteNotificationList(messagesList, false);
		}

		public static void WriteNotificationList(List<string> messagesList, bool clearBefore) {
			var message = string.Join(" ", messagesList.ToArray());
			WriteNotification(message, clearBefore);
		}

		public static void ShowError(Exception ex) {
			// envoyer un message au shell pour qu'il affiche l'exception comme il veut
			// TODO voir si on peut enrichir les infos à partir de l'exception
			ServiceLocator
				.Current.GetInstance<IUnityContainer>()
				.Resolve<IEventAggregator>()
				.GetEvent<ShowErrorEvent>()
				.Publish(ex.Message);	
		}
	}
}
