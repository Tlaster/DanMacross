using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DanMacross.Common;
using DanMacross.Model;
using Xamarin.Forms;

namespace DanMacross
{
    public class DannMakuView : ContentView
    {
        public static readonly BindableProperty PositionProperty = BindableProperty.Create(nameof(Position), typeof(TimeSpan), typeof(DannMakuView), TimeSpan.Zero, propertyChanged: OnPositionChanged);

        public TimeSpan Position
        {
            get { return (TimeSpan)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        public static readonly BindableProperty DannMakuFileProperty = BindableProperty.Create(nameof(DannMakuFile), typeof(string), typeof(DannMakuView));

        public string DannMakuFile
        {
            get { return (string)GetValue(DannMakuFileProperty); }
            set { SetValue(DannMakuFileProperty, value); }
        }

        public static readonly BindableProperty AssemblyProperty = BindableProperty.Create(nameof(Assembly), typeof(Assembly), typeof(DannMakuView));

        public Assembly Assembly
        {
            get { return (Assembly)GetValue(AssemblyProperty); }
            set { SetValue(AssemblyProperty, value); }
        }

        public static readonly BindableProperty DannMakuProperty = BindableProperty.Create(nameof(DannMaku), typeof(IDannMakuListModel), typeof(DannMakuView));

        public IDannMakuListModel DannMaku
        {
            get { return (IDannMakuListModel)GetValue(DannMakuProperty); }
            set { SetValue(DannMakuProperty, value); }
        }

        public DannMakuView()
        {

        }

        private static void OnPositionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((DannMakuView)bindable).SeekTo();
        }

        private void SeekTo()
        {

        }

        private async Task LoadFile()
        {
            if (string.IsNullOrEmpty(DannMakuFile))
                return;
            string data;
            if (DannMakuFile.ToLower().StartsWith("http://") || DannMakuFile.ToLower().StartsWith("https://"))
                using (var client = new HttpClient())
                    data = await client.GetStringAsync(DannMakuFile);
            else
                using (var stream = Assembly.GetManifestResourceStream(DannMakuFile))
                using (var reader = new StreamReader(stream))
                    data = await reader.ReadToEndAsync();
            if (string.IsNullOrEmpty(data))
                return;
            IDannMakuListModel model;
            switch (Path.GetExtension(DannMakuFile).ToLower())
            {
                case ".json":
                    model = StaticResources.FileReader.FromJson(data);
                    break;
                case ".xml":
                    model = StaticResources.FileReader.FromXml(data);
                    break;
                default:
                    model = null;
                    break;
            }
            if (model == null)
                return;

        }
    }
}
