using Android.Views;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using DanMacross;
using System.ComponentModel;
using Android.Media;
using Android.Graphics;
using Android.Runtime;
using System;
using Android.Widget;

[assembly: ExportRenderer(typeof(VideoPlayer), typeof(DanMacross.Droid.Renderer.VideoPlayerRenderer))]

namespace DanMacross.Droid.Renderer
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, VideoView>, ISurfaceHolderCallback
    {
        private MediaPlayer _player;
        private VideoView _videoview;

        public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
        {
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            _player.SetDisplay(holder);
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                _videoview = new VideoView(Context);
                base.SetNativeControl(_videoview);
                _videoview.Holder.AddCallback(this);
                _player = new MediaPlayer();
                _player.SetDataSource(Element.Source);
                _player.Prepare();
                if (Element.AutoPlay)
                {
                    _player.Start();
                }
            }
            if (e.OldElement == null)
            {

            }
            if (e.NewElement == null)
            {
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            switch (e.PropertyName)
            {
                case nameof(Element.Width):
                    break;
                case nameof(Element.Height):
                    break;
                default:
                    break;
            }
        }
    }


}