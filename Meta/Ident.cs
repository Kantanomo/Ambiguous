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
    public partial class Ident : UserControl
    {
        public string Class;
        public int ident;
        public string Label;
        public int Base;
        public int Offset;
        public int ChunkModifier;
        public Map Map;
        public Ident(string Class, int ident,int Base, int Offset, string Label, Map Map)
        {
            this.Class = Class;
            this.ident = ident;
            this.Base = Base;
            this.Offset = Offset;
            this.Label = Label;
            this.Map = Map;
            InitializeComponent();
            comboBox1.Sorted = true;
            foreach (string s in Map.Tags.TagTypes)
                comboBox1.Items.Add(s);
            comboBox1.Items.Add("Null");
            label1.Text = Label;
            if (Program.Devmode)
                button2.Visible = true;
        }
        public void Update(int ChunkModifier)
        {
            this.ChunkModifier = ChunkModifier;
            Map.Reader.BaseStream.Position = ((Base - Map.Index.SecondaryMagic) + ChunkModifier + Offset);
            Class = Globals.ReverseStr(new string(Map.Reader.ReadChars(4)));
            ident = Map.Reader.ReadInt32();
            if (Class.Trim() != "" && Class != "Null" && Class != "����")
            {
                for (int i = 0; i < comboBox1.Items.Count; i++)
                    if (comboBox1.Items[i].ToString() == Class)
                        comboBox1.SelectedIndex = i;
                if ((uint)ident == 0xffffffff) comboBox2.Text = "Null";
                else
                comboBox2.Text = Map.Tags.NameFromIdent(ident);
            }
            else
            {
                comboBox1.Text = "Null";
                comboBox2.Text = "Null";
            }
        }
        private void Ident_Load(object sender, EventArgs e)
        {
            if (ident == 0)
                Update(0);
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            foreach (TagObject tag in Map.Tags)
                if (tag.Class == comboBox1.SelectedItem.ToString())
                    comboBox2.Items.Add(tag.Path);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() != "Null")
                Poke.PokeIdent(comboBox1.SelectedItem.ToString(), (uint)Map.Tags[comboBox2.SelectedItem.ToString(), comboBox1.SelectedItem.ToString()].Identifier, (Base + ChunkModifier + Offset), Map);
            else
                Poke.PokeIdent("FFFF", 0xFFFFFFFF, (Base + ChunkModifier + Offset), Map);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("halo2.exe+47CD54 / " + Offset.ToString("X"));
        }
    }
}
