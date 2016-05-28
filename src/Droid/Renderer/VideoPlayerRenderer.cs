using Android.Views;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using DanMacross;
using System.ComponentModel;
using Com.Google.Android.Exoplayer.Upstream;
using Com.Google.Android.Exoplayer;
using Com.Google.Android.Exoplayer.Extractor;
using Android.Media;
using Android.Graphics;
using Android.Runtime;
using System;

[assembly: ExportRenderer(typeof(VideoPlayer), typeof(DanMacross.Droid.Renderer.VideoPlayerRenderer))]

namespace DanMacross.Droid.Renderer
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, SurfaceView>, ISurfaceHolderCallback
    {
        private const int BUFFER_SEGMENT_SIZE = 64 * 1024;
        private const int BUFFER_SEGMENT_COUNT = 256;
        private const int RENDERERNUMBER = 2;
        private IExoPlayer _exoPlayer;
        private SurfaceView _view;
        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                _view = new SurfaceView(Context);
                _view.Holder.AddCallback(this);
                var allocator = new DefaultAllocator(BUFFER_SEGMENT_SIZE);
                var dataSource = new DefaultUriDataSource(Context, "userAgent");
                var sampleSource = new ExtractorSampleSource(Android.Net.Uri.Parse(Element.Source), dataSource, allocator, BUFFER_SEGMENT_COUNT * BUFFER_SEGMENT_SIZE);
                var videoRenderer = new MediaCodecVideoTrackRenderer(Context, sampleSource, MediaCodecSelector.Default, (int)VideoScalingMode.ScaleToFit);
                var audioRenderer = new MediaCodecAudioTrackRenderer(sampleSource, MediaCodecSelector.Default);
                _exoPlayer = ExoPlayerFactory.NewInstance(RENDERERNUMBER);
                if (Element.AutoPlay)
                    _exoPlayer.PlayWhenReady = true;
                _exoPlayer.Prepare(videoRenderer, audioRenderer);
                _exoPlayer.SendMessage(videoRenderer, MediaCodecVideoTrackRenderer.MsgSetSurface, _view);
                SetNativeControl(_view);
            }
            if (e.OldElement == null)
            {

            }
            if (e.NewElement == null)
            {
                _exoPlayer.Release();
                _exoPlayer.Dispose();
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

        }

        public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
        {

        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            if (Element.AutoPlay)
                _exoPlayer.PlayWhenReady = true;
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {

        }
    }
}