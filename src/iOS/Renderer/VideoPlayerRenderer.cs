using System;
using System.Collections.Generic;
using System.Text;
using AVFoundation;
using AVKit;
using CoreGraphics;
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
        AVAsset _asset;
        AVPlayerItem _playerItem;
        AVPlayer _player;

        AVPlayerLayer _playerLayer;

        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> e)
        {
            base.OnElementChanged(e);
            _asset = AVAsset.FromUrl(NSUrl.FromString(Element.Source));
            _playerItem = new AVPlayerItem(_asset);
            _player = new AVPlayer(_playerItem);
            _playerLayer = AVPlayerLayer.FromPlayer(_player);
            _playerLayer.Frame = NativeView.Frame;
            NativeView.Layer.AddSublayer(_playerLayer);
            if (Element.AutoPlay)
                _player.Play();
            //TODO:AVPlayer not showing
        }
    }
}
