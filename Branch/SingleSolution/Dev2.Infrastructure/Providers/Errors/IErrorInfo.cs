﻿using System;

namespace Dev2.Providers.Errors
{
    public interface IErrorInfo
    {
        Guid InstanceID { get; set; }
        string Message { get; set; }
        string StackTrace { get; set; }
        ErrorType ErrorType { get; set; }
        FixType FixType { get; set; }
        string FixData { get; set; }
    }
}