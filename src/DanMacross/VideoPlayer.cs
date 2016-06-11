using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DanMacross.Entities;
using FormsPlugin.Iconize;
using Xamarin.Forms;

namespace DanMacross
{
    public class VideoPlayer : View
    {
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            propertyName: "Source",
            returnType: typeof(string),
            declaringType: typeof(VideoPlayer),
            defaultBindingMode: BindingMode.OneWay,
            defaultValue: null);

        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly BindableProperty IsFullScreenProperty = BindableProperty.Create(
            propertyName: "IsFullScreen",
            returnType: typeof(bool),
            declaringType: typeof(VideoPlayer),
            defaultValue: false);

        public bool IsFullScreen
        {
            get { return (bool)GetValue(IsFullScreenProperty); }
            set { SetValue(IsFullScreenProperty, value); }
        }


        public static readonly BindableProperty AutoPlayProperty = BindableProperty.Create(
            propertyName: "AutoPlay",
            returnType: typeof(bool),
            declaringType: typeof(VideoPlayer),
            defaultValue: false);

        public bool AutoPlay
        {
            get { return (bool)GetValue(AutoPlayProperty); }
            set { SetValue(AutoPlayProperty, value); }
        }

        public static readonly BindableProperty PositionProperty = BindableProperty.Create(
            propertyName: "Position",
            returnType: typeof(TimeSpan),
            declaringType: typeof(VideoPlayer),
            defaultValue: TimeSpan.FromSeconds(0));

        public TimeSpan Position
        {
            get { return (TimeSpan)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        public static readonly BindableProperty DurationProperty = BindableProperty.Create(
            propertyName: "Duration",
            returnType: typeof(TimeSpan),
            declaringType: typeof(VideoPlayer),
            defaultValue: TimeSpan.FromSeconds(100));

        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public static readonly BindableProperty VolumeProperty = BindableProperty.Create(
            propertyName: "Volume",
            returnType: typeof(double),
            declaringType: typeof(VideoPlayer),
            defaultValue: 1.0);
        
        public double Volume
        {
            get { return (double)GetValue(VolumeProperty); }
            set { SetValue(VolumeProperty, value); }
        }


        public static readonly BindableProperty CurrentStateProperty = BindableProperty.Create(
            propertyName: "CurrentState",
            returnType: typeof(PlaybackState),
            declaringType: typeof(VideoPlayer),
            defaultValue: PlaybackState.Pause);

        public PlaybackState CurrentState
        {
            get { return (PlaybackState)GetValue(CurrentStateProperty); }
            set { SetValue(CurrentStateProperty, value); }
        }

    }
}
