using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DanMacross.Model
{
    public interface IDannMakuModel
    {
        string Content { get; set; }
        Color Foreground { get; set; }
        ScreenLocation Location { get; set; }
        TimeSpan Position { get; set; }
    }

    public class DannMakuModel : IDannMakuModel
    {
        public TimeSpan Position { get; set; }
        public string Content { get; set; }
        public ScreenLocation Location { get; set; }
        public Color Foreground { get; set; }

    }

    public class DannMakuListModel : IDannMakuListModel
    {
        public List<IDannMakuModel> DannMakuList { get; set; }
    }

    public interface IDannMakuListModel
    {
        List<IDannMakuModel> DannMakuList { get; set; }
        
    }

    public enum ScreenLocation
    {
        Top,
        Right,
        Bottom,
        Left,
    }
}
