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
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Drawing2D;
using Azure.Storage.Files.Shares.Models;

namespace NissayaEditor
{
#pragma warning disable CA1416
    public partial class Form_PageReadingView : Form
    {
        public Form1 parent;
        Bitmap orgBitmap;
        Bitmap baseImage;
        Bitmap curImage;
        Bitmap viewImage;
        Bitmap viewImageZoomedIn;
        Bitmap viewImageZoomedOut;
        public string imageFileName = string.Empty;
        const int readViewHeight = 100;
        public int readView_Y = 0;
        public int readView_X = 30;
        int moveOffset = 55;

        int cropX = 0; //40, 10, 560, 780
        int cropY = 10;
        int cropW = 560;
        int cropH = 800;

        bool flagGuide = false;
        bool controlKeyPressed = false;

        const string UpArrowFile = "./Images/UpArrow26x26.jpg";
        const string DownArrowFile = "./Images/DownArrow26x26.jpg";
        const string SquareFrameFile = "./Images/SquareFrame26x26.jpg";
        public Form_FullPageView form_FullPageView = null;
        Rectangle rect = new Rectangle();
        ImageSettings imageSettings = new ImageSettings();

        public Form_PageReadingView(Form1 parent)
        {
            InitializeComponent();
            this.parent = parent;
            string dir = Directory.GetCurrentDirectory();
            Owner = parent;
            if (File.Exists(UpArrowFile)) button_UpArrow.Image = System.Drawing.Image.FromFile(UpArrowFile);
            if (File.Exists(DownArrowFile)) button_DownArrow.Image = System.Drawing.Image.FromFile(DownArrowFile);
            if (File.Exists(SquareFrameFile)) button_FullPageView.Image = System.Drawing.Image.FromFile(SquareFrameFile);

            MyToolTip ttp = new MyToolTip(Form1.DefaultFont);
            //ToolTip ttp = new ToolTip();
            //ttp.OwnerDraw = true;
            //ttp.Draw += new DrawToolTipEventHandler(toolTip1_Draw);
            //ttp.Popup += new PopupEventHandler(toolTip1_Popup);
            //ttp.BackColor = Color.Yellow;
            //ttp.ForeColor = Color.Black;
            ttp.SetToolTip(button_X, "Close");
            ttp.SetToolTip(button_ZoomIn, "Zoom in");
            ttp.SetToolTip(button_ZoomOut, "Zoom out");
            ttp.SetToolTip(button_UpArrow, "Scroll up");
            ttp.SetToolTip(button_DownArrow, "Scroll down");
            ttp.SetToolTip(button_FullPageView, "Full page view");

            //this.Size = new Size(this.Size.Width, this.Size.Height - 10);
            //panel_ReadView.Location = new Point(0, 0);
            //panel_ReadView.Height = this.ClientRectangle.Height;

            form_FullPageView = new Form_FullPageView(this); //, cropX, cropY, cropW, cropH);
            form_FullPageView.OnSetReadingView += OnSetReadingView;
            BringToFront();
        }
        //void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        //{
        //    Font f = new Font("Myanmar Text", 10.0f);
        //    ToolTip ttp = (ToolTip)sender;
        //    ttp.BackColor = System.Drawing.Color.Yellow;
        //    e.DrawBackground();
        //    e.DrawBorder();
        //    e.Graphics.DrawString(e.ToolTipText, f, Brushes.Black, new PointF(6, 4));
        //}

        //void toolTip1_Popup(object sender, PopupEventArgs e)
        //{
        //    ToolTip ttp = (ToolTip)sender;
        //    string s = ttp.GetToolTip(e.AssociatedControl);
        //    // on popip set the size of tool tip
        //    e.ToolTipSize = TextRenderer.MeasureText(s, new Font("Myanmar Text", 10.0f));
        //}
        public void SaveData() {  form_FullPageView.SaveImageInfo(); }
        //public void ShowFullPage(string imageFile, int y = 340)
        //{
        //    imageFileName = imageFile;
        //    form_FullPageView.SetImage(imageFile);

            //Bitmap sourceMap = new Bitmap(imageFileName);
            //int h = sourceMap.Height;
            //int w = sourceMap.Width * cropH/h;
            //orgBitmap = new Bitmap(new Bitmap(imageFile), w, cropH);// 640, 800);
            //form_FullPageView.SetImage(orgBitmap, imageFile);
            //if (cropY + cropH > orgBitmap.Height) { cropH = orgBitmap.Height - cropY; }
            //if (cropX + cropW > orgBitmap.Width) { cropW = orgBitmap.Width - cropX; }
            //RectangleF rect = new RectangleF(cropX, cropY, cropW, cropH);
            //curImage = orgBitmap.Clone(rect, orgBitmap.PixelFormat);
            //baseImage = orgBitmap.Clone(rect, orgBitmap.PixelFormat);

            //rect = new RectangleF(0, y, curImage.Width, readViewHeight);
            //viewImageZoomedOut = viewImage = curImage.Clone(rect, curImage.PixelFormat);

            //viewImage = new Bitmap(viewImage, (int)(viewImage.Width * 1.25), (int)(viewImage.Height * 1.25));
            //rect = new RectangleF(60, 0, viewImage.Width - 120, readViewHeight);
            //viewImageZoomedIn = viewImage.Clone(rect, viewImage.PixelFormat);
            //viewImage = viewImageZoomedOut;

