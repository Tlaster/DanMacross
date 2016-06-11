using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DanMacross;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms.Platform.UWP;

using NativeMediaElement = Windows.UI.Xaml.Controls.MediaElement;

[assembly: ExportRenderer(typeof(VideoPlayer), typeof(DanMacross.UWP.Renderer.VideoPlayerRenderer))]
namespace DanMacross.UWP.Renderer
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, NativeMediaElement>
    {
        private DispatcherTimer _timer;

        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                _timer = new DispatcherTimer();
                _timer.Interval = TimeSpan.FromSeconds(0);
                _timer.Tick += _timer_Tick;
                SetNativeControl(new NativeMediaElement());
                Control.AutoPlay = Element.AutoPlay;
                Control.CurrentStateChanged += Control_CurrentStateChanged;
                //Control.AreTransportControlsEnabled = true;
                Control.RegisterPropertyChangedCallback(NativeMediaElement.NaturalDurationProperty, (sender, dp) => Element.Duration = Control.NaturalDuration.TimeSpan);
            }
            if (e.OldElement != null)
            {
                Control.Stop();
                _timer.Stop();
            }
            if (e.NewElement != null)
            {
                
            }
        }

        private void _timer_Tick(object sender, object e)
        {
            Element.Position = Control.Position;
        }

        private void Control_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            switch (Control.CurrentState)
            {
                case Windows.UI.Xaml.Media.MediaElementState.Playing:
                    _timer.Start();
                    break;
                default:
                    _timer.Stop();
                    break;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            switch (e.PropertyName)
            {
                case nameof(Element.IsFullScreen):
                    Control.IsFullWindow = Element.IsFullScreen;
                    break;
                case nameof(Element.Source):
                    if (Element?.Source != null)
                        Control.Source = new Uri(Element.Source);
                    break;
                case nameof(Element.AutoPlay):
                    Control.AutoPlay = Element.AutoPlay;
                    break;
                case nameof(Element.Position):
                    //Control.Position = Element.Position;
                    break;
                //case nameof(Element.Width):
                //    Control.Width = Element.Width;
                //    break;
                //case nameof(Element.Height):
                //    Control.Height = Element.Height;
                //    break;
                case nameof(Element.Volume):
                    Control.Volume = Element.Volume;
                    break;
                case nameof(Element.CurrentState):
                    switch (Element.CurrentState)
                    {
                        case Entities.PlaybackState.Playing:
                            Control.Play();
                            break;
                        case Entities.PlaybackState.Pause:
                            Control.Pause();
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
