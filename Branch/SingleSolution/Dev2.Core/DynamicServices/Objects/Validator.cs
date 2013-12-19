﻿#region Change Log
//  Author:         Sameer Chunilall
//  Date:           2010-01-24
//  Log No:         9299
//  Description:    The Validator type contains metadata on the type of validation that needs to occur on an input
//                  The supported type of validation are Required, Regex and RequiredAndRegex.
//                  This type is wrapped within the Unlimited.Framework.DynamicServices.Input type
//                  
//                  
//                  
#endregion

using Dev2.DynamicServices.Objects.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dev2.DynamicServices {
    #region Using Directives

    #endregion

    #region Validator Class - Represents a validator that can validate any Service Action Input
    /// <summary>
    /// Provides a representation of a validator.
    /// Describes the types of validation that can occur
    /// </summary>
    public class Validator : DynamicServiceObjectBase{

        public Validator() : base( enDynamicServiceObjectType.Validator) {

        }

        #region Public Properties
        /// <summary>
        /// The type of validation required
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enValidationType ValidatorType { get; set; }

        #endregion

        public override bool Compile() {
            return true;
        }
    }
    #endregion
}