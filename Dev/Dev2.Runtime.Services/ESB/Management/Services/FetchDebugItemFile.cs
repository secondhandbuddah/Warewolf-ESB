﻿using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Dev2.DynamicServices;
using Dev2.Providers.Logs;
using Dev2.Workspaces;
using Newtonsoft.Json;

namespace Dev2.Runtime.ESB.Management.Services
{
    public class FetchDebugItemFile : IEsbManagementEndpoint
    {

        public string Execute(IDictionary<string, string> values, IWorkspace theWorkspace)
        {
            string debugItemFilePath;

            if (values == null)
            {
                Logger.TraceInfo("values are missing");
                throw new InvalidDataContractException("values are missing");
            }

            if (!values.TryGetValue("DebugItemFilePath", out debugItemFilePath))
            {
                Logger.TraceInfo("DebugItemFilePath is missing");
                throw new InvalidDataContractException("DebugItemFilePath is missing");
            }

            if (File.Exists(debugItemFilePath))
            {
                Logger.TraceInfo("DebugItemFilePath found");
                StringBuilder result = new StringBuilder("<JSON>");
                var logData = File.ReadAllText(debugItemFilePath);
                result.Append(JsonConvert.SerializeObject(logData));
                result.Append("</JSON>");
                return result.ToString();
            }

            Logger.TraceInfo("DebugItemFilePath not found, throwing an exception");
            throw new InvalidDataContractException(string.Format("DebugItemFilePath {0} not found", debugItemFilePath));
        }

        public DynamicService CreateServiceEntry()
        {
            var findDirectoryService = new DynamicService
            {
                Name = HandlesType(),
                DataListSpecification = "<DataList><DebugItemFilePath ColumnIODirection=\"Input\"></DebugItemFilePath><Dev2System.ManagmentServicePayload ColumnIODirection=\"Both\"></Dev2System.ManagmentServicePayload></DataList>"
            };

            var findDirectoryServiceAction = new ServiceAction
            {
                Name = HandlesType(),
                ActionType = enActionType.InvokeManagementDynamicService,
                SourceMethod = HandlesType()
            };

            findDirectoryService.Actions.Add(findDirectoryServiceAction);

            return findDirectoryService;
        }

        public string HandlesType()
        {
            return "FetchDebugItemFileService";
        }
    }
}