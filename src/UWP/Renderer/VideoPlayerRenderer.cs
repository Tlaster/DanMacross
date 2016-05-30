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
                _mediaElement.AutoPlay = Element.AutoPlay;
                //_mediaElement.AreTransportControlsEnabled = true;
                if (Element?.Source != null)
                {
                    _mediaElement.Source = new Uri(Element.Source);
                }
                SetNativeControl(_mediaElement);
            }
            if (e.OldElement != null)
            {
                _mediaElement.Stop();
                _mediaElement = null;
            }
            if (e.NewElement != null)
            {
                
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
                    break;
                case nameof(Element.AutoPlay):
                    _mediaElement.AutoPlay = Element.AutoPlay;
                    break;
                case nameof(Element.Position):
                    _mediaElement.Position = Element.Position;
                    break;
                //case nameof(Element.Width):
                //    _mediaElement.Width = Element.Width;
                //    break;
                //case nameof(Element.Height):
                //    _mediaElement.Height = Element.Height;
                //    break;
                case nameof(Element.Volume):
                    _mediaElement.Volume = Element.Volume;
                    break;
                default:
                    break;
            }
        }
        
    }
}
