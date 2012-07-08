using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
namespace Ambiguous
{
    public partial class Reflexive : UserControl
    {
        public bool hidden = false;
        public Map Map;
        public string Label;
        public int Count;
        public int Base;
        public int Offset;
        public int ChunkSize;
        public int RawTranslation;
        public XmlReader Reader;
        public Reflexive(Map Map, string Label, int Count,int Base, int Offset, int Size,XmlReader Reader)
        {
            this.Map = Map;
            this.Label = Label;
            this.Count = Count;
            this.Base = Base;
            this.Offset = Offset;
            this.ChunkSize = Size;
            this.Reader = Reader;
            if (Base != 0)
            {
                Map.Reader.BaseStream.Position = ((Offset + Base) - Map.Index.SecondaryMagic) + 4;
                this.RawTranslation = Map.Reader.ReadInt32();
            }
            else
                this.RawTranslation = 0;
            InitializeComponent();
            for (int i = 0; i < Count; i++)
                comboBox1.Items.Add(i.ToString());
            label1.Text = Label;
            ReadControls();
            if (Count == 0)
            {
                button1.Text = "+";
                panel1.Hide();
                hidden = true;
            }
            else
            {
                comboBox1.SelectedIndex = 0;
                this.Height += panel1.Height;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!hidden)
            {
                button1.Text = "+";
                panel1.Hide();
                this.Height -= panel1.Height;
                hidden = true;
            }
            else
            {
                panel1.Show();
                button1.Text = "-";
                this.Height += panel1.Height;
                hidden = false;
            }
        }
        public void ReadControls()
        {
            Reader.ReadStartElement();
            List<Control> Controls = new List<Control>();
            while (Reader.Read())
                {
                    switch (Reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            {
                                switch (Reader.Name.ToLower())
                                {
                                    case "byte":
                                        {
                                            string Description = Reader.GetAttribute("name");
                                            int Offset = Convert.ToInt32(Reader.GetAttribute("offset"));
                                            Value b;
                                            if (Count > 0)
                                                b = new Value(Description, typeof(byte), 0,RawTranslation, Offset,Map);
                                            else
                                               b = new Value(Description, typeof(byte), -1,RawTranslation, Offset,Map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "short":
                                        {
                                            string Description = Reader.GetAttribute("name");
                                            int Offset = Convert.ToInt32(Reader.GetAttribute("offset"));
                                            Value b;
                                            if (Count > 0)
                                                b = new Value(Description, typeof(short), 0, RawTranslation, Offset,Map);
                                            else
                                                b = new Value(Description, typeof(short), -1, RawTranslation, Offset,Map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "ushort":
                                        {
                                            string Description = Reader.GetAttribute("name");
                                            int Offset = Convert.ToInt32(Reader.GetAttribute("offset"));
                                            Value b;
                                            if (Count > 0)
                                                b = new Value(Description, typeof(ushort), 0, RawTranslation, Offset,Map);
                                            else
                                                b = new Value(Description, typeof(ushort), -1, RawTranslation, Offset,Map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "int":
                                        {
                                            string Description = Reader.GetAttribute("name");
                                            int Offset = Convert.ToInt32(Reader.GetAttribute("offset"));
                                            Value b;
                                            if (Count > 0)
                                                b = new Value(Description, typeof(int), 0, RawTranslation, Offset,Map);
                                            else
                                                b = new Value(Description, typeof(int), -1, RawTranslation, Offset,Map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "uint":
                                        {
                                            string Description = Reader.GetAttribute("name");
                                            int Offset = Convert.ToInt32(Reader.GetAttribute("offset"));
                                            Value b;
                                            if (Count > 0)
                                                b = new Value(Description, typeof(uint), 0, RawTranslation, Offset,Map);
                                            else
                                                b = new Value(Description, typeof(uint), -1, RawTranslation, Offset,Map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "float":
                                        {
                                            string Description = Reader.GetAttribute("name");
                                            int Offset = Convert.ToInt32(Reader.GetAttribute("offset"));
                                            Value b;
                                            if (Count > 0)
                                                b = new Value(Description, typeof(float), 0, RawTranslation, Offset,Map);
                                            else
                                                b = new Value(Description, typeof(float), -1, RawTranslation, Offset,Map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "string":
                                        {
                                            string Description = Reader.GetAttribute("name");
                                            int Offset = Convert.ToInt32(Reader.GetAttribute("offset"));
                                            int Length = Convert.ToInt32(Reader.GetAttribute("length"));
                                            Value b;
                                            if (Count > 0)
                                                b = new Value(Description, typeof(string), "",Length,RawTranslation, Offset,Map);
                                            else
                                                b = new Value(Description, typeof(string), "Null",0,RawTranslation, Offset,Map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "unknown":
                                        {
                                            string Description = Reader.GetAttribute("name");
                                            int Offset = Convert.ToInt32(Reader.GetAttribute("offset"));
                                            Value b;
                                            if (Count > 0)
                                                b = new Value(Description, typeof(int), 0, RawTranslation, Offset,Map);
                                            else
                                                b = new Value(Description, typeof(int), -1, RawTranslation, Offset,Map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "long":
                                        {
                                            string Description = Reader.GetAttribute("name");
                                            int Offset = Convert.ToInt32(Reader.GetAttribute("offset"));
                                            Value b;
                                            if (Count > 0)
                                                b = new Value(Description, typeof(long), 0, RawTranslation, Offset,Map);
                                            else
                                                b = new Value(Description, typeof(long), -1, RawTranslation, Offset,Map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "ident":
                                        {
                                            string Description = Reader.GetAttribute("name");
                                            int Offset = Convert.ToInt32(Reader.GetAttribute("offset"));
                                            Ident i;
                                            if (Count > 0)
                                            {
                                                try
                                                {
                                                    i = new Ident("", 0,RawTranslation,Offset, Description, Map);
                                                }
                                                catch (Exception)
                                                {
                                                    i = new Ident("Null", -1,RawTranslation, Offset, Description, Map);
                                                }
                                            }
                                            else
                                                 i = new Ident("Null", -1,RawTranslation, Offset, Description, Map);
                                            Controls.Add(i);
                                            i.Dock = DockStyle.Top;
                                            i.BringToFront();
                                            break;
                                        }
                                    case "reflexive":
                                        {
                                            string Description = Reader.GetAttribute("name");
                                            int Offset = Convert.ToInt32(Reader.GetAttribute("offset"));
                                            int Size = Convert.ToInt32(Reader.GetAttribute("size"));
                                            Reflexive b;
                                            if (Count > 0)
                                            {
                                                Map.Reader.BaseStream.Position = ((Offset+RawTranslation) - Map.Index.SecondaryMagic);
                                                b = new Reflexive(Map, Description, Map.Reader.ReadInt32(),RawTranslation, Offset, Size, Reader.ReadSubtree());
                                            }
                                            else
                                                b = new Reflexive(Map, Description, 0,RawTranslation, Offset, Size, Reader.ReadSubtree());
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "stringid":
                                        {
                                            string Description = Reader.GetAttribute("name");
                                            int Offset = Convert.ToInt32(Reader.GetAttribute("offset"));
                                            StringID b;
                                            if (Count > 0)
                                                b = new StringID(Description,RawTranslation, Offset, 0, Map);
                                            else
                                                b = new StringID(Description,RawTranslation, Offset, -1, Map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "enum8":
                                        {
                                            string Description = Reader.GetAttribute("name");
                                            int Offset = Convert.ToInt32(Reader.GetAttribute("offset"));
                                            List<string> Items = new List<string>();
                                            List<int> Values = new List<int>();
                                            XmlReader x = Reader.ReadSubtree();
                                            while (x.Read())
                                            {
                                                switch (x.Name.ToLower())
                                                {
                                                    case "option":
                                                        {
                                                            Items.Add(Reader.GetAttribute("name"));
                                                            Values.Add(Convert.ToInt32(Reader.GetAttribute("value")));
                                                            break;
                                                        }
                                                }
                                            }
                                            Enum e;
                                            if (Count > 0)
                                                e = new Enum(Description,RawTranslation, Offset, typeof(byte), Items, Values, 0,Map);
                                            else
                                                e = new Enum(Description,RawTranslation, Offset, typeof(byte), new List<string>(), new List<int>(), -1,Map);
                                            Controls.Add(e);
                                            e.Dock = DockStyle.Top;
                                            e.BringToFront();
                                            break;
                                        }
                                    case "enum16":
                                        {
                                            string Description = Reader.GetAttribute("name");
                                            int Offset = Convert.ToInt32(Reader.GetAttribute("offset"));
                                            List<string> Items = new List<string>();
                                            List<int> Values = new List<int>();
                                            XmlReader x = Reader.ReadSubtree();
                                            while (x.Read())
                                            {
                                                switch (x.Name.ToLower())
                                                {
                                                    case "option":
                                                        {
                                                            Items.Add(Reader.GetAttribute("name"));
                                                            Values.Add(Convert.ToInt32(Reader.GetAttribute("value")));
                                                            break;
                                                        }
                                                }
                                            }
                                            Enum e;
                                            if (Count > 0)
                                                e = new Enum(Description, RawTranslation, Offset, typeof(int), Items, Values, 0,Map);
                                            else
                                                e = new Enum(Description, RawTranslation, Offset, typeof(int), new List<string>(), new List<int>(), -1,Map);
                                            Controls.Add(e);
                                            e.Dock = DockStyle.Top;
                                            e.BringToFront();
                                            break;
                                        }
                                    case "enum32":
                                        {
                                            string Description = Reader.GetAttribute("name");
                                            int Offset = Convert.ToInt32(Reader.GetAttribute("offset"));
                                            List<string> Items = new List<string>();
                                            List<int> Values = new List<int>();
                                            XmlReader x = Reader.ReadSubtree();
                                            while (x.Read())
                                            {
                                                switch (x.Name.ToLower())
                                                {
                                                    case "option":
                                                        {
                                                            Items.Add(Reader.GetAttribute("name"));
                                                            Values.Add(Convert.ToInt32(Reader.GetAttribute("value")));
                                                            break;
                                                        }
                                                }
                                            }
                                            Enum e;
                                            if (Count > 0)
                                                e = new Enum(Description, RawTranslation, Offset, typeof(int), Items, Values, 0,Map);
                                            else
                                                e = new Enum(Description, RawTranslation, Offset, typeof(int), new List<string>(), new List<int>(), -1,Map);
                                            Controls.Add(e);
                                            e.Dock = DockStyle.Top;
                                            e.BringToFront();
                                            break;
                                        }
                                    case "bitmask8":
                                        {
                                            string Description = Reader.GetAttribute("name");
                                            int Offset = Convert.ToInt32(Reader.GetAttribute("offset"));
                                            List<string> Items = new List<string>();
                                            List<int> Values = new List<int>();
                                            XmlReader x = Reader.ReadSubtree();
                                            while (x.Read())
                                            {
                                                switch (x.Name.ToLower())
                                                {
                                                    case "option":
                                                        {
                                                            Items.Add(Reader.GetAttribute("name"));
                                                            Values.Add(Convert.ToInt32(Reader.GetAttribute("value")));
                                                            break;
                                                        }
                                                }
                                            }
                                            Bitmask b;
                                            if (Count > 0)
                                                b = new Bitmask(Description, typeof(byte), Items, Values, RawTranslation, Offset, Map);
                                            else
                                                b = new Bitmask(Description, -1, typeof(byte), Items, Values, RawTranslation, Offset, Map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "bitmask16":
                                        {
                                            string Description = Reader.GetAttribute("name");
                                            int Offset = Convert.ToInt32(Reader.GetAttribute("offset"));
                                            List<string> Items = new List<string>();
                                            List<int> Values = new List<int>();
                                            XmlReader x = Reader.ReadSubtree();
                                            while (x.Read())
                                            {
                                                switch (x.Name.ToLower())
                                                {
                                                    case "option":
                                                        {
                                                            Items.Add(Reader.GetAttribute("name"));
                                                            Values.Add(Convert.ToInt32(Reader.GetAttribute("value")));
                                                            break;
                                                        }
                                                }
                                            }
                                            Bitmask b;
                                            if (Count > 0)
                                                b = new Bitmask(Description, typeof(short), Items, Values, RawTranslation, Offset, Map);
                                            else
                                                b = new Bitmask(Description,-1, typeof(short), Items, Values, RawTranslation, Offset, Map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "bitmask32":
                                        {
                                            string Description = Reader.GetAttribute("name");
                                            int Offset = Convert.ToInt32(Reader.GetAttribute("offset"));
                                            List<string> Items = new List<string>();
                                            List<int> Values = new List<int>();
                                            XmlReader x = Reader.ReadSubtree();
                                            while (x.Read())
                                            {
                                                switch (x.Name.ToLower())
                                                {
                                                    case "option":
                                                        {
                                                            Items.Add(Reader.GetAttribute("name"));
                                                            Values.Add(Convert.ToInt32(Reader.GetAttribute("value")));
                                                            break;
                                                        }
                                                }
                                            }
                                            Bitmask b;
                                            if (Count > 0)
                                                b = new Bitmask(Description, typeof(int), Items, Values, RawTranslation, Offset, Map);
                                            else
                                                b = new Bitmask(Description, -1, typeof(int), Items, Values, RawTranslation, Offset, Map);
                                            Controls.Add(b);
                                            b.Dock = DockStyle.Top;
                                            b.BringToFront();
                                            break;
                                        }
                                    case "string32":
                                        {
                                            //string Description = Reader.GetAttribute("name");
                                            //int Offset = Convert.ToInt32(Reader.GetAttribute("offset")) + Map.SelectedTag.MetaOffset;
                                            //Map.br.BaseStream.Position = Offset;
                                            //string Value = new string(Map.br.ReadChars(32));
                                            //String s = new String(Description, Offset, Value, StringType.String32);
                                            //Controls.Add(s);
                                            //s.Dock = DockStyle.Top;
                                            //s.BringToFront();
                                            break;
                                        }
                                    case "string64":
                                        {
                                            //string Description = Reader.GetAttribute("name");
                                            //int Offset = Convert.ToInt32(Reader.GetAttribute("offset")) + Map.SelectedTag.MetaOffset;
                                            //Map.br.BaseStream.Position = Offset;
                                            //string Value = new string(Map.br.ReadChars(64));
                                            //String s = new String(Description, Offset, Value, StringType.String64);
                                            //Controls.Add(s);
                                            //s.Dock = DockStyle.Top;
                                            //s.BringToFront();
                                            break;
                                        }
                                    case "unicode64":
                                        {
                                            //string Description = Reader.GetAttribute("name");
                                            //int Offset = Convert.ToInt32(Reader.GetAttribute("offset")) + Map.SelectedTag.MetaOffset;
                                            //Map.br.BaseStream.Position = Offset;
                                            //string Value = "";
                                            //for (int i = 0; i < 32; i++)
                                            //{
                                            //    Value += Convert.ToChar(Map.br.ReadByte());
                                            //    Map.br.BaseStream.Position++;
                                            //}
                                            //String s = new String(Description, Offset, Value, StringType.Unicode64);
                                            //Controls.Add(s);
                                            //s.Dock = DockStyle.Top;
                                            //s.BringToFront();
                                            break;
                                        }
                                    case "unicode256":
                                        {
                                            //string Description = Reader.GetAttribute("name");
                                            //int Offset = Convert.ToInt32(Reader.GetAttribute("offset")) + Map.SelectedTag.MetaOffset;
                                            //Map.br.BaseStream.Position = Offset;
                                            //string Value = "";
                                            //for (int i = 0; i < 128; i++)
                                            //{
                                            //    Value += Convert.ToChar(Map.br.ReadByte());
                                            //    Map.br.BaseStream.Position++;
                                            //}
                                            //String s = new String(Description, Offset, Value, StringType.Unicode256);
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
                Reader.Close();
                for (int i = Controls.Count - 1; i > -1; i--)
                    this.panel1.Controls.Add(Controls[i]);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                foreach (Control c in panel1.Controls)
                {
                    if (c.GetType() == typeof(Value))
                        ((Value)c).Update(ChunkSize * comboBox1.SelectedIndex);
                    if (c.GetType() == typeof(Enum))
                        ((Enum)c).Update(ChunkSize * comboBox1.SelectedIndex);
                    if (c.GetType() == typeof(Ident))
                        ((Ident)c).Update(ChunkSize * comboBox1.SelectedIndex);
                    if (c.GetType() == typeof(StringID))
                        ((StringID)c).Update(ChunkSize * comboBox1.SelectedIndex);
                    if (c.GetType() == typeof(Bitmask))
                        ((Bitmask)c).Update(ChunkSize * comboBox1.SelectedIndex);
                }
            }
        }

        private void Reflexive_DoubleClick(object sender, EventArgs e)
        {
            ((Panel)this.Parent).ScrollControlIntoView(this);
        }
    }
}
