﻿using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;

namespace Dev2.Studio.UI.Tests.UIMaps
{
    public class WebSourceWizardUIMap : UIMapBase
    {
        public void ClickSave()
        {
            Mouse.Click(StudioWindow.GetChildren()[0].GetChildren()[0], new Point(648, 501));
        }
    }
}