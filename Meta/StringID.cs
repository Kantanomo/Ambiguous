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
    public partial class StringID : UserControl
    {
        public string Label;
        public int Offset;
        public int Base;
        public int ChunkModifier;
        public Map Map;
        public int Value;
        public StringID(string Label,int Base, int Offset,int Value, Map Map)
        {
            this.Label = Label;
            this.Base = Base;
            this.Offset = Offset;
            this.Map = Map;
            this.Value = Value;
            InitializeComponent();
            label1.Text = Label;
            foreach (SIDObject s in Map.StringTable)
                comboBox1.Items.Add(s.Name);
            if (Value == 0)
                Update(0);
            if (Program.Devmode)
                button2.Visible = true;
        }
        public void Update(int ChunkModifier)
        {
            this.ChunkModifier = ChunkModifier;
            Map.Reader.BaseStream.Position = ((Base - Map.Index.SecondaryMagic) + Offset + ChunkModifier);
            comboBox1.Text = Map.StringTable.StringFromIdent(Map.Reader.ReadInt32());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Poke.PokeValue(typeof(uint),Map.StringTable.IndetFromString(comboBox1.SelectedItem.ToString()),(Base + ChunkModifier + Offset), Map);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("halo2.exe+47CD54 / " + Offset.ToString("X"));
        }
    }
}
