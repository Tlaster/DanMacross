using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Com.Google.Android.Exoplayer;

[assembly: ExportRenderer(typeof(VideoPlayer), typeof(DanMacross.Droid.Renderer.VideoPlayerRenderer))]

namespace DanMacross.Droid.Renderer
{
	public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, ExoPlayer>
    {
		private ExoPlayer _exoPlayer;
		protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> e)
		{
			base.OnElementChanged (e);
			if (Control == null)
			{

			}
            if (e.OldElement == null)
            {

            }
            if (e.NewElement == null)
            {

            }
		}
    }
}