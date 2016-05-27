using System;
using System.Collections.Generic;
using System.Text;
using AVFoundation;
using AVKit;
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
        UIButton _playButton;
        AVPlayerViewController _avPlayerViewController;

        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> e)
        {
            base.OnElementChanged(e);
            var url = new NSUrl(Element.Source);
            _asset = AVAsset.FromUrl(url);
            _playerItem = new AVPlayerItem(_asset);
            _player = new AVPlayer(_playerItem);
            _avPlayerViewController = new AVPlayerViewController();
            _avPlayerViewController.Player = _player;
            _playerLayer = AVPlayerLayer.FromPlayer(_player);
            _player.Play();
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            _playerLayer.Frame = NativeView.Frame;
            _avPlayerViewController.View.Frame = NativeView.Frame;
            NativeView.Layer.AddSublayer(_avPlayerViewController.View.Layer);
        }
    }
}
