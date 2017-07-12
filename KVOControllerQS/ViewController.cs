using System;

using UIKit;
using FBKVOControllerNS;
using AVFoundation;
using Foundation;
using System.Collections.Generic;
using System.Linq;
using CoreAnimation;
using CoreMedia;

namespace KVOControllerQS
{
    enum PlayerStatus
    {
        Playing,
        Paused,
        Stopped
    }
    public partial class ViewController : UIViewController
    {
        AVPlayer _Player;
        static double TIME_STEP = 10;
        UIBarButtonItem PlayItem;
        UIBarButtonItem StopItem;
        UIBarButtonItem PauseItem;
        FBKVOController _KVOController;
        CADisplayLink _DisplayLink;
        PlayerStatus _PlayerStatus = PlayerStatus.Stopped;

		protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            PlayItem = new UIBarButtonItem(UIBarButtonSystemItem.Play,(sender, e) => {
                DidTapPlay(PlayItem);
            });
            StopItem = new UIBarButtonItem(UIBarButtonSystemItem.Stop, (sender, e) =>
            {
                DidTapStop(StopItem);
            });
            PauseItem = new UIBarButtonItem(UIBarButtonSystemItem.Pause, (sender, e) =>
			{
				DidTapPause(PauseItem);
			});

            _KVOController = FBKVOController.ControllerWithObserver(this);

            NSNotificationCenter.DefaultCenter.AddObserver(AVPlayerItem.DidPlayToEndTimeNotification, PlayerDidPlayToEnd);
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void DidTapPlay(UIBarButtonItem sender)
        {
            if (_Player != null)
            {
                if (_Player.TimeControlStatus == AVPlayerTimeControlStatus.Playing) return;
                _Player.Play();
            }
            else
            {
                try
                {
                    var songURL = NSBundle.MainBundle.GetUrlForResource("song", "mp3", "Songs");
                    _Player = AVPlayer.FromUrl(songURL);
                    _KVOController.Observe(_Player,
									  "timeControlStatus",
                                           NSKeyValueObservingOptions.Initial | NSKeyValueObservingOptions.New,
									  PlayerStatusDidChange);

                    _Player.Play();
                }
                catch (Exception ex)
                {
                    var alert = UIAlertController.Create("Error", ex.Message, UIAlertControllerStyle.Alert);
                    alert.AddAction(UIAlertAction.Create("Dismiss", UIAlertActionStyle.Cancel, null));
                    PresentViewController(alert, true, null);
                }
            }
        }

        private void PlayerStatusDidChange(NSObject arg0, NSObject arg1, NSDictionary<NSString, NSObject> arg2)
        {
            System.Diagnostics.Debug.WriteLine(_Player.Status);
            System.Diagnostics.Debug.WriteLine(_Player.TimeControlStatus);
            System.Diagnostics.Debug.WriteLine("=======================");
            switch (_Player.TimeControlStatus) {
                case AVPlayerTimeControlStatus.Playing:
                    if ( _PlayerStatus != PlayerStatus.Playing) {
                        _PlayerStatus = PlayerStatus.Playing;
                        _DisplayLink = CADisplayLink.Create(UpdateDisplay);
					    _DisplayLink.AddToRunLoop(NSRunLoop.Current, NSRunLoopMode.Default);                       
                    }
                        
                    break;
                case AVPlayerTimeControlStatus.Paused:
                    _DisplayLink?.Invalidate();
                    _DisplayLink = null;
                    break;
                    default:
                    break;
            }
			UpdateToolbarButtons();
		}

        private void PlayerDidFinishPlaying(object sender, AVStatusEventArgs e)
        {
            TimeSlider.Value = 0;
        }

		private void UpdateDisplay()
		{
            if (_Player == null) {
                TimeSlider.Value = 0;
                DiscIV.Transform = CoreGraphics.CGAffineTransform.MakeRotation(0);
                return;
            }

            TimeSlider.Value = _Player.CurrentItem.Duration.Seconds.Equals(0) ? 0
                : (float)(_Player.CurrentTime.Seconds / _Player.CurrentItem.Duration.Seconds);
            var radians = (nfloat)(_Player.CurrentTime.Seconds * Math.PI / 60);
            DiscIV.Transform = CoreGraphics.CGAffineTransform.MakeRotation(radians);
        }

        void UpdateToolbarButtons() {
            var items = new List<UIBarButtonItem>(ControlToolbar.Items);
            var newItems = items.GetRange(items.Count - 5, 5); // space - rewind - space - fast forward - space
			switch (_Player.TimeControlStatus)
			{
				case AVPlayerTimeControlStatus.Playing:
					newItems.Insert(0, StopItem);
                    {
						var flex = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
						newItems.Insert(0, flex);
                    }
					newItems.Insert(0, PauseItem);
					newItems.Insert(0, items.First()); //flexible space
					break;
				case AVPlayerTimeControlStatus.Paused:
                    if (_PlayerStatus == PlayerStatus.Stopped) {
						newItems.Insert(0, PlayItem);
						newItems.Insert(0, items.First());
                    }
                    else {
						newItems.Insert(0, StopItem);
						var flex = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
						newItems.Insert(0, flex);
						newItems.Insert(0, PlayItem);
						newItems.Insert(0, items.First()); //flexible space
					}
					break;
				default:
                    newItems = items;
					break;
			}
            ControlToolbar.Items = newItems.ToArray();
        }


        void DidTapStop(UIBarButtonItem sender)
        {
            if (_Player == null) return;
            if (_PlayerStatus != PlayerStatus.Stopped) {
                _PlayerStatus = PlayerStatus.Stopped;
                _Player.Seek(CMTime.Zero);
                _Player.Pause();
            }
        }

		private void DidTapPause(UIBarButtonItem pauseItem)
		{
            if (_Player == null || _Player.TimeControlStatus != AVPlayerTimeControlStatus.Playing) return;
            _Player.Pause();
            _PlayerStatus = PlayerStatus.Paused;
		}

        partial void DidTapRewind(UIBarButtonItem sender)
        {
            if (_Player == null) return;
            _Player.Seek(new CMTime((long) Math.Max(_Player.CurrentTime.Seconds - TIME_STEP, 0), 1));
        }

        partial void DidTapFastForward(UIBarButtonItem sender)
        {
			if (_Player == null) return;
            _Player.Seek(new CMTime((long)Math.Min(_Player.CurrentTime.Seconds + TIME_STEP, _Player.CurrentItem.Duration.Seconds), 1));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (_Player != null) {
                _Player.Pause();
                _Player = null;
            }
            _DisplayLink.Invalidate();
        }


        private void PlayerDidPlayToEnd(NSNotification obj)
        {
            _Player.Seek(CMTime.Zero);
        }
    }
}
