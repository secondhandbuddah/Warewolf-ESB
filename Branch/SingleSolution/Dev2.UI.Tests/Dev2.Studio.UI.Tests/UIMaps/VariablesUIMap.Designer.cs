﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by coded UI test builder.
//      Version: 11.0.0.0
//
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------

using Dev2.Studio.UI.Tests.Utils;

namespace Dev2.CodedUI.Tests.UIMaps.VariablesUIMapClasses
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Input;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    
    
    [GeneratedCode("Coded UITest Builder", "11.0.51106.1")]
    public partial class VariablesUIMap
    {
        private UITestControl _variableExplorer;

        VisualTreeWalker vtw = new VisualTreeWalker();

        public VariablesUIMap()
        {
            _variableExplorer = vtw.GetControlFromRoot(1, true, "Uia.DataListView");
        }

        private UITestControl GetUpdateButton()
        {
            UITestControl theControl = this.UIBusinessDesignStudioWindow.UIItemCustom;
            theControl.Find();
            UITestControl updateButton = new UITestControl(theControl);
            updateButton.SearchProperties[WpfTree.PropertyNames.AutomationId] = "UI_AddRemovebtn_AutoID";
            updateButton.Find();
            return updateButton;
        }

        private UITestControl GetScalarVariables()
        {
            return vtw.GetChildByAutomationIDPath(_variableExplorer, "ScalarExplorer");
        }

        private UITestControl GetRecordSetList()
        {
            UITestControl theControl = _variableExplorer;
            return theControl.GetChildren()[1];
        }

        public UITestControlCollection GetScalarVariableList()
        {
            UITestControl variableMenu = GetScalarVariables();

            var kids = variableMenu.GetChildren();

            if (kids != null)
            {
                UITestControlCollection variableList = kids[0].GetChildren();
                UITestControlCollection returnList = new UITestControlCollection();
                foreach (UITestControl theItem in variableList)
                {
                    if (theItem.ControlType.Name == "TreeItem")
                    {
                        returnList.Add(theItem);
                    }
                }

                return returnList;
            }

            return null;
        }
        
        #region Properties
        public UIBusinessDesignStudioWindow UIBusinessDesignStudioWindow
        {
            get
            {
                if ((this.mUIBusinessDesignStudioWindow == null))
                {
                    this.mUIBusinessDesignStudioWindow = new UIBusinessDesignStudioWindow();
                }
                return this.mUIBusinessDesignStudioWindow;
            }
        }
        #endregion
        
        #region Fields
        private UIBusinessDesignStudioWindow mUIBusinessDesignStudioWindow;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "11.0.51106.1")]
    public class UIBusinessDesignStudioWindow : WpfWindow
    {
        
        public UIBusinessDesignStudioWindow()
        {
            #region Search Criteria
            this.SearchProperties.Add(new PropertyExpression(WpfWindow.PropertyNames.ClassName, "HwndWrapper", PropertyExpressionOperator.Contains));
            this.SearchProperties.Add(new PropertyExpression(WpfWindow.PropertyNames.Name, "Warewolf", PropertyExpressionOperator.Contains));
            #endregion
        }
        
        #region Properties
        public UIItemCustom UIItemCustom
        {
            get
            {
                if ((this.mUIItemCustom == null))
                {
                    this.mUIItemCustom = new UIItemCustom(this);
                }
                return this.mUIItemCustom;
            }
        }
        #endregion
        
        #region Fields
        private UIItemCustom mUIItemCustom;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "11.0.51106.1")]
    public class UIItemCustom : WpfCustom
    {
        
        public UIItemCustom(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[UITestControl.PropertyNames.ClassName] = "Uia.DataListView";
            this.WindowTitles.Add(TestBase.GetStudioWindowName());
            #endregion
        }
        
        #region Properties
        public UIScalarExplorerTree UIScalarExplorerTree
        {
            get
            {
                if ((this.mUIScalarExplorerTree == null))
                {
                    this.mUIScalarExplorerTree = new UIScalarExplorerTree(this);
                }
                return this.mUIScalarExplorerTree;
            }
        }
        #endregion
        
        #region Fields
        private UIScalarExplorerTree mUIScalarExplorerTree;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "11.0.51106.1")]
    public class UIScalarExplorerTree : WpfTree
    {
        
        public UIScalarExplorerTree(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WpfTree.PropertyNames.AutomationId] = "scalarExplorer";
            this.WindowTitles.Add(TestBase.GetStudioWindowName());
            #endregion
        }
        
        #region Properties
        public UIItemTreeItem UIItemTreeItem
        {
            get
            {
                if ((this.mUIItemTreeItem == null))
                {
                    this.mUIItemTreeItem = new UIItemTreeItem(this);
                }
                return this.mUIItemTreeItem;
            }
        }
        #endregion
        
        #region Fields
        private UIItemTreeItem mUIItemTreeItem;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "11.0.51106.1")]
    public class UIItemTreeItem : WpfTreeItem
    {
        
        public UIItemTreeItem(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.WindowTitles.Add(TestBase.GetStudioWindowName());
            #endregion
        }
        
        #region Properties
        public UIDev2StudioCoreModelsTreeItem UIDev2StudioCoreModelsTreeItem
        {
            get
            {
                if ((this.mUIDev2StudioCoreModelsTreeItem == null))
                {
                    this.mUIDev2StudioCoreModelsTreeItem = new UIDev2StudioCoreModelsTreeItem(this);
                }
                return this.mUIDev2StudioCoreModelsTreeItem;
            }
        }
        #endregion
        
        #region Fields
        private UIDev2StudioCoreModelsTreeItem mUIDev2StudioCoreModelsTreeItem;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "11.0.51106.1")]
    public class UIDev2StudioCoreModelsTreeItem : WpfTreeItem
    {
        
        public UIDev2StudioCoreModelsTreeItem(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WpfTreeItem.PropertyNames.Name] = "Dev2.Studio.Core.Models.DataList.DataListItemModel";
            this.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
            this.WindowTitles.Add(TestBase.GetStudioWindowName());
            #endregion
        }
        
        #region Properties
        public WpfEdit UINameTxtEdit
        {
            get
            {
                if ((this.mUINameTxtEdit == null))
                {
                    this.mUINameTxtEdit = new WpfEdit(this);
                    #region Search Criteria
                    this.mUINameTxtEdit.SearchProperties[WpfEdit.PropertyNames.AutomationId] = "NameTxt";
                    this.mUINameTxtEdit.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
                    this.mUINameTxtEdit.WindowTitles.Add(TestBase.GetStudioWindowName());
                    #endregion
                }
                return this.mUINameTxtEdit;
            }
        }
        #endregion
        
        #region Fields
        private WpfEdit mUINameTxtEdit;
        #endregion
    }
}
