﻿using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Runtime.Serialization;
using System.Text;
using Dev2.Communication;
using Dev2.DynamicServices;
using Dev2.DynamicServices.Objects;
using Dev2.Workspaces;

namespace Dev2.Runtime.ESB.Management.Services
{
    /// <summary>
    /// Check a user's credentials
    /// </summary>
    class CheckCredentials : IEsbManagementEndpoint
    {
        public StringBuilder Execute(Dictionary<string, StringBuilder> values, IWorkspace theWorkspace)
        {
            string domain = null;
            string username = null;
            string password = null;

            StringBuilder tmp;
            values.TryGetValue("Domain", out tmp);
            if (tmp != null)
            {
                domain = tmp.ToString();
            }

            values.TryGetValue("Username", out tmp);

            if (tmp != null)
            {
                username = tmp.ToString();
            }

            values.TryGetValue("Password", out tmp);

            if (tmp != null)
            {
                password = tmp.ToString();
            }


            if(string.IsNullOrEmpty(domain) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new InvalidDataContractException("Domain or Username or Password is missing");
            }

            var result = new ExecuteMessage {HasError = false};

            try
            {
                if(domain.Equals("."))
                {
                    domain = Environment.UserDomainName;
                }
                bool isValid;
                using(var context = new PrincipalContext(ContextType.Domain, domain))
                {
                    isValid = context.ValidateCredentials(username, password);

                    context.Dispose();
                }
                result.SetMessage(isValid ? "<result>Connection successful!</result>" : "<result>Connection failed. Ensure your username and password are valid</result>");
            }
            catch
            {
                result.SetMessage("<result>Connection failed. Ensure your username and password are valid</result>");
            }

            Dev2JsonSerializer serializer = new Dev2JsonSerializer();

            return serializer.SerializeToBuilder(result);
        }

        public DynamicService CreateServiceEntry()
        {
            var checkCredentialsService = new DynamicService
            {
                Name = HandlesType(),
                DataListSpecification = "<DataList><Domain/><Username/><Password/><Dev2System.ManagmentServicePayload ColumnIODirection=\"Both\"></Dev2System.ManagmentServicePayload></DataList>"
            };

            var checkCredentialsServiceAction = new ServiceAction
            {
                Name = HandlesType(),
                ActionType = enActionType.InvokeManagementDynamicService,
                SourceMethod = HandlesType()
            };

            checkCredentialsService.Actions.Add(checkCredentialsServiceAction);

            return checkCredentialsService;
        }

        public string HandlesType()
        {
            return "CheckCredentialsService";
        }
    }
}
