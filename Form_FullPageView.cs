using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tipitaka_DBTables;
using static NissayaEditor.Form_PageReadingView;

namespace NissayaEditor
{
#pragma warning disable CA1416
    // panel1 = 623, 800
    // Form = 640, 800
    public struct ImageSettings
    {
        public string imageFileName;
        public int cropW, cropH;
        // viewport
        public int X0, Y0, W0, H0;
        public int readView_X, readView_Y, readView_W, readView_H;
        public void SetValues(int cropW, int cropH, int x0, int y0, int w0, int h0)
        {
            this.cropW = cropW; this.cropH = cropH;
            this.X0 = x0; this.Y0 = y0; this.W0 = w0; this.H0 = h0;
        }
        public void SetReadView(int x, int y, int w, int h)
        {
            this.readView_X = x; this.readView_Y = y; this.readView_W = w; this.readView_H = h;
        }
        public void Copy(ImageSettings src)
        {
            imageFileName = src.imageFileName;
            cropW = src.cropW;
            cropH = src.cropH;
            X0 = src.X0;
            Y0 = src.Y0;
            W0 = src.W0;
            H0 = src.H0;
            readView_X = src.readView_X;
            readView_Y = src.readView_Y;
            readView_W = src.readView_W;
            readView_H = src.readView_H;
        }
    }
    struct ImageInfo
    {
        public string imageFileName;
        public Bitmap image;
        public ImageSettings imageSettings;
        public ImageInfo()
        {
            imageFileName = string.Empty;
            image = null;
            //Y = 0;
            imageSettings = new ImageSettings();
        }
    }
    public partial class Form_FullPageView : Form
    {
        string imageFile = string.Empty;
        Bitmap orgBitmap;
        Bitmap curImage;
        int cropX, cropY, cropW, cropH;
        Form parent;
        public int readView_X, readView_Y, readView_Width, readView_Height;
        public event EventHandler<EventArgs> OnSetReadingView = null;
        public bool flagExit = false;
        ImageSettings imageSettings = new ImageSettings();
        Dictionary<string, Bitmap> dictImages = new Dictionary<string, Bitmap>();
        Dictionary<string, ImageSettings> dictImageInfo = new Dictionary<string, ImageSettings>();
        SQLiteObj sqlObj = null;
        public Form_FullPageView(Form parent, int x, int y, int w, int h)
        {
            InitializeComponent();
            this.parent = parent;
            cropX = x; cropY = y; cropW = w; cropH = h;
            //Form_PageReadingView formPageReading = (Form_PageReadingView)parent;
            //Form1 mainParent = (Form1)formPageReading.Parent;
            //Form1 mainParent = (Form1)parent;
            Form1 form1 = (Form1)((Form_PageReadingView)parent).parent;
            sqlObj = form1.sqlObj;
        }
        public Form_FullPageView(Form parent)
        {
            InitializeComponent();
            this.parent = parent;
            Form1 form1 = (Form1)((Form_PageReadingView)parent).parent;
            sqlObj = form1.sqlObj;
        }
        public void SaveImageInfo()
        {
            if (sqlObj == null) return;
            foreach (ImageSettings imgSettings in dictImageInfo.Values)
                sqlObj.UpdateSourcePageInfo(imgSettings);
        }
        private void GetImageSettings(Bitmap image, string fileName = "")
        {
            ImageSettings? imgSettings;
            if (dictImageInfo.ContainsKey(fileName))
            {
                imgSettings = dictImageInfo[fileName];
            }
            else
                imgSettings = sqlObj.GetSourcePageInfo(fileName);

            if (imgSettings != null)
            {
                imageSettings.Copy((ImageSettings)imgSettings);
                return;
            }

            imageSettings.imageFileName = fileName;
            int width = 0;
            if (image.Height >= 800) width = image.Width * 800 / image.Height;
            else width = image.Width;

            if (image.Width >= 900)
            {
                imageSettings.SetValues(width, 800, 40, 10, width - 80, 780);
                imageSettings.SetReadView(20, 10, width - 140, 100);
                return;
            }
            if (image.Width >= 650 && image.Width < 900)
            {
                imageSettings.SetValues(width, 800, 0, 10, width, 780);
                imageSettings.SetReadView(10, 10, width - 20, 100);
                return;
            };
            imageSettings.SetValues(image.Width, image.Height, 0, 0, image.Width, image.Height);
            imageSettings.SetReadView(30, 10, width - 60, 100);
            return;
        }
        public ImageSettings GetImageSettings() { return imageSettings; }
        public void SetImage(string imageFile)
        {
            this.imageFile = imageFile;
            try
            {
                if (dictImages.ContainsKey(imageFile))
                {
                    curImage = dictImages[imageFile];
                    imageSettings.Copy(dictImageInfo[imageFile]);
                    return;
                }
                // it is not int dict
                // check if the info is in the database
                orgBitmap = new Bitmap(imageFile);
                GetImageSettings(orgBitmap, imageFile);
                orgBitmap = new Bitmap(orgBitmap, imageSettings.cropW, imageSettings.cropH);
                RectangleF rect = new RectangleF(imageSettings.X0, imageSettings.Y0, imageSettings.W0, imageSettings.H0);
                curImage = orgBitmap.Clone(rect, orgBitmap.PixelFormat);
                this.Text = "Full Page View - " + imageFile;
                dictImages[imageFile] = curImage;
                dictImageInfo[imageFile] = imageSettings;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public Bitmap GetImage() { return curImage; }
        //public void SetImage(Bitmap bitmap, string imageFilename = "")
        //{
        //    this.orgBitmap = bitmap;
        //    try
        //    {
        //        if (cropY + cropH > orgBitmap.Height) { cropH = orgBitmap.Height - cropY; }
        //        if (cropX + cropW > orgBitmap.Width) { cropX = orgBitmap.Width - cropW; }
        //        RectangleF rect = new RectangleF(cropX, cropY, cropW, cropH);
        //        curImage = orgBitmap.Clone(rect, orgBitmap.PixelFormat);
        //        imageFile = imageFilename;
        //        dictImages[imageFilename] = curImage;
        //        this.Text = "Full Page View - " + imageFile;
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message);
        //    }
        //}
        private void FullPageView_MouseClick(object sender, MouseEventArgs e)
        {
            int half_H = (imageSettings.readView_H / 2);
            //imageSettings.readView_Y = e.Y - half_H;
            Y_BoundCheck(e.Y - half_H);
            dictImageInfo[imageFile] = imageSettings;

            //if (imageSettings.readView_Y < 0) imageSettings.readView_Y = 0;
            //if (imageSettings.readView_Y > imageSettings.H0 - imageSettings.readView_H)
            //    imageSettings.readView_Y = imageSettings.H0 - imageSettings.readView_H;
            panel1.Invalidate();
            //SetReadingViewXY(readView_X, readView_Y, readView_Height);

            EventHandler<EventArgs> handler = OnSetReadingView;
            if (handler != null)
            {
                ReadViewBoxEventArgs eargs = new ReadViewBoxEventArgs();
                eargs.X = imageSettings.readView_X;
                eargs.Y = imageSettings.readView_Y;
                eargs.H = imageSettings.readView_H;
                handler(this, eargs);
            }
            Hide();
        }
        private void SetReadingViewXY(int x, int y, int height)
        {
            readView_X = x; readView_Y = y; readView_Height = height;
            readView_Width = panel1.Width - (2 * x);
            if (readView_Y < 0) readView_Y = 0;
        }
        //public void SetReadingView(int x, int y, int height)
        //{
        //    SetReadingViewXY(x, y, height);
        //    //readView_X = x; readView_Y= y; readView_Height = height;
        //    //readView_Width = panel1.Width - (2 * x);
        //    //if (readView_Y < 0) readView_Y = 0;
        //    panel1.Invalidate();
        //}
        public int SetReadingViewY(int y) { return Y_BoundCheck(y); }
        private int Y_BoundCheck(int y)
        {
            imageSettings.readView_Y = y;
            if (imageSettings.readView_Y < 0) imageSettings.readView_Y = 0;
            if (imageSettings.readView_Y > imageSettings.H0 - imageSettings.readView_H)
                imageSettings.readView_Y = imageSettings.H0 - imageSettings.readView_H;
            return imageSettings.readView_Y;
        }
        private void FullPageView_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = (e.CloseReason == CloseReason.UserClosing);
        }

        private void Panel1_OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(curImage, new Point(0, 0));
            //Pen p = new Pen(Color.FromArgb(255, 230, 180, 140), 2);
            Pen p = new Pen(Color.Red, 1);
            //e.Graphics.DrawRectangle(p, readView_X, readView_Y, readView_Width, readView_Height);
            e.Graphics.DrawRectangle(p, imageSettings.readView_X, imageSettings.readView_Y,
                imageSettings.readView_W, imageSettings.readView_H);

        }
    }
#pragma warning restore CA1416
}
