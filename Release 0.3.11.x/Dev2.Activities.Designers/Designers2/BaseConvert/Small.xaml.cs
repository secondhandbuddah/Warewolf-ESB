﻿using System.Windows;
using System.Windows.Input;

namespace Dev2.Activities.Designers2.BaseConvert
{
    public partial class Small
    {
        public Small()
        {
            InitializeComponent();
            DataGrid = SmallDataGrid;
        }

        protected override IInputElement GetInitialFocusElement()
        {
            return DataGrid.GetFocusElement(0);
        }
    }
}
