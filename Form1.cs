using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using H2Memory_class;
namespace Ambiguous
{
    public partial class Form1 : Form
    {
        public Map map;
        public H2Memory H2 = new H2Memory(H2Type.Halo2Vista, false);
        PluginReader p = new PluginReader();
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    bool load = false;
                    if (MessageBox.Show("Is this map currently loaded into Halo 2 Vista?", "", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        load = true;
                    this.map = new Map(ofd.FileName, load);
                }
            if (this.map != null)
            {
                toolStripButton1.Checked = true;
                LoadTreeView();
            }
        }
        #region TreeView
        Hashtable root = new Hashtable();
        private void LoadTreeView()
        {
            if (toolStripButton1.Checked == true)
            {
                treeView1.Nodes.Clear();
                foreach (string s in map.Tags.TagTypes)
                    treeView1.Nodes.Add(s,s);
                foreach (TagObject tag in map.Tags)
                    treeView1.Nodes[tag.Class].Nodes.Add(tag.Path);
            }
            if (toolStripButton2.Checked == true)
            {
                string[] steps;
                string step;
                Hashtable currentDir;
                TreeNode currentNode;
                string path;
                currentDir = root;
                currentDir.Clear();
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(FolderNode(map.Path, ""));
                foreach (TagObject tag in map.Tags)
                {
                    steps = tag.Path.Split('\\');
                    currentDir = root;
                    currentNode = treeView1.Nodes[0];
                    path = "";
                    for (int i = 0; i < (steps.Length - 1); i++)
                    {
                        step = steps[i];
                        path = path + step + "\\";
                        if (!currentDir.ContainsKey(step))
                        {
                            currentDir.Add(step, new Hashtable());
                            TreeNode tn = FolderNode(step, path);
                            tn.Checked = false;
                            currentNode.Nodes.Add(tn);
                        }
                        currentDir = (Hashtable)currentDir[step];
                        currentNode = FindNode(currentNode, path);
                    }
                    currentDir.Add(steps[steps.GetUpperBound(0)] + "." + tag.Class, tag.Index);
                    TreeNode tn1 = FolderNode(steps[steps.GetUpperBound(0)] + "." + tag.Class, tag.Path + "." + tag.Class);
                    tn1.Checked = false;
                    currentNode.Nodes.Add(tn1);
                }
                treeView1.Nodes[0].Expand();
            }
        }
        private TreeNode FolderNode(string text, string path)
        {
            TreeNode node = new TreeNode(text);
            node.Tag = path;
            return node;
        }
        private TreeNode FindNode(TreeNode root, string path)
        {
            for (int i = 0; i < root.Nodes.Count; i++)
            {
                if ((string)root.Nodes[i].Tag == path)
                    return root.Nodes[i];
                if (path.StartsWith((string)root.Nodes[i].Tag))
                    return FindNode(root.Nodes[i], path);
            }
            return root;
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            toolStripButton2.Checked = false;
            LoadTreeView();
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            toolStripButton1.Checked = false;
            LoadTreeView();
        }
        #endregion
        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new MapDetails(map).Show();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Level > 0)
            {
                Panel MetaPanel = ((Panel)tabControl1.SelectedTab.Controls[0]);
                MetaPanel.Controls.Clear();
                if (toolStripButton1.Checked == true)
                {
                    tabControl1.SelectedTab.Text = treeView1.SelectedNode.Text.Split('\\')[treeView1.SelectedNode.Text.Split('\\').Length - 1] + "." + treeView1.SelectedNode.Parent.Text;
                    p.Read(map.Tags[treeView1.SelectedNode.Text, treeView1.SelectedNode.Parent.Text].RawMetaOffset, treeView1.SelectedNode.Parent.Text, ref MetaPanel, map.Tags[treeView1.SelectedNode.Text, treeView1.SelectedNode.Parent.Text], map);
                }
                if (toolStripButton2.Checked == true)
                {
                    if (treeView1.SelectedNode.Text.Contains('.'))
                    {
                        tabControl1.SelectedTab.Text = treeView1.SelectedNode.Tag.ToString().Split('\\')[treeView1.SelectedNode.Tag.ToString().Split('\\').Length - 1];
                        p.Read(map.Tags[treeView1.SelectedNode.Tag.ToString().Split('.')[0], treeView1.SelectedNode.Tag.ToString().Split('.')[1]].RawMetaOffset, treeView1.SelectedNode.Text.Split('.')[1], ref MetaPanel, map.Tags[treeView1.SelectedNode.Tag.ToString().Split('.')[0], treeView1.SelectedNode.Tag.ToString().Split('.')[1]], map);
                    }
                }
            }
        }

        private void newTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage NewPage = new TabPage();
            NewPage.BackColor = System.Drawing.Color.DarkOliveGreen;
            NewPage.Controls.Add(new Panel());
            ((Panel)NewPage.Controls[0]).Controls.Clear();
            ((Panel)NewPage.Controls[0]).Dock = DockStyle.Fill;
            ((Panel)NewPage.Controls[0]).AutoScroll = true;
            NewPage.Text = "New Tab";
            tabControl1.TabPages.Add(NewPage);
        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != 0)
                tabControl1.TabPages.RemoveAt(tabControl1.SelectedIndex);
            else
            {
                tabPage1.Text = "New Tab";
                tabPage1.Controls[0].Controls.Clear();
            }
        }
    }
}
