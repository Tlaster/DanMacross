using System;
using System.Collections.Generic;
using System.ComponentModel;
using DanMacross.Entities;
using Xamarin.Forms;

namespace DanMacross
{
	public partial class MediaElement : ContentView
	{
		public MediaElement()
		{
			InitializeComponent();
            BindingContext = this;
        }
        public MediaElement(string source) : this()
        {
            Source = source;
        }
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            propertyName: "Source",
            returnType: typeof(string),
            declaringType: typeof(MediaElement),
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
            declaringType: typeof(MediaElement),
            defaultValue: false);

        public bool IsFullScreen
        {
            get { return (bool)GetValue(IsFullScreenProperty); }
            set { SetValue(IsFullScreenProperty, value); }
        }


        public static readonly BindableProperty AutoPlayProperty = BindableProperty.Create(
            propertyName: "AutoPlay",
            returnType: typeof(bool),
            declaringType: typeof(MediaElement),
            defaultValue: false);

        public bool AutoPlay
        {
            get { return (bool)GetValue(AutoPlayProperty); }
            set { SetValue(AutoPlayProperty, value); }
        }

        public static readonly BindableProperty CurrentStateProperty = BindableProperty.Create(
            propertyName: "CurrentState",
            returnType: typeof(PlaybackState),
            declaringType: typeof(MediaElement),
            defaultValue: PlaybackState.Pause);

        public PlaybackState CurrentState
        {
            get { return (PlaybackState)GetValue(CurrentStateProperty); }
            set
            {
                SetValue(CurrentStateProperty, value);
                UpdateButtonText();
            }
        }
        
        private void UpdateButtonText()
        {
            switch (CurrentState)
            {
                case PlaybackState.Playing:
                    PlayerButton.Text = "fa-pause";
                    break;
                case PlaybackState.Pause:
                    PlayerButton.Text = "fa-play-circle-o";
                    break;
                default:
                    break;
            }
        }

        public async void VolumeButtonClicked(object sender, EventArgs e)
        {
            if (VolumeControl.IsVisible)
            {
                VolumeControl.TranslateTo(0, 10, easing: Easing.CubicInOut);
                await VolumeControl.FadeTo(0, easing: Easing.CubicInOut);
                VolumeControl.IsVisible = false;
            }
            else
            {
                VolumeControl.IsVisible = true;
                VolumeControl.TranslateTo(0, -10, easing: Easing.CubicInOut);
                await VolumeControl.FadeTo(1, easing: Easing.CubicInOut);
            }
        }

        public void PlayButtonClicked(object sender, EventArgs e)
        {
            switch (CurrentState)
            {
                case PlaybackState.Playing:
                    CurrentState = PlaybackState.Pause;
                    break;
                case PlaybackState.Pause:
                    CurrentState = PlaybackState.Playing;
                    break;
                default:
                    break;
            }
        }

    }
}

