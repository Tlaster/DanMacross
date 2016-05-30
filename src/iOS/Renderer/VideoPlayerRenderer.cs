using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
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
        AVAsset _asset;
        AVPlayerItem _playerItem;
        AVPlayer _player;
        AVPlayerLayer _playerLayer;

        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                _asset = AVAsset.FromUrl(NSUrl.FromString(Element.Source));
                _playerItem = new AVPlayerItem(_asset);
                _player = new AVPlayer(_playerItem);
                _playerLayer = AVPlayerLayer.FromPlayer(_player);
                _playerLayer.Frame = new CGRect(0, 0, Element.Width, Element.Height);
                NativeView.Layer.AddSublayer(_playerLayer);
                if (Element.AutoPlay)
                    _player.Play(); 
            }
            if (e.OldElement == null)
            {
                _asset.Dispose();
                _asset = null;
                _playerItem.Dispose();
                _playerItem = null;
                _player.Dispose();
                _player = null;
                _playerLayer.Dispose();
                _playerLayer = null;
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
                case nameof(Element.Height):
                    _playerLayer.Frame = new CGRect(0, 0, Element.Width, Element.Height);
                    break;
                case nameof(Element.Position):
                    _player.Seek(CMTime.FromSeconds(Element.Position.TotalSeconds, NSEC_PER_SEC));
                    break;
                case nameof(Element.Source):
                    _asset = AVAsset.FromUrl(NSUrl.FromString(Element.Source));
                    _playerItem = new AVPlayerItem(_asset);
                    _player.ReplaceCurrentItemWithPlayerItem(_playerItem);
                    if (Element.AutoPlay)
                        _player.Play();
                    break;
                case nameof(Element.Volume):
                    _player.Volume = Convert.ToSingle(Element.Volume);
                    break;
                default:
                    break;
            }
        }
    }
}
