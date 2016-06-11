using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Timers;
using AVFoundation;
using AVKit;
using CoreGraphics;
using CoreMedia;
using DanMacross;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(VideoPlayer), typeof(DanMacross.iOS.Renderer.VideoPlayerRenderer))]
namespace DanMacross.iOS.Renderer
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, UIView>
    {
        private const int NSEC_PER_SEC = 1000000000;
        private AVAsset _asset;
        private AVPlayerItem _playerItem;
        private AVPlayer _player;
        private AVPlayerLayer _playerLayer;
        private Timer _timer;

        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                _timer = new Timer(TimeSpan.FromSeconds(1).TotalMilliseconds);
                _timer.Elapsed += _timer_Elapsed;
            }
            if (e.OldElement != null)
            {
                _timer?.Stop();
                _timer?.Dispose();
                _timer = null;
                _asset?.Dispose();
                _asset = null;
                _playerItem?.Dispose();
                _playerItem = null;
                _player?.Dispose();
                _player = null;
                _playerLayer?.Dispose();
                _playerLayer = null;
            }
            if (e.NewElement != null)
            {

            }
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_player != null && _player.Error == null && _player.Rate != 0)
            {
                Element.Position = TimeSpan.FromSeconds(_player.CurrentTime.Seconds);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            switch (e.PropertyName)
            {
                case nameof(Element.Width):
                case nameof(Element.Height):
                    if (_playerLayer != null)
                        _playerLayer.Frame = new CGRect(0, 0, Element.Width, Element.Height);
                    break;
                case nameof(Element.Position):
                    //_player.Seek(CMTime.FromSeconds(Element.Position.TotalSeconds, NSEC_PER_SEC));
                    break;
                case nameof(Element.Source):
                    _asset = AVAsset.FromUrl(NSUrl.FromString(Element.Source));
                    _playerItem = new AVPlayerItem(_asset);
                    if (_player == null)
                    {
                        _player = new AVPlayer(_playerItem);
                        _playerLayer = AVPlayerLayer.FromPlayer(_player);
                        _playerLayer.Frame = new CGRect(0, 0, Element.Width, Element.Height);
                        NativeView.Layer.AddSublayer(_playerLayer);
                    }
                    else
                    {
                        _player.ReplaceCurrentItemWithPlayerItem(_playerItem);
                    }
                    Element.Duration = TimeSpan.FromSeconds(_asset.Duration.Seconds);
                    _timer.Start();
                    if (Element.AutoPlay)
                        _player.Play();
                    break;
                case nameof(Element.Volume):
                    _player.Volume = Convert.ToSingle(Element.Volume);
                    break;
                case nameof(Element.IsFullScreen):
                    
                    break;
                case nameof(Element.CurrentState):
                    switch (Element.CurrentState)
                    {
                        case Entities.PlaybackState.Playing:
                            _player.Play();
                            break;
                        case Entities.PlaybackState.Pause:
                            _player.Pause();
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