            //readView_Y = y;
            //form_FullPageView.SetReadingView(30, y, readViewHeight);
            //form_FullPageView.Show(Owner);
            //panel_ReadView.Invalidate();
        //}
        public void Show(string imageFile, int y = 340)
        {
            form_FullPageView.SetImage(imageFile);
            imageFileName = imageFile;
            curImage = form_FullPageView.GetImage();
            imageSettings.Copy(form_FullPageView.GetImageSettings());   // get the image settings from form_FullPageView
            //CreateImages(imageFile, y);
            CreateReadViewImages();
            base.Show();
            BringToFront();
        }
        //private void CreateImages(string imageFile, int y = 340)
        //{
        //    imageFileName = imageFile;
        //    orgBitmap = new Bitmap(new Bitmap(imageFile), 640, 800);
        //    form_FullPageView.SetImage(orgBitmap, imageFile);
        //    RectangleF rect = new RectangleF(40, 10, 560, 780);
        //    curImage = orgBitmap.Clone(rect, orgBitmap.PixelFormat);
        //    CreateReadViewImages(y);
        //    readView_Y = y;
        //    panel_ReadView.Invalidate();
        //}
        private void CreateReadViewImages()
        {
            imageSettings.readView_Y = form_FullPageView.SetReadingViewY(imageSettings.readView_Y);
            RectangleF rect = new RectangleF(imageSettings.readView_X, imageSettings.readView_Y, imageSettings.readView_W, imageSettings.readView_H);
            viewImageZoomedOut = viewImage = curImage.Clone(rect, curImage.PixelFormat);

            int zoomW = (int)(viewImage.Width * 1.35);
            int zoomH = (int)(viewImage.Height * 1.35);
            viewImage = new Bitmap(viewImage, zoomW, zoomH);
            rect = new RectangleF(0, 0, zoomW, zoomH);
            viewImageZoomedIn = viewImage.Clone(rect, viewImage.PixelFormat);
            viewImage = viewImageZoomedOut;
        }
        //private void CreateReadViewImages(int y)
        //{
        //    imageSettings.readView_Y = y;
        //    form_FullPageView.SetReadingViewY(imageSettings.readView_Y);
        //    RectangleF rect = new RectangleF(0, y, curImage.Width, readViewHeight);
        //    viewImageZoomedOut = viewImage = curImage.Clone(rect, curImage.PixelFormat);

        //    viewImage = new Bitmap(viewImage, (int)(viewImage.Width * 1.25), (int)(viewImage.Height * 1.25));
        //    rect = new RectangleF(60, 0, viewImage.Width - 120, readViewHeight);
        //    viewImageZoomedIn = viewImage.Clone(rect, viewImage.PixelFormat);
        //    viewImage = viewImageZoomedOut;
        //}
        private void button_X_Click(object sender, EventArgs e)
        {
            flagGuide = !flagGuide;
            panel_ReadView.Invalidate();
        }
        public new void Hide()
        {
            if (form_FullPageView != null) form_FullPageView.Hide();
            base.Hide();
        }
        //private void PageReadingView_OnPaint(object sender, PaintEventArgs e)
        //{
        //    rect.Location = new Point(0, 0);
        //    rect.Size = new Size(this.Size.Width, this.Size.Height);
        //    Pen pen = new Pen(ForeColor, 2);
        //    e.Graphics.DrawRectangle(pen, 0, 0, rect.Size.Width, rect.Size.Height);
        //}

        private void PageReadingView_OnPaint(object sender, PaintEventArgs e)
        {
            //e.Graphics.DrawImage(viewImage, new Point(0, 0));
            panel_ReadView.BackgroundImageLayout = ImageLayout.None;
            panel_ReadView.BackgroundImage = viewImage;
            if (flagGuide)
            {
                Pen pen = new Pen(Color.Blue, 1);
                Point pt1 = new Point(0, readViewHeight / 2);
                Point pt2 = new Point(viewImage.Width, readViewHeight / 2);
                e.Graphics.DrawLine(pen, pt1, pt2);
            }
        }

        private void button_ZoomIn_Click(object sender, EventArgs e)
        {
            viewImage = viewImageZoomedIn;
            panel_ReadView.Invalidate();
        }

        private void button_ZoomOut_Click(object sender, EventArgs e)
        {
            viewImage = viewImageZoomedOut;
            panel_ReadView.Invalidate();
        }

        private void PageReadingView_OnClosing(object sender, FormClosingEventArgs e)
        {
            form_FullPageView.Hide();
            Hide();
            e.Cancel = (e.CloseReason == CloseReason.UserClosing);
        }
        void OnSetReadingView(object sender, EventArgs e)
        {
            ReadViewBoxEventArgs eargs = (ReadViewBoxEventArgs)e;
            imageSettings.readView_Y = eargs.Y;
            CreateReadViewImages();
            panel_ReadView.Invalidate();
        }
        private void button_FullPageView_Click(object sender, EventArgs e)
        {
            //form_FullPageView.SetReadingView(readView_X, readView_Y, readViewHeight);
            form_FullPageView.Show(Owner);
        }
        private void button_UpArrow_Click(object sender, EventArgs e)
        {
            int offset = moveOffset;
            if (controlKeyPressed) offset = 5;
            imageSettings.readView_Y -= offset;
            if (imageSettings.readView_Y < 0) imageSettings.readView_Y = 0;
            CreateReadViewImages();
            panel_ReadView.Invalidate();
        }

        private void button_DownArrow_Click(object sender, EventArgs e)
        {
            int offset = moveOffset;
            if (controlKeyPressed) offset = 5;
            imageSettings.readView_Y += offset;
            int lowest = curImage.Height - 90;
            if (imageSettings.readView_Y > lowest) imageSettings.readView_Y = lowest;
            CreateReadViewImages();
            panel_ReadView.Invalidate();
        }

        private void UpDown_KeyAction(object sender, KeyEventArgs e)
        {
            controlKeyPressed = e.Control;
        }
    }
    public class ReadViewBoxEventArgs : EventArgs
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int H { get; set; }
    }
#pragma warning restore CA1416
}
