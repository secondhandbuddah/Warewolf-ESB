﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dev2.Common;
using Dev2.Common.Interfaces.Core.Graph;
using Dev2.Runtime.ServiceModel.Data;

namespace Dev2.Runtime.ServiceModel.Utils
{
    /// <summary>
    /// Make Service Mapping Operations Testable ;)
    /// </summary>
    public class ServiceMappingHelper
    {

        /// <summary>
        /// Maps the database outputs.
        /// </summary>
        /// <param name="outputDescription">The output description.</param>
        /// <param name="theService">The service.</param>
        /// <param name="addFields">if set to <c>true</c> [add fields].</param>
        public void MapDbOutputs(IOutputDescription outputDescription, ref DbService theService, bool addFields)
        {

            // only fetch paths with valid data to map ;)
            var outputsToMap = outputDescription.DataSourceShapes[0].Paths.Where(p => (!string.IsNullOrEmpty(p.DisplayPath) && p.DisplayPath != "DocumentElement"));
            
            var rsFields = new List<RecordsetField>(theService.Recordset.Fields);
            #pragma warning disable 219
            int recordsetIndex = 0;
            #pragma warning restore 219

            foreach(var path in outputsToMap)
            {
                // Remove bogus names and dots
                var name = path.DisplayPath.Replace("NewDataSet", "").Replace(".Table.", "").Replace("DocumentElement","");
                var alias = path.DisplayPath.Replace("NewDataSet", "").Replace(".Table.", "").Replace(".","").Replace("DocumentElement", "");

                var idx = name.IndexOf("()", StringComparison.InvariantCultureIgnoreCase);
                if(idx >= 0)
                {
                    name = name.Remove(0, idx + 2);
                }

                var field = new RecordsetField { Name = name, Alias = string.IsNullOrEmpty(path.OutputExpression) ? alias : path.OutputExpression, Path = path };

                RecordsetField rsField;
                if(!addFields && (rsField = rsFields.FirstOrDefault(f => f.Path != null ? f.Path.ActualPath == path.ActualPath : f.Name == field.Name)) != null)
                {
                    field.Alias = rsField.Alias;
                }

                theService.Recordset.Fields.Add(field);

                // 2013.12.11 - COMMUNITY BUG - 341463 - data with empty cells displays incorrectly
                var data = path.SampleData.Split(new[] { GlobalConstants.AnytingToXmlCommaToken }, StringSplitOptions.None);
                var recordIndex = 0;

                foreach (var item in data)
                {
                    theService.Recordset.SetValue(recordIndex, recordsetIndex, item);
                    recordIndex++;
                }

                recordsetIndex++;
            }

        }
    }
}
