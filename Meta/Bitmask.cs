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
    public partial class Bitmask : UserControl
    {
        public string Label;
        public Type Type;
        public string Index;
        public List<string> Items;
        public List<int> Values;
        public int Base;
        public int ChunkModifier = 0;
        public int Offset;
        public Map Map;
        public int BitSize;
        public Bitmask(string Label,Type Type, List<string> Items, List<int> Values,int Base, int Offset, Map Map)
        {
            this.Label = Label;
            this.Type = Type;
            this.Items = Items;
            this.Values = Values;
            this.Base = Base;
            this.Offset = Offset;
            this.Map = Map;
            InitializeComponent();
            this.label1.Text = Label;
            checkedListBox1.ScrollAlwaysVisible = true;
            if (Type == typeof(byte)) BitSize = 8;
            if (Type == typeof(short)) BitSize = 16;
            if (Type == typeof(int)) BitSize = 32;
            Update(0);
        }
        public Bitmask(string Label,int index, Type Type, List<string> Items, List<int> Values, int Base, int Offset, Map Map)
        {
            this.Label = Label;
            this.Type = Type;
            this.Items = Items;
            this.Values = Values;
            this.Base = Base;
            this.Offset = Offset;
            this.Map = Map;
            InitializeComponent();
            this.label1.Text = Label;
            checkedListBox1.ScrollAlwaysVisible = true;
            if (Type == typeof(byte)) BitSize = 8;
            if (Type == typeof(short)) BitSize = 16;
            if (Type == typeof(int)) BitSize = 32;
            if (index != -1)
                Update(0);
        }
        public void Update(int ChunkModifier)
        {
            checkedListBox1.Items.Clear();
            this.ChunkModifier = ChunkModifier;
            Map.Reader.BaseStream.Position = ((Base - Map.Index.SecondaryMagic) + Offset + ChunkModifier);
            byte[] Buffer = new byte[0];
            this.Index = Map.Reader.ReadByte().ToString();
            if (Type == typeof(byte))
                Buffer = BitConverter.GetBytes(Convert.ToByte(Index));
            if (Type == typeof(short))
                Buffer = BitConverter.GetBytes(Convert.ToInt16(Index));
            if (Type == typeof(int))
                Buffer = BitConverter.GetBytes(Convert.ToInt32(Index));
            bool[] Bits = ConvertToBoolArray(Buffer);
            List<CheckBox> Checks = new List<CheckBox>();
            for (int i = 0; i < BitSize; i++)
            {
                CheckBox c = new CheckBox();
                if (i < Items.Count)
                {
                    c.Text = Items[i];
                    c.Tag = Values[i];
                }
                else
                {
                    c.Text = "Bit - " + i.ToString();
                    c.Tag = i;
                    c.Visible = false;
                }
                if (Bits[(int)c.Tag] == true)
                    c.Checked = true;
                Checks.Add(c);
            }
            Checks.Reverse();
            foreach (CheckBox c in Checks)
                if (c.Visible == true)
                    checkedListBox1.Items.Add(c.Text, c.Checked);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            List<bool> bits = new List<bool>();
            uint tmp = 1;
            uint tmpVal = 0;
            for (int x = checkedListBox1.Items.Count - 1; x > 0; x--)
            {
                for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
                    if (checkedListBox1.Items[x].ToString() == checkedListBox1.CheckedItems[i].ToString() && checkedListBox1.Items[x].ToString() != "") { tmpVal += tmp; break; }
                tmp <<= 1; //shift
            }
            Poke.PokeValue(typeof(uint), tmpVal, (Base + ChunkModifier + Offset),Map);
        }
        private bool[] ConvertToBoolArray(byte[] Bytes)
        {
            bool[] bitArray = new bool[Bytes.Length * 8];
            int input;
            int bittester;
            for (int i = 0; i < Bytes.Length; i++)
            {
                input = BitConverter.ToInt16(new byte[2] { Bytes[i], 0 }, 0);
                bittester = 1;
                for (int counter = 0; counter < 8; counter++)
                {
                    bitArray[(i * 8) + counter] = (input & bittester) > 0 ? true : false;
                    bittester *= 2;
                }
            }
            return bitArray;
        }
    }
}
