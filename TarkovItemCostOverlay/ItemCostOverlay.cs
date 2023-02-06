using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TarkovItemCostOverlay
{
    public partial class ItemCostOverlay : Form
    {
        #region DLLImports
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        public struct RECT
        {
            public int left, top, right, bottom;
        }
        #endregion

        // Global declarations
        private Graphics g;
        private Rectangle screenBounds;
        private Bitmap screenCapture;
        private System.Drawing.Image screenshot;
        private int startupCount = 0;
        private int vertMove = 0;

        public ItemCostOverlay()
        {
            InitializeComponent();
        }

        private void ItemCostOverlay_Load(object sender, EventArgs e)
        {
            // Set an arbitary color to the background of the Form.
            this.BackColor = Color.Wheat;
            // Set this arbitrary color to full transparency.
            this.TransparencyKey = Color.Wheat;
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.None;

            this.TopMost = true;
            int initialStyle = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);
            WindowState = FormWindowState.Maximized;

            // Get the screen bounds
            screenBounds = Screen.PrimaryScreen.Bounds;
            CaptureFromImage();
        }

        private void ItemCostOverlay_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            //CaptureScreen();
            DrawToScreen();
            if (startupCount > 5)
                ModifyImage();
            else
                startupCount++;
            g.DrawLine(Pens.Purple, new Point(0, 27 + vertMove), new Point(1099, 1027 + vertMove));
            vertMove++;
        }

        private void captureTimer_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void CaptureScreen()
        {
            screenCapture = new Bitmap(screenBounds.Width, screenBounds.Height);
            using (Graphics gfx = Graphics.FromImage(screenCapture))
            {
                gfx.CopyFromScreen(screenBounds.X, screenBounds.Y, 0, 0, screenBounds.Size);
            }
        }
        private void CaptureFromImage()
        {
            screenshot = System.Drawing.Image.FromFile("E:\\Projects\\VisualStudio\\TarkovItemCostOverlay\\Images\\Screenshot1.png");
            Bitmap tmp = (Bitmap)screenshot;
            screenCapture = tmp.Clone(screenBounds, tmp.PixelFormat);
        }
        private void ModifyImage()
        {
            g.DrawLine(Pens.Red, new Point(0, 17), new Point(1099, 1017));
            int pixelCount = 0;
            for (int i = 0; i < screenCapture.Width; i++)
            {
                for (int j = 0; j < screenCapture.Height; j++) 
                { 
                    if (screenCapture.GetPixel(i, j) == Color.FromArgb(188, 199, 205))
                    {
                        pixelCount++;
                        screenCapture.SetPixel(i, j, Color.Red);
                        int it = i;
                        int jt = j;
                        g.DrawLine(Pens.Purple, new Point(it + 1, jt), new Point(it + 100, jt));
                        g.DrawLine(Pens.Purple, new Point(it - 1, jt), new Point(it - 100, jt));
                        g.DrawLine(Pens.Purple, new Point(it, jt + 1), new Point(it, jt + 100));
                        g.DrawLine(Pens.Purple, new Point(it, jt - 1), new Point(it, jt - 100));
                    }
                }
            }
            pixelCount++;
        }
        private void DrawToScreen()
        {
            int width = screenBounds.Width;
            int height = screenBounds.Height;
            int x = screenBounds.Right - width;
            int y = screenBounds.Bottom - height;
            g.DrawImage(screenCapture, new Rectangle(x, y, width, height), new Rectangle(0, 0, screenBounds.Width, screenBounds.Height), GraphicsUnit.Pixel);
        }
    }
}
