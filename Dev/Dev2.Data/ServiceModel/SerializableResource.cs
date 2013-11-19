﻿using System;
using System.Collections.Generic;
using Dev2.Providers.Errors;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dev2.Data.ServiceModel
{

    /// <summary>
    /// Light weight resource to ship to the studio ;)
    /// </summary>
    public class SerializableResource
    {
        /// <summary>
        /// The resource ID that uniquely identifies the resource.
        /// </summary>
        public Guid ResourceID { get; set; }

        /// <summary>
        /// The version that uniquely identifies the resource.
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// The display name of the resource.
        /// </summary>
        public string ResourceName { get; set; }

        /// <summary>
        /// Gets or sets the type of the resource.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ResourceType ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the category of the resource.
        /// </summary>   
        public string ResourceCategory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [is valid].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is valid]; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid { get; set; }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public List<ErrorInfo> Errors { get; set; }

        /// <summary>
        /// Gets or sets the data list.
        /// </summary>
        /// <value>
        /// The data list.
        /// </value>
        public string DataList { get; set; }

    }
}
