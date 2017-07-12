using System;
using AVFoundation;
using Foundation;
using UIKit;

namespace KVOControllerQS
{
    public class AudioPlayerVC: UIViewController
    {
        AVAudioPlayer player;
        static double TIME_STEP = 10;

        protected AudioPlayerVC(IntPtr handle) : base(handle)
        {
			// Note: this .ctor should not contain any initialization logic.
		}

        void StartPlaying() {
			var songURL = NSBundle.MainBundle.GetUrlForResource("song", "mp3", "Songs");
			player = AVAudioPlayer.FromUrl(songURL);
			player.FinishedPlaying += PlayerDidFinishPlaying;
			player.Play();
        }

        private void PlayerDidFinishPlaying(object sender, AVStatusEventArgs e)
        {
            player = null;
        }

		void DidTapStop(UIBarButtonItem sender)
		{
			if (player == null || !player.Playing) return;
			player.Stop();
			player.CurrentTime = 0;
		}

		private void DidTapPause(UIBarButtonItem pauseItem)
		{
			if (player == null || !player.Playing) return;
			player.Pause();
		}

	    void DidTapRewind(UIBarButtonItem sender)
		{
			if (player == null) return;
			player.CurrentTime -= Math.Max(TIME_STEP, player.CurrentTime);
		}

	    void DidTapFastForward(UIBarButtonItem sender)
		{
			if (player == null) return;
			player.CurrentTime += Math.Min(TIME_STEP, player.Duration - player.CurrentTime);
		}

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
			if (player != null)
			{
				player.Stop();
				player.FinishedPlaying -= PlayerDidFinishPlaying;
				player = null;
			}
        }
    }
}
