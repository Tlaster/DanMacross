using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DanMacross
{
    public class VideoPlayer : View
    {
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            propertyName: "Source",
            returnType: typeof(string),
            declaringType: typeof(VideoPlayer),
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

    }
}
