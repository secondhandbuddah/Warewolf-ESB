﻿using System.Activities.Presentation.Model;
using Dev2.Activities.Designers2.Zip;

namespace Dev2.Activities.Designers.Tests.Zip
{
    public class TestZipDesignerViewModel : ZipDesignerViewModel
    {
        public TestZipDesignerViewModel(ModelItem modelItem)
            : base(modelItem)
        {
        }

        public int ValidateInputPathHitCount { get; private set; }
        protected override void ValidateInputPath()
        {
            ValidateInputPathHitCount++;
            base.ValidateInputPath();
        }

        public int ValidateOutputPathHitCount { get; private set; }
        protected override void ValidateOutputPath()
        {
            ValidateOutputPathHitCount++;
            base.ValidateOutputPath();
        }

        public int ValidateInputAndOutputPathHitCount { get; private set; }
        protected override void ValidateInputAndOutputPaths()
        {
            ValidateInputAndOutputPathHitCount++;
            base.ValidateInputAndOutputPaths();
        }

        public int ValidateUserNameAndPasswordHitCount { get; private set; }
        protected override void ValidateUserNameAndPassword()
        {
            ValidateUserNameAndPasswordHitCount++;
            base.ValidateUserNameAndPassword();
        }

        public int ValidateDestinationUserNameAndPasswordHitCount { get; private set; }
        protected override void ValidateDestinationUserNameAndPassword()
        {
            ValidateDestinationUserNameAndPasswordHitCount++;
            base.ValidateDestinationUserNameAndPassword();
        }

        public string CompressionRatio { set { SetProperty(value); } get { return GetProperty<string>(); } }
    }

}