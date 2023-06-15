using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;

namespace WunschBox
{
    public partial class Form1 : Form
    {
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        private struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }


        System.Timers.Timer timer = new System.Timers.Timer(100);
        System.Timers.Timer timer2 = new System.Timers.Timer(10000);
        DateTime lastRequestDate = DateTime.Now;

        public Form1()
        {
            InitializeComponent();
            if (Properties.Settings.Default.Opacity == 0)
            {
                Properties.Settings.Default.Opacity = 0.1;
                Properties.Settings.Default.Save();
            }
            this.Opacity = Properties.Settings.Default.Opacity;
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
            timer.Elapsed += OnTimeEvent;
            timer.Enabled = true;
            timer2.Elapsed += OnTimeEvent2;
            timer2.Enabled = true;
        }
        private void OnTimeEvent2(object? sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                var request = WebRequest.Create("https://yourDomain.de/plugins/radio_wunschbox/mosi.php?mosi=" + System.Web.HttpUtility.UrlEncode(lastRequestDate.ToString("yyyy-MM-dd HH:mm:ss")));
                var response = request.GetResponse();
                var responseString = "";
                using (var st = new StreamReader(response.GetResponseStream()))
                {
                    responseString = st.ReadToEnd();
                }

                if (!string.IsNullOrEmpty(responseString))
                {
                    var songs = JsonConvert.DeserializeObject<List<SongWuensche>>(responseString);
                    if (songs.Count > 0)
                    {
                        ChangeText(richTextBox1, Color.Black);
                        foreach (var song in songs)
                        {
                            AppendText(richTextBox1, "Neuer Songwunsch von " + System.Web.HttpUtility.HtmlDecode(song.von_wen) + " für " + System.Web.HttpUtility.HtmlDecode(song.fuer_wen) + " am " + DateTime.Now, Color.Red);
                            AppendText(richTextBox1, "Lied: " + System.Web.HttpUtility.HtmlDecode(song.wunschtitel), Color.Blue);
                            AppendText(richTextBox1, "Gruß: " + System.Web.HttpUtility.HtmlDecode(song.text), Color.Green);
                        }
                    }
                }
                lastRequestDate = DateTime.Now;
            }
            catch(Exception ex) 
            {
                AppendText(richTextBox1, "IRGENDWAS IST SCHIEF GELAUFEN: " + ex.ToString(), Color.Red);
            }
           
        }

        private void AppendText(RichTextBox box, string text, Color color)
        {
            if (box.InvokeRequired)
            {
                Action saveBox = delegate { AppendText(box, text, color); };
                box.Invoke(saveBox);
            }
            else
            {
                box.SelectionStart = box.TextLength;
                box.SelectionLength = 0;

                box.SelectionColor = color;
                box.AppendText(text);
                box.SelectionColor = box.ForeColor;
                box.AppendText(Environment.NewLine);
            }
        }

        private void ChangeText(RichTextBox box, Color color)
        {
            if (box.InvokeRequired)
            {
                Action saveBox = delegate { ChangeText(box, color); };
                box.Invoke(saveBox);
            }
            else
            {
                box.SelectionStart = box.TextLength;
                box.SelectionLength = 0;

                box.SelectionColor = color;
                box.Text = box.Text;
            }
        }

        private void OnTimeEvent(object? sender, System.Timers.ElapsedEventArgs e)
        {
            if (Process.GetProcessesByName(Properties.Settings.Default.Application).Length == 0)
            {
                if (this.InvokeRequired)
                {
                    Action saveState = delegate { OnTimeEvent(sender, e); };
                    this.Invoke(saveState);
                }
                else
                {
                    this.WindowState = FormWindowState.Normal;
                }
            }
            else
            {
                foreach (Process p in Process.GetProcessesByName(Properties.Settings.Default.Application))
                {
                    if (p.MainWindowHandle != IntPtr.Zero)
                    {
                        if (p.MainWindowTitle.Contains("Notepad"))
                        {
                            WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
                            GetWindowPlacement(p.MainWindowHandle, ref placement);
                            switch (placement.showCmd)
                            {
                                case 1:
                                    if (this.InvokeRequired)
                                    {
                                        Action saveState = delegate { OnTimeEvent(sender, e); };
                                        this.Invoke(saveState);
                                    }
                                    else
                                    {
                                        this.WindowState = FormWindowState.Normal;
                                    }
                                    break;
                                case 2:
                                    if (this.InvokeRequired)
                                    {
                                        Action saveState = delegate { OnTimeEvent(sender, e); };
                                        this.Invoke(saveState);
                                    }
                                    else
                                    {
                                        this.WindowState = FormWindowState.Minimized;
                                    }
                                    break;
                                case 3:
                                    if (this.InvokeRequired)
                                    {
                                        Action saveState = delegate { OnTimeEvent(sender, e); };
                                        this.Invoke(saveState);
                                    }
                                    else
                                    {
                                        this.WindowState = FormWindowState.Normal;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                var settings = new Settings(this);
                settings.Show();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void ChangeOpacity(double opacity)
        {
            this.Opacity = opacity;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                Properties.Settings.Default.Location = RestoreBounds.Location;
                Properties.Settings.Default.Size = RestoreBounds.Size;
                Properties.Settings.Default.Maximised = true;
                Properties.Settings.Default.Minimised = false;
            }
            else if (WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.Location = Location;
                Properties.Settings.Default.Size = Size;
                Properties.Settings.Default.Maximised = false;
                Properties.Settings.Default.Minimised = false;
            }
            else
            {
                Properties.Settings.Default.Location = RestoreBounds.Location;
                Properties.Settings.Default.Size = RestoreBounds.Size;
                Properties.Settings.Default.Maximised = false;
                Properties.Settings.Default.Minimised = true;
            }
            Properties.Settings.Default.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Maximised)
            {
                Location = Properties.Settings.Default.Location;
                WindowState = FormWindowState.Maximized;
                Size = Properties.Settings.Default.Size;
            }
            else if (Properties.Settings.Default.Minimised)
            {
                Location = Properties.Settings.Default.Location;
                WindowState = FormWindowState.Minimized;
                Size = Properties.Settings.Default.Size;
            }
            else
            {
                Location = Properties.Settings.Default.Location;
                Size = Properties.Settings.Default.Size;
            }
        }
    }
}