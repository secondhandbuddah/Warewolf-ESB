﻿using System;
using System.Activities.Presentation;
using System.Activities.Presentation.Services;
using System.Collections.Generic;
using System.Windows.Input;
using Dev2.Studio.Core.Interfaces;
using Dev2.Studio.ViewModels.Workflow;
using Dev2.Studio.Views.Workflow;
using Dev2.Utilities;

namespace Dev2.Core.Tests.Workflows
{
    public class TestWorkflowDesignerViewModel : WorkflowDesignerViewModel
    {
        public TestWorkflowDesignerViewModel(IContextualResourceModel resource, IWorkflowHelper workflowHelper, bool createDesigner = true)
            : base(resource, workflowHelper, createDesigner)
        {
            _wd = new WorkflowDesigner();
        }

        public void TestModelServiceModelChanged(ModelChangedEventArgs e)
        {
            base.ModelServiceModelChanged(null, e);
        }

        public void TestWorkflowDesignerModelChanged()
        {
            base.WdOnModelChanged(new object(), new EventArgs());
        }

        public WorkflowDesigner Wd
        {
            get
            {
                return new WorkflowDesigner();
            }
            set
            {
                _wd = value;
            }
        }
        
        public void SetDataObject(dynamic dataobject)
        {
            DataObject = dataobject;
        }

        public void TestWorkflowDesignerModelChangedWithNullSender()
        {
            base.WdOnModelChanged(null, new EventArgs());
        }
    }
}
