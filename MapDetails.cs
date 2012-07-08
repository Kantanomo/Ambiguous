using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ambiguous
{
    public partial class MapDetails : Form
    {
        Map Map;
        public MapDetails(Map map)
        {
            this.Map = map;
            InitializeComponent();
            toolStripComboBox1.SelectedIndex = 0;
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox1.SelectedIndex == 0)
                propertyGrid1.SelectedObject = Map.Header;
            if (toolStripComboBox1.SelectedIndex == 1)
                propertyGrid1.SelectedObject = Map.Index;
        }

    }
}
