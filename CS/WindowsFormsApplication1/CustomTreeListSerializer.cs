using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DevExpress.XtraTreeList;
using System.Reflection;
using System.IO;

namespace WindowsFormsApplication1 {

    [XmlRootAttribute("TreeList")]
    [XmlInclude(typeof(CustomerAddress))]
    [XmlInclude(typeof(CustomerCertificate))]
    public class CustomTreeListSerializer : DevExpress.XtraTreeList.TreeList.TreeListXmlSerializationHelper { }       

    public static class TreeListExtensions { 
        public static void ExportToXmlEx(this TreeList tree, string pathName) {
            CustomTreeListSerializer sh = new CustomTreeListSerializer();
            PropertyInfo pi =  typeof(DevExpress.XtraTreeList.TreeList.TreeListXmlSerializationHelper).GetProperty("TreeList", BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(sh, tree, null);
            sh.OnBeforeSerializing();
            Stream stream = null;
            try {
                stream = new FileStream(pathName as String, FileMode.Create);
                XmlSerializer serializer = new XmlSerializer(typeof(CustomTreeListSerializer));
                if(stream != null) serializer.Serialize(stream, sh);
            }
            finally {
                if(stream is FileStream) ((FileStream)stream).Close();
            }        
        }

        public static void ImportFromXmlEx(this TreeList tree, string pathName) {
            if(!tree.IsUnboundMode || tree.TreeListDisposing) return;
            Stream stream = null;
            tree.BeginUpdate();
            tree.BeginUnboundLoad();
            try {
                stream = new FileStream(pathName as String, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(CustomTreeListSerializer));
                System.Xml.XmlReader reader = new System.Xml.XmlTextReader(stream);
                if(reader == null || !serializer.CanDeserialize(reader)) return;
                CustomTreeListSerializer sh = null;
                if((sh = (CustomTreeListSerializer)serializer.Deserialize(reader)) == null) return;
                PropertyInfo pi = typeof(DevExpress.XtraTreeList.TreeList.TreeListXmlSerializationHelper).GetProperty("TreeList", BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(sh, tree, null);
                sh.OnAfterDeserializing();
            }
            finally {
                if(stream is FileStream) ((FileStream)stream).Close();
                tree.EndUnboundLoad();
                tree.EndUpdate();
            }
        }    

    }
}
