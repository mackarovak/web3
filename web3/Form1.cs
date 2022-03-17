using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace web3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            historyBox.Text = File.ReadAllText("history.txt");
            bookmarks.Items.AddRange(File.ReadAllLines("bookmarks.txt"));
        }

        ListBox bookmarks = new ListBox();
        RichTextBox historyBox = new RichTextBox();

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();

        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && toolStripTextBox1.Text != null && tabControl1.SelectedTab.Text != "История" && tabControl1.SelectedTab.Text != "Закладки")
            {
                webBrowser1.Navigate(toolStripTextBox1.Text);
                File.AppendAllText("history.txt", toolStripTextBox1.Text + "\n");
                historyBox.Text = File.ReadAllText("history.txt");
            }
            else { }
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text != null && tabControl1.SelectedTab.Text != "История" && tabControl1.SelectedTab.Text != "Закладки")
            {
                webBrowser1.Navigate(toolStripTextBox1.Text);
                File.AppendAllText("history.txt", toolStripTextBox1.Text + "\n");
                historyBox.Text = File.ReadAllText("history.txt");
            }
            else { }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text != "История" && tabControl1.SelectedTab.Text != "Закладки")
            {
                webBrowser1.Refresh();
            }
            else { }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text != null && tabControl1.SelectedTab.Text != "История" && tabControl1.SelectedTab.Text != "Закладки")
            {
                webBrowser1.Navigate(toolStripTextBox1.Text);
                File.AppendAllText("history.txt", toolStripTextBox1.Text + "\n");
                historyBox.Text = File.ReadAllText("history.txt");
            }
            else { }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text != "Новая вкладка" && tabControl1.SelectedTab.Text != "История" && tabControl1.SelectedTab.Text != "Закладки")
            {
                webBrowser1.Stop();
            }
        }

        int tabCounter = 0;

        private void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            tabControl1.SelectedTab.Text = ((WebBrowser)tabControl1.SelectedTab.Controls[0]).DocumentTitle;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            WebBrowser browser = new WebBrowser();
            browser.Visible = true;
            browser.ScriptErrorsSuppressed = true;
            browser.Dock = DockStyle.Fill;
            browser.DocumentCompleted += Browser_DocumentCompleted;
            tabControl1.TabPages.Add("Новая вкладка");
            tabControl1.SelectTab(tabCounter+1);
            tabControl1.SelectedTab.Controls.Add(browser);
            tabCounter++;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count > 1)
            {
                tabControl1.TabPages.RemoveAt(tabControl1.SelectedIndex);
                tabControl1.SelectTab(tabControl1.TabPages.Count - 1);
                tabCounter--;
            }
            else
            {
                this.Close();
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            File.AppendAllText("bookmarks.txt", tabControl1.SelectedTab.Text + "\n");
            bookmarks.Items.Add(tabControl1.SelectedTab.Text + "\n");
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
             webBrowser1.GoHome();
        }

        private void историяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add("История");
            tabControl1.SelectTab(tabCounter);
            tabControl1.SelectedTab.Controls.Add(historyBox);
            historyBox.Dock = DockStyle.Fill;
            tabCounter++;
        }

        private void закладкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add("Закладки");
            tabControl1.SelectTab(tabCounter);
            tabControl1.SelectedTab.Controls.Add(bookmarks);
            bookmarks.Dock = DockStyle.Fill;
            bookmarks.ContextMenuStrip = deleteHistoryMenu;
            tabCounter++;
        }

        private void удалитьИзЗакладокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
                {
                 List<string> bookmarksContent = new List<string>();
                 bookmarksContent.AddRange(File.ReadAllLines("bookmarks.txt"));
                 bookmarksContent.RemoveAt(bookmarks.SelectedIndex);
                 File.WriteAllLines("bookmarks.txt", bookmarksContent);
                 bookmarks.Items.RemoveAt(bookmarks.SelectedIndex);
                }
            catch { }
        }

    }
}
