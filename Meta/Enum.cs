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
    public partial class Enum : UserControl
    {
        public string Label;
        public int Base;
        public int Offset;
        public int ChunkModifier;
        public Type Type;
        public List<string> Items;
        public List<int> Values;
        public int Index;
        public Map Map;
        public Enum(string Label,int Base, int Offset, Type Type, List<string> Items, List<int> Values, int Index, Map Map)
        {
            this.Label = Label;
            this.Base = Base;
            this.Offset = Offset;
            this.Type = Type;
            this.Items = Items;
            this.Values = Values;
            this.Index = Index;
            this.Map = Map;
            InitializeComponent();
            label1.Text = Label;
            foreach (string s in Items)
                comboBox1.Items.Add(s);
            if (Index == 0)
                Update(0);
            if (Program.Devmode)
                button2.Visible = true;
        }
        public void Update(int ChunkModifier)
        {
            this.ChunkModifier = ChunkModifier;
            Map.Reader.BaseStream.Position = ((Base - Map.Index.SecondaryMagic) + ChunkModifier + Offset);
            if (Type == typeof(byte))
                Index = Map.Reader.ReadByte();
            if (Type == typeof(short))
                Index = Map.Reader.ReadInt16();
            if (Type == typeof(int))
                Index = Map.Reader.ReadInt32();
            for (int i = 0; i < Values.Count; i++)
                if (Values[i] == Index)
                    for (int x = 0; x < comboBox1.Items.Count; x++)
                        if (comboBox1.Items[x].ToString() == Items[i])
                            comboBox1.SelectedIndex = x;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Poke.PokeValue(Type, Index, (Offset + ChunkModifier + Base),Map);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Values.Count; i++)
                if (Items[i] == comboBox1.SelectedItem.ToString())
                    Index = Values[i];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("halo2.exe+47CD54 / " + Offset.ToString("X"));
        }
    }
}
