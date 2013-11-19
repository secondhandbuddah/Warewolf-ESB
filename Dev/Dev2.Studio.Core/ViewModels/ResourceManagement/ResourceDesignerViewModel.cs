﻿using System;
using System.Text;
using Dev2.Studio.Core.AppResources.Enums;
using Dev2.Studio.Core.Interfaces;
using Dev2.Studio.Core.ViewModels.Base;

namespace Dev2.Studio.Core.ViewModels
{
    public class ResourceDesignerViewModel : SimpleBaseViewModel, IDisposable, IDesignerViewModel
    {
        #region Class Members

        private IEnvironmentModel _environmentModel = null;
        private IContextualResourceModel _contexttualResourceModel;

        #endregion Class Members

        #region Ctor

        public ResourceDesignerViewModel(IContextualResourceModel model, IEnvironmentModel environmentModel)
        {
            _contexttualResourceModel = model;
            _environmentModel = environmentModel;
        }

        #endregion

        #region Properties

        public IEnvironmentModel EnvironmentModel
        {
            get
            {
                return _environmentModel;
            }
        }

        public string ServiceDefinition
        {
            get
            {
                if(string.IsNullOrEmpty(_contexttualResourceModel.WorkflowXaml))
                {
                    _contexttualResourceModel.WorkflowXaml = DefaultDefinition();
                }

                return _contexttualResourceModel.WorkflowXaml;
            }
            set
            {
                _contexttualResourceModel.WorkflowXaml = value;
                NotifyOfPropertyChange(() => ServiceDefinition);
                if(ResourceModel != null) ResourceModel.WorkflowXaml = ServiceDefinition;
            }

        }

        public IContextualResourceModel ResourceModel
        {

            get { return _contexttualResourceModel; }
            set { _contexttualResourceModel = value; }
        }

        #endregion

        #region Methods

        private string DefaultDefinition()
        {

            var sb = new StringBuilder();

            switch(_contexttualResourceModel.ResourceType)
            {
                case ResourceType.Service:
                    sb.Append(string.Format("<Service Name=\"{0}\">",
                    _contexttualResourceModel.ResourceName));
                    sb.Append("\r\n\t\t");
                    sb.Append("<Actions>");
                    sb.Append("\r\n\t\t\t");
                    sb.Append("<Action Name=\"\" Type=\"\" SourceName=\"\" SourceMethod=\"\">");
                    sb.Append("\r\n\t\t\t\t");
                    sb.Append("<Input Name=\"\" Source=\"\">");
                    sb.Append("\r\n\t\t\t\t\t");
                    sb.Append("<Validator Type=\"Required\" />");
                    sb.Append("\r\n\t\t\t\t");
                    sb.Append("</Input>");
                    sb.Append("\r\n\t\t\t");
                    sb.Append("</Action>");
                    sb.Append("\r\n\t\t");
                    sb.Append("</Actions>");
                    sb.Append("\r\n\t");
                    sb.Append("</Service>");

                    break;

                case ResourceType.Source:
                    sb.Append(string.Format("<Source Name=\"{0}\" Type=\"\" ConnectionString=\"\" AssemblyName=\"\" AssemblyLocation=\"\" Uri=\"\" /> ", _contexttualResourceModel.ResourceName));
                    break;

                default:
                    throw new ArgumentException("Unexpected Resource Type");

            }

            return sb.ToString();
        }

        void IDisposable.Dispose()
        {

        }

        #endregion Methods
    }
}
