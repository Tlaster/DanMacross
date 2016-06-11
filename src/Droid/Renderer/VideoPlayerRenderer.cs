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
using System.Timers;

[assembly: ExportRenderer(typeof(VideoPlayer), typeof(DanMacross.Droid.Renderer.VideoPlayerRenderer))]

namespace DanMacross.Droid.Renderer
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, VideoView>, ISurfaceHolderCallback
    {
        private MediaPlayer _player;
        private Timer _timer;

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
                _timer = new Timer(TimeSpan.FromSeconds(1).TotalMilliseconds);
                _timer.Elapsed += _timer_Elapsed;
                SetNativeControl(new VideoView(Context));
                Control.Holder.AddCallback(this);
				_player = new MediaPlayer();
                //_player.VideoSizeChanged += (sender, ee) => {
                //	//Control.LayoutParameters = new LayoutParams(Convert.ToInt32(Element.Width),Convert.ToInt32(((float)ee.Height / (float)ee.Width) * (float)Element.Width));
                //	//Control.ForceLayout();
                //};
                _player.SetDataSource(Element.Source);
                _player.Prepare();
                Element.Duration = TimeSpan.FromMilliseconds(_player.Duration);
                if (Element.AutoPlay)
                {
                    _player.Start();
                }
                _timer.Start();
                //Control.Layout (0, 200, Convert.ToInt32(((float)_player.VideoHeight / (float)_player.VideoWidth) * (float)Element.Width), Convert.ToInt32(Element.Width));

            }
            if (e.OldElement != null)
            {
                _timer.Stop();
                _timer.Dispose();
                _timer = null;
            }
            if (e.NewElement != null)
            {
				
            }
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_player.IsPlaying)
            {
                Element.Position = TimeSpan.FromMilliseconds(_player.CurrentPosition);
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
                case nameof(Element.Volume):
                    _player.SetVolume(Convert.ToSingle(Element.Volume), Convert.ToSingle(Element.Volume));
                    break;
                case nameof(Element.IsFullScreen):

                    break;
                case nameof(Element.CurrentState):
                    switch (Element.CurrentState)
                    {
                        case Entities.PlaybackState.Playing:
                            _player.Start();
                            break;
                        case Entities.PlaybackState.Pause:
                            _player.Pause();
                            break;
                        default:
                            break;
                    }
                    break;
                case nameof(Element.Source):
                    _player.SetDataSource(Element.Source);
                    _player.Prepare();
                    Element.Duration = TimeSpan.FromMilliseconds(_player.Duration);
                    if (Element.AutoPlay)
                    {
                        _player.Start();
                    }
                    _timer.Start();
                    break;
                default:
                    break;
            }
        }
    }


}