﻿using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;

namespace Dev2.Studio.UI.Tests.UIMaps
{
    public partial class FeedbackUIMap
    {
        public bool DoesRecordedFeedbackWindowExist()
        {
            WinWindow RecordedFeedbackWindow = GetRecordedFeedbackWindow();
            Point p = new Point();
            return RecordedFeedbackWindow.TryGetClickablePoint(out p);
        }

        public bool DoesFeedbackWindowExist()
        {
            WpfWindow theWindow = GetFeedbackWindow();
            Point p = new Point();
            return theWindow.TryGetClickablePoint(out p);
        }

        public void ClickStartStopRecordingButton()
        {
            Point theButtonPosition = StartRecordingButtonPoint();
            Mouse.Click(theButtonPosition);
        }

        public void FeedbackWindow_ClickCancel()
        {
            WpfButton theButton = FeedbackWindow_CancelButton();
            Mouse.Click(theButton, new Point(5, 5));
        }

        public void FeedbackWindow_ClickOpenDefaultEmail()
        {
            WpfButton theButton = FeedbackWindow_OpenDefaultEmailButton();
            Mouse.Click(theButton, new Point(5, 5));
        }
    }
}
