using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DanMacross.Model;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.IO;

namespace DanMacross.Common
{
    public class DannMakuParser
    {
        public static IEnumerable<DannMakuModel> Parse(string content, ContentType type = ContentType.JSON)
        {
            switch (type)
            {
                case ContentType.JSON:
                    return JsonConvert.DeserializeObject<IEnumerable<DannMakuModel>>(content);
                case ContentType.XML:
                    {
                        var des = new XmlSerializer(typeof(IEnumerable<DannMakuModel>));
                        using (var reader = new StringReader(content))
                        {
                            return (IEnumerable<DannMakuModel>)des.Deserialize(reader);
                        }
                    }
            }
            return null;
        }
    }
    public enum ContentType
    {
        JSON,
        XML,
    }
}
