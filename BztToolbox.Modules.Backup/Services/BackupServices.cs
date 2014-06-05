using System.Collections.ObjectModel;
using System.Linq;
using BztToolbox.Common.Utility;
using Microsoft.BizTalk.ExplorerOM;
using System;
using BztToolbox.Modules.Backup.Constants;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace BztToolbox.Modules.Backup.Services
{
	public class BackupServices : IBackupServices
	{
		private BtsCatalogExplorer _catalog;

		public BackupServices(string connectionString) {
			this._catalog = new BtsCatalogExplorer();
			this._catalog.ConnectionString = connectionString;
		}

		#region IBackupServices Membres

		public ObservableCollection<Application> GetAllApplications() {
			return new ObservableCollection<Application>(
				this._catalog.Applications
					.Cast<Application>()
					.OrderBy(x => x.Name)
				);
		}

		public string ExportBinding(string appName, string backupPath) {
			string outFileName;
			return this.ExportApp(appName, backupPath, ExportType.Binding, out outFileName);
		}

		public string ExportMsi(string appName, string backupPath) {
			string outFileName;
			return this.ExportApp(appName, backupPath, ExportType.Msi, out outFileName);
		}

		public string ExportMsiWithResourcesFilter(string appName, string backupPath, string resourcesSpecFileName) {
			var fileName = this.GenerateExportFileName(appName, ExportType.MsiResourceSpec);
			var protectedFilePath = "\"" + backupPath + @"\" + fileName + "\"";
			var protectedAppName = "\"" + appName + "\"";
			var protectedResFileName = "\"" + resourcesSpecFileName + "\"";

			var cmdText = string.Format(
							this.GetCmdtext(ExportType.MsiResourceSpec),
							protectedAppName,
							protectedFilePath,
							protectedResFileName
						);

			string trace;
			ProcessHelper.ExecuteCommand(cmdText, out trace);

			return trace;
		}

		public string ExportResourceSpecFile(string appName, string backupPath, out string resourcesFileName) {
			return this.ExportApp(appName, backupPath, ExportType.ResourceSpec, out resourcesFileName);
		}

		public void ConfigResourcesSpecFile(string fileName, bool exportBindings, bool exportAssemblies, bool exportWebDirectories) {
			List<string> typesFilter = new List<string>();
			
			if (exportAssemblies) {
				typesFilter.Add("System.BizTalk:BizTalkAssembly");
			}
			if (exportBindings) {
				typesFilter.Add("System.BizTalk:BizTalkBinding");
			}
			if (exportWebDirectories) {
				typesFilter.Add("System.BizTalk:WebDirectory");
			}

			// recherche est suppression des noeuds dont le type n'est pas dans la liste des types à conserver
			var xdoc = XDocument.Load(fileName);

			xdoc.Root
				.Descendants()
				.Descendants()
				.Where(
					x => !typesFilter.Contains(x.FirstAttribute.Value)
				)
				.Remove();

			xdoc.Save(fileName);
		}
		#endregion

		private string ExportApp(string appName, string backupPath, ExportType type, out string fileName) {
			fileName = this.GenerateExportFileName(appName, type);
			var protectedFilePath = "\"" + backupPath + @"\" + fileName + "\"";
			var protectedAppName = "\"" + appName + "\"";

			var cmdText = string.Format(
							this.GetCmdtext(type),
							protectedAppName,
							protectedFilePath
						);

			string trace;
			ProcessHelper.ExecuteCommand(cmdText, out trace);

			return trace;
		}

		private string GenerateExportFileName(string appName, ExportType type) {
			var fileName = string.Format(
				"{0}_{1}",
				DateTime.Now.ToString("yyyyMmddHHmmssfff"),
				appName
			);

			switch (type) {
				case ExportType.Binding:
					fileName += ".xml";
					break;
				case ExportType.ResourceSpec:
					fileName += "_Res.xml";
					break;
				case ExportType.Msi:
				case ExportType.MsiResourceSpec:
					fileName += ".msi";
					break;
				default:
					throw new Exception("Type d'export non pris en charge : " + type.ToString());
			}

			return fileName;
		}

		private string GetCmdtext(ExportType type) {
			switch (type) {
				case ExportType.Binding:
					return "ExportBindings -ApplicationName:{0} -Destination:{1}";
				case ExportType.Msi:
					return "ExportApp -ApplicationName:{0} -Package:{1}";
				case ExportType.ResourceSpec:
					return "ListApp -ApplicationName:{0} -ResourceSpec:{1}";
				case ExportType.MsiResourceSpec:
					return "ExportApp -ApplicationName:{0} -Package:{1} -ResourceSpec:{2}";
				default:
					throw new Exception("Type d'export non pris en charge : " + type.ToString());
			}
		}
	}
}
