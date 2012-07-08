using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;
namespace Ambiguous
{
    public class PluginReader
    {
        public static XmlTextReader xr;
        public static List<Control> Controls = new List<Control>();
        public static List<string> Revisions = new List<string>();

        public static string Class;
        public static string Author;
        public static string Version;

        public void Read(int tagoff, string st, ref Panel Meta, TagObject ST,Map map)
        {
            #region Reading
            string C = st.Replace('*', '!');
            C = C.Replace('/', '_');
            C = C.Replace('\\', '_');
            C = C.Replace('<', '_');
            C = C.Replace('>', '_');
            string Location = Application.StartupPath + "\\Plugins\\" + C + ".Xml";

            if (!File.Exists(Location))
            {
                MessageBox.Show("Can not find plugin!");
            }
            else
            {
                xr = new XmlTextReader(new FileStream(Location, FileMode.Open, FileAccess.Read, FileShare.Read));

                Controls.Clear();
                Meta.Hide();
                Meta.Refresh();
                Meta.SuspendLayout();
                Class = "";
                Author = "";
                Version = "";
                while (xr.Read())
                {
                    switch (xr.NodeType)
                    {
                        case XmlNodeType.Element:
                            {
                                switch (xr.Name.ToLower())
                                {
                                    case "plugin":
                                        {
                                            Class = xr.GetAttribute("class");
                                            Author = xr.GetAttribute("author");
                                            Version = xr.GetAttribute("version");
                                            break;
                                        }
                                    case "revision":
                                        {
                                            Revisions.Add(xr.ReadElementString());
                                            break;
                                        }
                                    case "byte":
                                        {
                                            string Description = xr.GetAttribute("name");
                                            int Offset = Convert.ToInt32(xr.GetAttribute("offset"));
                                            Value b = new Value(Description, typeof(byte),0,tagoff, Offset, map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "short":
                                        {
                                            string Description = xr.GetAttribute("name");
                                            int Offset = Convert.ToInt32(xr.GetAttribute("offset"));
                                            Value b = new Value(Description, typeof(short),0, tagoff, Offset, map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "ushort":
                                        {
                                            string Description = xr.GetAttribute("name");
                                            int Offset = Convert.ToInt32(xr.GetAttribute("offset"));
                                            Value b = new Value(Description, typeof(ushort), 0, tagoff, Offset, map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "int":
                                        {
                                            string Description = xr.GetAttribute("name");
                                            int Offset = Convert.ToInt32(xr.GetAttribute("offset"));
                                            Value b = new Value(Description, typeof(int), 0, tagoff, Offset, map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "uint":
                                        {
                                            string Description = xr.GetAttribute("name");
                                            int Offset = Convert.ToInt32(xr.GetAttribute("offset"));
                                            Value b = new Value(Description, typeof(uint), 0, tagoff, Offset, map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "float":
                                        {
                                            string Description = xr.GetAttribute("name");
                                            int Offset = Convert.ToInt32(xr.GetAttribute("offset"));
                                            Value b = new Value(Description, typeof(float), 0, tagoff, Offset,map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "string":
                                        {
                                            string Description = xr.GetAttribute("name");
                                            int Offset = Convert.ToInt32(xr.GetAttribute("offset"));
                                            int Length = Convert.ToInt32(xr.GetAttribute("length"));
                                            Value b = new Value(Description, typeof(string), "",Length,tagoff, Offset, map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "unknown":
                                        {
                                            string Description = xr.GetAttribute("name");
                                            int Offset = Convert.ToInt32(xr.GetAttribute("offset"));
                                            Value b = new Value(Description, typeof(int),0, tagoff, Offset, map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "long":
                                        {
                                            string Description = xr.GetAttribute("name");
                                            int Offset = Convert.ToInt32(xr.GetAttribute("offset"));
                                            Value b = new Value(Description, typeof(long), 0, tagoff, Offset, map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "ident":
                                        {
                                            string Description = xr.GetAttribute("name");
                                            int Offset = Convert.ToInt32(xr.GetAttribute("offset"));
                                            Ident i = new Ident("Null", 0,tagoff,Offset, Description, map);
                                            Controls.Add(i);
                                            i.Dock = DockStyle.Top;
                                            i.BringToFront();
                                            break;
                                        }
                                    case "reflexive":
                                        {
                                            string Description = xr.GetAttribute("name");
                                            int Offset = Convert.ToInt32(xr.GetAttribute("offset"));
                                            int Size = Convert.ToInt32(xr.GetAttribute("size"));
                                            map.Reader.BaseStream.Position = ((Offset+tagoff) - map.Index.SecondaryMagic);
                                            Reflexive b = new Reflexive(map, Description, map.Reader.ReadInt32(),tagoff, Offset, Size, xr.ReadSubtree());
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "stringid":
                                        {
                                            string Description = xr.GetAttribute("name");
                                            int Offset = Convert.ToInt32(xr.GetAttribute("offset"));
                                            StringID b = new StringID(Description, tagoff, Offset, 0, map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "enum8":
                                        {
                                            string Description = xr.GetAttribute("name");
                                            int Offset = Convert.ToInt32(xr.GetAttribute("offset"));
                                            //map.Reader.BaseStream.Position = ((Offset+tagoff) - map.Index.SecondaryMagic);
                                            //int Index = map.Reader.ReadByte();
                                            List<string> Items = new List<string>();
                                            List<int> Values = new List<int>();
                                            XmlReader x = xr.ReadSubtree();
                                            while (x.Read())
                                            {
                                                switch (x.Name.ToLower())
                                                {
                                                    case "option":
                                                        {
                                                            Items.Add(xr.GetAttribute("name"));
                                                            Values.Add(Convert.ToInt32(xr.GetAttribute("value")));
                                                            break;
                                                        }
                                                }
                                            }
                                            Enum e = new Enum(Description,tagoff, Offset, typeof(byte), Items, Values, 0, map);
                                            Controls.Add(e);
                                            e.Dock = DockStyle.Top;
                                            e.BringToFront();
                                            break;
                                        }
                                    case "enum16":
                                        {
                                            string Description = xr.GetAttribute("name");
                                            int Offset = Convert.ToInt32(xr.GetAttribute("offset"));
                                            //map.Reader.BaseStream.Position = ((Offset+tagoff) - map.Index.SecondaryMagic);
                                            //int Index = map.Reader.ReadInt16();
                                            List<string> Items = new List<string>();
                                            List<int> Values = new List<int>();
                                            XmlReader x = xr.ReadSubtree();
                                            while (x.Read())
                                            {
                                                switch (x.Name.ToLower())
                                                {
                                                    case "option":
                                                        {
                                                            Items.Add(xr.GetAttribute("name"));
                                                            Values.Add(Convert.ToInt32(xr.GetAttribute("value")));
                                                            break;
                                                        }
                                                }
                                            }
                                            Enum e = new Enum(Description,tagoff, Offset, typeof(short), Items, Values, 0,map);
                                            Controls.Add(e);
                                            e.Dock = DockStyle.Top;
                                            e.BringToFront();
                                            break;
                                        }
                                    case "enum32":
                                        {
                                            string Description = xr.GetAttribute("name");
                                            int Offset = Convert.ToInt32(xr.GetAttribute("offset"));
                                            //map.Reader.BaseStream.Position = ((Offset+tagoff) - map.Index.SecondaryMagic);
                                            //int Index = map.Reader.ReadInt32();
                                            List<string> Items = new List<string>();
                                            List<int> Values = new List<int>();
                                            XmlReader x = xr.ReadSubtree();
                                            while (x.Read())
                                            {
                                                switch (x.Name.ToLower())
                                                {
                                                    case "option":
                                                        {
                                                            Items.Add(xr.GetAttribute("name"));
                                                            Values.Add(Convert.ToInt32(xr.GetAttribute("value")));
                                                            break;
                                                        }
                                                }
                                            }
                                            Enum e = new Enum(Description,tagoff, Offset, typeof(int), Items, Values, 0, map);
                                            Controls.Add(e);
                                            e.Dock = DockStyle.Top;
                                            e.BringToFront();
                                            break;
                                        }
                                    case "bitmask8":
                                        {
                                            string Description = xr.GetAttribute("name");
                                            int Offset = Convert.ToInt32(xr.GetAttribute("offset"));
                                            List<string> Items = new List<string>();
                                            List<int> Values = new List<int>();
                                            XmlReader x = xr.ReadSubtree();
                                            while (x.Read())
                                            {
                                                switch (x.Name.ToLower())
                                                {
                                                    case "option":
                                                        {
                                                            Items.Add(xr.GetAttribute("name"));
                                                            Values.Add(Convert.ToInt32(xr.GetAttribute("value")));
                                                            break;
                                                        }
                                                }
                                            }
                                            Bitmask b = new Bitmask(Description,typeof(byte),Items,Values,tagoff,Offset,map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "bitmask16":
                                        {
                                            string Description = xr.GetAttribute("name");
                                            int Offset = Convert.ToInt32(xr.GetAttribute("offset"));
                                            List<string> Items = new List<string>();
                                            List<int> Values = new List<int>();
                                            XmlReader x = xr.ReadSubtree();
                                            while (x.Read())
                                            {
                                                switch (x.Name.ToLower())
                                                {
                                                    case "option":
                                                        {
                                                            Items.Add(xr.GetAttribute("name"));
                                                            Values.Add(Convert.ToInt32(xr.GetAttribute("value")));
                                                            break;
                                                        }
                                                }
                                            }
                                            Bitmask b = new Bitmask(Description, typeof(short), Items, Values, tagoff, Offset, map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "bitmask32":
                                        {
                                            string Description = xr.GetAttribute("name");
                                            int Offset = Convert.ToInt32(xr.GetAttribute("offset"));
                                            List<string> Items = new List<string>();
                                            List<int> Values = new List<int>();
                                            XmlReader x = xr.ReadSubtree();
                                            while (x.Read())
                                            {
                                                switch (x.Name.ToLower())
                                                {
                                                    case "option":
                                                        {
                                                            Items.Add(xr.GetAttribute("name"));
                                                            Values.Add(Convert.ToInt32(xr.GetAttribute("value")));
                                                            break;
                                                        }
                                                }
                                            }
                                            Bitmask b = new Bitmask(Description, typeof(int), Items, Values, tagoff, Offset, map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "string32":
                                        {
                                            //string Description = xr.GetAttribute("name");
                                            //int Offset = Convert.ToInt32(xr.GetAttribute("offset")) + map.SelectedTag.MetaOffset;
                                            //map.br.BaseStream.Position = Offset;
                                            //string Value = new string(map.br.ReadChars(32));
                                            //String s = new String(Description, Offset, ValueringType.String32);
                                            //Controls.Add(s);
                                            //s.Dock = DockStyle.Top;
                                            //s.BringToFront();
                                            break;
                                        }
                                    case "string64":
                                        {
                                            //string Description = xr.GetAttribute("name");
                                            //int Offset = Convert.ToInt32(xr.GetAttribute("offset")) + map.SelectedTag.MetaOffset;
                                            //map.br.BaseStream.Position = Offset;
                                            //string Value = new string(map.br.ReadChars(64));
                                            //String s = new String(Description, Offset, ValueringType.String64);
                                            //Controls.Add(s);
                                            //s.Dock = DockStyle.Top;
                                            //s.BringToFront();
                                            break;
                                        }
                                    case "unicode64":
                                        {
                                            //string Description = xr.GetAttribute("name");
                                            //int Offset = Convert.ToInt32(xr.GetAttribute("offset")) + map.SelectedTag.MetaOffset;
                                            //map.br.BaseStream.Position = Offset;
                                            //string Value = "";
                                            //for (int i = 0; i < 32; i++)
                                            //{
                                            //    Value += Convert.ToChar(map.br.ReadByte());
                                            //    map.br.BaseStream.Position++;
                                            //}
                                            //String s = new String(Description, Offset, ValueringType.Unicode64);
                                            //Controls.Add(s);
                                            //s.Dock = DockStyle.Top;
                                            //s.BringToFront();
                                            break;
                                        }
                                    case "unicode256":
                                        {
                                            //string Description = xr.GetAttribute("name");
                                            //int Offset = Convert.ToInt32(xr.GetAttribute("offset")) + map.SelectedTag.MetaOffset;
                                            //map.br.BaseStream.Position = Offset;
                                            //string Value = "";
                                            //for (int i = 0; i < 128; i++)
                                            //{
                                            //    Value += Convert.ToChar(map.br.ReadByte());
                                            //    map.br.BaseStream.Position++;
                                            //}
                                            //String s = new String(Description, Offset, ValueringType.Unicode256);
                                            //Controls.Add(s);
                                            //s.Dock = DockStyle.Top;
                                            //s.BringToFront();
                                            break;
                                        }
                                }
                                break;
                            }
                    }
                }

                xr.Close();

                //Load Controls
                for (int i = Controls.Count - 1; i > -1; i--)
                {
                    Meta.Controls.Add(Controls[i]);
                }
                Meta.ResumeLayout(true);
                Meta.Show();
            }
            #endregion
        }
    }
}
