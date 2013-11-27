﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by coded UI test builder.
//      Version: 11.0.0.0
//
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------

using System.Linq;

namespace Dev2.Studio.UI.Tests.UIMaps.ActivityDropWindowUIMapClasses
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
    
    
    [GeneratedCode("Coded UITest Builder", "11.0.60315.1")]
    public partial class ActivityDropWindowUIMap : UIMapBase
    {

        /// <summary>
        /// ClickOkButton
        /// </summary>
        public void ClickOkButton()
        {
            // Click 'OK' button
            Mouse.Click(GetOkButtonOnActivityDropWindow(), new Point(79, 11));
        }

        /// <summary>
        /// ClickCancelButton
        /// </summary>
        public void ClickCancelButton()
        {
            WpfButton uICancelButton = null;
            foreach(var child in StudioWindow.GetChildren()[0].GetChildren())
            {
                if(child.FriendlyName == "Cancel")
                {
                    uICancelButton = child as WpfButton;
                    break;
                }
            }

            // Click 'Cancel' button
            Mouse.Click(uICancelButton, new Point(64, 7));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public UITestControl GetOkButtonOnActivityDropWindow()
        {
            foreach(var child in StudioWindow.GetChildren()[0].GetChildren())
            {
                if(child.FriendlyName == "OK")
                {
                    return child as WpfButton;
                }
            }
            return null;
        }

        /// <summary>
        /// SingleClickAFolder
        /// </summary>
        public void SingleClickAFolder()
        {
            var localHostExplorerTree = GetLocalHostExplorerTree();
            foreach(var treeChild in localHostExplorerTree.GetChildren())
            {
                var workflowsAutoID = treeChild.GetProperty("AutomationID").ToString();
                if(workflowsAutoID.Contains("WORKFLOW"))
                {
                    var firstFolder = treeChild.GetChildren()[6];
                    if(firstFolder.ControlType == "TreeItem")
                    {
                        Mouse.Click(firstFolder, new Point(57, 9));
                        return;
                    }
                }
            }
            throw new UITestControlNotFoundException("Folder not found");
        }

        private UITestControl GetLocalHostExplorerTree()
        {
            var SelectActivityDialog = StudioWindow.GetChildren()[0];
            foreach (var child in SelectActivityDialog.GetChildren())
            {
                var navViewAutoID = child.GetProperty("AutomationID").ToString();
                if (navViewAutoID.Contains("TheNavigationView"))
                {
                    foreach (var navigationViewChid in child.GetChildren())
                    {
                        var navAutoID = navigationViewChid.GetProperty("AutomationID").ToString();
                        if (navAutoID.Contains("Navigation"))
                        {
                            foreach (var navChild in navigationViewChid.GetChildren())
                            {
                                var autoID = navChild.GetProperty("AutomationID").ToString();
                                if (autoID.Contains("localhost"))
                                {
                                    return navChild;
                                }
                            }
                        }
                    }
                }
            }
            throw new UITestControlNotFoundException("Localhost explorer tree not found, Activity Drop Window may not have openned yet.");
        }

        /// <summary>
        /// SingleClickAResource
        /// </summary>
        public void SingleClickFirstResource()
        {
            var localHostExplorerTree = GetLocalHostExplorerTree();
            foreach(var treeChild in localHostExplorerTree.GetChildren())
            {
                var workflowsAutoID = treeChild.GetProperty("AutomationID").ToString();
                if(workflowsAutoID.Contains("WORKFLOW"))
                {
                    var firstFolder = treeChild.GetChildren()[6];
                    if(firstFolder.ControlType == "TreeItem")
                    {
                        var firstResource = firstFolder.GetChildren()[7];
                        if(firstResource.ControlType == "TreeItem")
                        {
                            Mouse.Click(firstResource, new Point(73, 9));
                            return;
                        }
                    }
                }
            }
            throw new UITestControlNotFoundException("No resources found");
        }

        /// <summary>
        /// DoubleClickAFolder
        /// </summary>
        public void DoubleClickFirstWorkflowFolder()
        {
            var localHostExplorerTree = GetLocalHostExplorerTree();
            foreach(var treeChild in localHostExplorerTree.GetChildren())
            {
                var workflowsAutoID = treeChild.GetProperty("AutomationID").ToString();
                if (workflowsAutoID.Contains("WORKFLOW"))
                {
                    var firstFolder = treeChild.GetChildren()[6];
                    if (firstFolder.ControlType == "TreeItem")
                    {
                        Mouse.Click(firstFolder, new Point(57,9));
                        Playback.Wait(100);
                        Mouse.DoubleClick(firstFolder, new Point(57, 9));
                        return;
                    }
                }
            }
            throw new UITestControlNotFoundException("Folder not found");
        }

        /// <summary>
        /// DoubleClickAResource
        /// </summary>
        public void DoubleClickAResource()
        {
            var localHostExplorerTree = GetLocalHostExplorerTree();
            foreach(var treeChild in localHostExplorerTree.GetChildren())
            {
                var workflowsAutoID = treeChild.GetProperty("AutomationID").ToString();
                if(workflowsAutoID.Contains("WORKFLOW"))
                {
                    var firstFolder = treeChild.GetChildren()[6];
                    if(firstFolder.ControlType == "TreeItem")
                    {
                        var firstResource = firstFolder.GetChildren()[7];
                        if(firstResource.ControlType == "TreeItem")
                        {
                            Mouse.DoubleClick(firstResource, new Point(73, 9));
                            Playback.Wait(150);
                            return;
                        }
                    }
                }
            }
            throw new UITestControlNotFoundException("No resources found");
        }
    }
}
