﻿using System.Collections.Generic;
using System.Linq;
using Dev2.Data.ServiceModel;
using Dev2.DynamicServices;
using Dev2.Runtime.Hosting;
using Dev2.Workspaces;
using Newtonsoft.Json;

namespace Dev2.Runtime.ESB.Management.Services
{
    /// <summary>
    /// Find resources in the service catalog
    /// </summary>
    public class FindResource : IEsbManagementEndpoint
    {
        public string Execute(IDictionary<string, string> values, IWorkspace theWorkspace)
        {
            string resourceName;
            string type;
            string roles;

            values.TryGetValue("ResourceName", out resourceName);
            values.TryGetValue("ResourceType", out type);
            values.TryGetValue("Roles", out roles);

            // BUG 7850 - TWR - 2013.03.11 - ResourceCatalog refactor
            var resources = ResourceCatalog.Instance.GetResourceList(theWorkspace.ID, resourceName, type, roles);

            IList<SerializableResource> resourceList = resources.Select(new FindResourceHelper().SerializeResourceForStudio).ToList();

            var result = JsonConvert.SerializeObject(resourceList);

            return result;
        }

        public DynamicService CreateServiceEntry()
        {
            var findServices = new DynamicService { Name = HandlesType(), DataListSpecification = "<DataList><ResourceType/><Roles/><ResourceName/><Dev2System.ManagmentServicePayload ColumnIODirection=\"Both\"></Dev2System.ManagmentServicePayload></DataList>" };

            var findServiceAction = new ServiceAction { Name = HandlesType(), ActionType = enActionType.InvokeManagementDynamicService, SourceMethod = HandlesType() };

            findServices.Actions.Add(findServiceAction);

            return findServices;
        }

        public string HandlesType()
        {
            return "FindResourceService";
        }
    }
}
