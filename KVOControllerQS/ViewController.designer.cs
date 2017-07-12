// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace KVOControllerQS
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem _PlayItem { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIToolbar ControlToolbar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView DiscIV { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider TimeSlider { get; set; }

        [Action ("DidTapFastForward:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void DidTapFastForward (UIKit.UIBarButtonItem sender);

        [Action ("DidTapPlay:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void DidTapPlay (UIKit.UIBarButtonItem sender);

        [Action ("DidTapRewind:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void DidTapRewind (UIKit.UIBarButtonItem sender);

        void ReleaseDesignerOutlets ()
        {
            if (_PlayItem != null) {
                _PlayItem.Dispose ();
                _PlayItem = null;
            }

            if (ControlToolbar != null) {
                ControlToolbar.Dispose ();
                ControlToolbar = null;
            }

            if (DiscIV != null) {
                DiscIV.Dispose ();
                DiscIV = null;
            }

            if (TimeSlider != null) {
                TimeSlider.Dispose ();
                TimeSlider = null;
            }
        }
    }
}