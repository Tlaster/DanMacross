using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DanMacross;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(VideoPlayer), typeof(DanMacross.UWP.Renderer.VideoPlayerRenderer))]
namespace DanMacross.UWP.Renderer
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, MediaElement>
    {
        private MediaElement _mediaElement;

        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                _mediaElement = new MediaElement();
                _mediaElement.AreTransportControlsEnabled = true;
                if (Element?.Source != null)
                {
                    _mediaElement.Source = new Uri(Element.Source);
                    if (Element.AutoPlay)
                        _mediaElement.Play();
                }
                SetNativeControl(_mediaElement);
            }
            if (e.OldElement != null)
            {
                Tapped -= VideoPlayerRenderer_Tapped;
                _mediaElement.Stop();
            }
            if (e.NewElement != null)
            {
                Tapped += VideoPlayerRenderer_Tapped;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            switch (e.PropertyName)
            {
                case nameof(Element.IsFullScreen):
                    _mediaElement.IsFullWindow = Element.IsFullScreen;
                    break;
                case nameof(Element.Source):
                    if (Element?.Source != null)
                        _mediaElement.Source = new Uri(Element.Source);
                    if (Element.AutoPlay)
                        _mediaElement.Play();
                    break;
                case nameof(Element.AutoPlay):
                    break;
                default:
                    break;
            }
        }

        private void VideoPlayerRenderer_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {

        }
    }
}
