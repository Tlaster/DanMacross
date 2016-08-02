using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using DanMacross.Model;

namespace DanMacross.Common
{

    public interface IFileReader
    {
        IDannMakuListModel FromJson(string json);
        IDannMakuListModel FromJson(string json, Encoding encoding);
        IDannMakuListModel FromXml(string xml);
        IDannMakuListModel FromXml(string xml, Encoding encoding);
    }

    public class FileReader : IFileReader
    {
        public IDannMakuListModel FromXml(string xml)
        {
            var des = new DataContractSerializer(typeof(IDannMakuListModel));
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                return (IDannMakuListModel)des.ReadObject(ms);
            }
        }

        public IDannMakuListModel FromXml(string xml, Encoding encoding)
        {
            var des = new DataContractSerializer(typeof(IDannMakuListModel));
            using (var ms = new MemoryStream(encoding.GetBytes(xml)))
            {
                return (IDannMakuListModel)des.ReadObject(ms);
            }
        }

        public IDannMakuListModel FromJson(string json)
        {
            var deserializer = new DataContractJsonSerializer(typeof(IDannMakuListModel));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                return (IDannMakuListModel)deserializer.ReadObject(ms);
        }

        public IDannMakuListModel FromJson(string json, Encoding encoding)
        {
            var deserializer = new DataContractJsonSerializer(typeof(IDannMakuListModel));
            using (MemoryStream ms = new MemoryStream(encoding.GetBytes(json)))
                return (IDannMakuListModel)deserializer.ReadObject(ms);
        }
    }
}
