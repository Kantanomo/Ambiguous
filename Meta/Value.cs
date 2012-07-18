using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace Ambiguous
{
    public partial class Value : UserControl
    {
        public string Label;
        public Type Type;
        public object value;
        public int length;
        public int Base;
        public int Offset;
        public int ChunkModifier = 0;
        public Map Map;
        public Value(string Label, Type Type, object Value,int Base, int Offset, Map Map)
        {
            this.Label = Label;
            this.Type = Type;
            this.value = Value;
            this.Base = Base;
            this.Offset = Offset;
            this.Map = Map;
            InitializeComponent();
            label1.Text = Label;
            textBox1.Text = Value.ToString();
            if((int)value == 0)
            Update(0);
            if (Program.Devmode)
                button2.Visible = true;
        }
        public Value(string Label, Type Type, object Value,int length, int Base, int Offset, Map Map)
        {
            this.Label = Label;
            this.Type = Type;
            this.value = Value;
            this.length = length;
            this.Base = Base;
            this.Offset = Offset;
            this.Map = Map;
            InitializeComponent();
            label1.Text = Label;
            if (value.ToString() == "")
                Update(0);
        }
        public void Update(int ChunkModifier)
        {
            this.ChunkModifier = ChunkModifier;
            Map.Reader.BaseStream.Position = ((Base - Map.Index.SecondaryMagic) + ChunkModifier + Offset);
            if (Type == typeof(byte))
                value = Map.Reader.ReadByte();
            if (Type == typeof(short))
                value = Map.Reader.ReadInt16();
            if (Type == typeof(ushort))
                value = Map.Reader.ReadUInt16();
            if (Type == typeof(int))
                value = Map.Reader.ReadInt32();
            if (Type == typeof(uint))
                value = Map.Reader.ReadUInt32();
            if (Type == typeof(string))
                value = new string(Map.Reader.ReadChars(length));
            if (Type == typeof(long))
                value = Map.Reader.ReadInt64();
            if (Type == typeof(float))
                value = Map.Reader.ReadSingle();
            textBox1.Text = value.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Poke.PokeValue(Type, (object)textBox1.Text, (Base + Offset + ChunkModifier),Map);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("halo2.exe+47CD54 / " + Offset.ToString("X"));
        }
    }
}
