using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Rally.Lib.Utility.Imaging
{
    public class WaterMark
    {
        private byte[] imageBytes;
        private int _width;
        private int _height;
        private string _fontFamily;
        private int _fontSize;
        private bool _adaptable;
        private FontStyle _fontStyle;
        private Color _fontColor;
        private bool _shadow;
        private string _backgroundImage;
        private Color _bgColor;
        private int _left;
        private string _resultImage;
        private string _text;
        private int _top;
        private int _alpha;
        private int _red;
        private int _green;
        private int _blue;
        private long _quality;


        public byte[] ImageBytes
        {
            get
            {
                return this.imageBytes;
            }
            set
            {
                this.imageBytes = value;
            }
        }


        public WaterMark()
        {
            _width = 460;
            _height = 30;
            _fontFamily = "华文行楷";
            _fontSize = 20;
            _fontStyle = FontStyle.Regular;
            _adaptable = true;
            _shadow = false;
            _left = 0;
            _top = 0;
            _alpha = 255;
            _red = 0;
            _green = 0;
            _blue = 0;
            _backgroundImage = "";
            _quality = 100;
            _bgColor = Color.FromArgb(255, 229, 229, 229);

        }

        /**/
        /// <summary>
        /// 字体
        /// </summary>
        public string FontFamily
        {
            set { this._fontFamily = value; }
        }

        /**/
        /// <summary>
        /// 文字大小
        /// </summary>
        public int FontSize
        {
            set { this._fontSize = value; }
        }

        /**/
        /// <summary>
        /// 文字风格
        /// </summary>
        public FontStyle FontStyle
        {
            get { return _fontStyle; }
            set { _fontStyle = value; }
        }

        public Color FontColor
        {
            get => this._fontColor;
            set => this._fontColor = value;
        }


        /**/
        /// <summary>
        /// 透明度0-255,255表示不透明
        /// </summary>
        public int Alpha
        {
            get { return _alpha; }
            set { _alpha = value; }
        }

        /**/
        /// <summary>
        /// 水印文字是否使用阴影
        /// </summary>
        public bool Shadow
        {
            get { return _shadow; }
            set { _shadow = value; }
        }

        public int Red
        {
            get { return _red; }
            set { _red = value; }
        }

        public int Green
        {
            get { return _green; }
            set { _green = value; }
        }

        public int Blue
        {
            get { return _blue; }
            set { _blue = value; }
        }

        /**/
        /// <summary>
        /// 底图
        /// </summary>
        public string BackgroundImage
        {
            set { this._backgroundImage = value; }
        }

        /**/
        /// <summary>
        /// 水印文字的左边距
        /// </summary>
        public int Left
        {
            set { this._left = value; }
        }


        /**/
        /// <summary>
        /// 水印文字的顶边距
        /// </summary>
        public int Top
        {
            set { this._top = value; }
        }

        /**/
        /// <summary>
        /// 生成后的图片
        /// </summary>
        public string ResultImage
        {
            set { this._resultImage = value; }
        }

        /**/
        /// <summary>
        /// 水印文本
        /// </summary>
        public string Text
        {
            set { this._text = value; }
        }


        /**/
        /// <summary>
        /// 生成图片的宽度
        /// </summary>
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /**/
        /// <summary>
        /// 生成图片的高度
        /// </summary>
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        /**/
        /// <summary>
        /// 若文字太大，是否根据背景图来调整文字大小，默认为适应
        /// </summary>
        public bool Adaptable
        {
            get { return _adaptable; }
            set { _adaptable = value; }
        }

        public Color BgColor
        {
            get { return _bgColor; }
            set { _bgColor = value; }
        }

        /**/
        /// <summary>
        /// 输出图片质量，质量范围0-100,类型为long
        /// </summary>
        public long Quality
        {
            get { return _quality; }
            set { _quality = value; }
        }

        /**/
        /// <summary>
        /// 生成水印效果图
        /// </summary>
        /// <returns></returns>
        public bool Create()
        {
            Bitmap bitmap;
            Graphics g;

            //使用纯背景色
            if (this._backgroundImage.Trim() == "")
            {
                bitmap = new Bitmap(this._width, this._height, PixelFormat.Format64bppArgb);
                g = Graphics.FromImage(bitmap);
                g.Clear(this._bgColor);
            }
            else
            {
                bitmap = new Bitmap(Image.FromFile(this._backgroundImage));
                g = Graphics.FromImage(bitmap);
            }
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;

            Font f = new Font(_fontFamily, _fontSize, _fontStyle);
            //Font f1 = new Font(_fontFamily, _fontSize+2, _fontStyle);
            SizeF size = g.MeasureString(_text, f);

            // 调整文字大小直到能适应图片尺寸
            while (_adaptable == true && size.Width > bitmap.Width)
            {
                _fontSize--;
                f = new Font(_fontFamily, _fontSize, _fontStyle);
                size = g.MeasureString(_text, f);
            }

            Brush b = new SolidBrush(Color.FromArgb(_alpha, _red, _green, _blue));
            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Near;

            if (this._shadow)
            {
                SizeF textSize;
                //Graphics g;
                Brush myBackBrush = Brushes.Blue;
                Brush myForeBrush = Brushes.White;
                //Font myFont = new Font("Times New Roman", (float)this.nudFontSize.Value, FontStyle.Regular);
                float xLocation, yLocation;

                //g = picDemoArea.CreateGraphics();
                //g.Clear(Color.White);

                textSize = g.MeasureString(_text, f);

                xLocation = (1024 - textSize.Width) / 2;
                yLocation = (768 - textSize.Height) / 2;

                //g.DrawString(_text, f, myBackBrush,
                //  xLocation + 2,
                //   yLocation + 1);

                //g.DrawString(_text, f, myForeBrush, xLocation,
                //   yLocation);


                //Brush b2 = new SolidBrush(Color.FromArgb(150, 255, 255, 255));
                //g.DrawString(_text, f1, b2, new PointF(_left, _top), StrFormat);
                Brush b2 = new SolidBrush(Color.FromArgb(150, 255, 255, 255));
                g.DrawString(_text, f, b2, _left + 1, _top + 2);
            }
            g.DrawString(_text, f, b, new PointF(_left, _top), StrFormat);

            bitmap.Save(this._resultImage, ImageFormat.Jpeg);
            bitmap.Dispose();
            g.Dispose();
            return true;
        }

        /**/
        /// <summary>
        /// 生成水印效果图
        /// </summary>
        /// <returns></returns>
        public bool Create(ref byte[] OutputBuffer)
        {
            Bitmap bitmap = new Bitmap(this._width, this._height, PixelFormat.Format64bppArgb);
            Graphics g = Graphics.FromImage(bitmap);

            //使用纯背景色
            if (this.imageBytes != null)
            {
                if (this.imageBytes.Length > 0)
                {
                    MemoryStream memoryStream = new MemoryStream(imageBytes);
                    bitmap = new Bitmap(Image.FromStream(memoryStream));
                    g = Graphics.FromImage(bitmap);
                    memoryStream.Flush();
                    memoryStream.Close();
                }
            }
            else if (this._backgroundImage.Trim() != "")
            {
                bitmap = new Bitmap(Image.FromFile(this._backgroundImage));
                g = Graphics.FromImage(bitmap);
            }
            else
            {
                bitmap = new Bitmap(this._width, this._height, PixelFormat.Format64bppArgb);
                g = Graphics.FromImage(bitmap);
                g.Clear(this._bgColor);
            }

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;


            Font f = new Font(_fontFamily, _fontSize, _fontStyle);
            //Font f1 = new Font(_fontFamily, _fontSize+2, _fontStyle);
            SizeF size = g.MeasureString(_text, f);

            // 调整文字大小直到能适应图片尺寸
            while (_adaptable == true && size.Width > bitmap.Width)
            {
                _fontSize--;
                f = new Font(_fontFamily, _fontSize, _fontStyle);
                size = g.MeasureString(_text, f);
            }

            Brush b = new SolidBrush(this._fontColor); //new SolidBrush(Color.FromArgb(_alpha, _red, _green, _blue));
            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Near;

            if (this._top <= 0)
            {
                this._top = (bitmap.Height / 3 - 1);
            }

            if (this._shadow)
            {
                SizeF textSize;
                //Graphics g;
                Brush myBackBrush = Brushes.Blue;
                Brush myForeBrush = Brushes.White;
                //Font myFont = new Font("Times New Roman", (float)this.nudFontSize.Value, FontStyle.Regular);
                float xLocation, yLocation;

                //g = picDemoArea.CreateGraphics();
                //g.Clear(Color.White);

                textSize = g.MeasureString(_text, f);

                xLocation = (1024 - textSize.Width) / 2;
                yLocation = (768 - textSize.Height) / 2;

                //g.DrawString(_text, f, myBackBrush,
                //  xLocation + 2,
                //   yLocation + 1);

                //g.DrawString(_text, f, myForeBrush, xLocation,
                //   yLocation);


                //Brush b2 = new SolidBrush(Color.FromArgb(150, 255, 255, 255));
                //g.DrawString(_text, f1, b2, new PointF(_left, _top), StrFormat);
                Brush b2 = new SolidBrush(this._fontColor); //new SolidBrush(Color.FromArgb(150, 255, 255, 255));
                //b2 = new SolidBrush(Color.OrangeRed);

                g.DrawString(_text, f, b2, _left + 1, _top + 2);
            }
            g.DrawString(_text, f, b, new PointF(_left, _top), StrFormat);
            g.Flush();
            //bitmap.Save(this._resultImage, ImageFormat.Jpeg);
            //FileStream fileStream = new FileStream(this._resultImage, FileMode.Create, FileAccess.Write, FileShare.Write);
            //bitmap.Save(fileStream, ImageFormat.Jpeg);
            //fileStream.Close();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, ImageFormat.Jpeg);
                OutputBuffer = memoryStream.GetBuffer();
            }

            bitmap.Dispose();
            g.Dispose();

            return true;
        }

        /**/
        /// <summary>
        /// 生成水印效果图
        /// </summary>
        /// <returns></returns>
        public bool Create(Stream OutputStream)
        {
            Bitmap sourceBitmap = new Bitmap(this._width, this._height, PixelFormat.Format64bppArgb);

            Bitmap targetBitmap = null;

            Graphics g = Graphics.FromImage(sourceBitmap);

            //使用纯背景色
            if (this.imageBytes != null)
            {
                if (this.imageBytes.Length > 0)
                {
                    MemoryStream memoryStream = new MemoryStream(imageBytes);
                    sourceBitmap = new Bitmap(Image.FromStream(memoryStream));

                    if ((this._width > 0) && (this._height > 0))
                    {
                        targetBitmap = new Bitmap(sourceBitmap, this._width, this._height);
                        g = Graphics.FromImage(targetBitmap);
                    }
                    else
                    {
                        g = Graphics.FromImage(sourceBitmap);
                    }

                    memoryStream.Flush();
                    memoryStream.Close();
                }
            }
            else if (this._backgroundImage.Trim() != "")
            {
                sourceBitmap = new Bitmap(Image.FromFile(this._backgroundImage));
                g = Graphics.FromImage(sourceBitmap);
            }
            else
            {
                sourceBitmap = new Bitmap(this._width, this._height, PixelFormat.Format64bppArgb);
                g = Graphics.FromImage(sourceBitmap);
                g.Clear(this._bgColor);
            }

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;


            Font f = new Font(_fontFamily, _fontSize, _fontStyle);
            //Font f1 = new Font(_fontFamily, _fontSize+2, _fontStyle);
            SizeF size = g.MeasureString(_text, f);

            // 调整文字大小直到能适应图片尺寸

            if (targetBitmap != null)
            {
                while (_adaptable == true && size.Width > targetBitmap.Width)
                {
                    _fontSize--;
                    f = new Font(_fontFamily, _fontSize, _fontStyle);
                    size = g.MeasureString(_text, f);
                }
            }
            else
            {
                while (_adaptable == true && size.Width > sourceBitmap.Width)
                {
                    _fontSize--;
                    f = new Font(_fontFamily, _fontSize, _fontStyle);
                    size = g.MeasureString(_text, f);
                }
            }

            Brush b = new SolidBrush(Color.FromArgb(_alpha, _red, _green, _blue));
            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Near;

            if (this._top <= 0)
            {
                if (targetBitmap != null)
                {
                    this._top = (targetBitmap.Height / 3 - 1);
                }
                else
                {
                    this._top = (sourceBitmap.Height / 3 - 1);
                }
            }

            if (this._shadow)
            {
                SizeF textSize;
                //Graphics g;
                Brush myBackBrush = Brushes.Blue;
                Brush myForeBrush = Brushes.White;
                //Font myFont = new Font("Times New Roman", (float)this.nudFontSize.Value, FontStyle.Regular);
                float xLocation, yLocation;

                //g = picDemoArea.CreateGraphics();
                //g.Clear(Color.White);

                textSize = g.MeasureString(_text, f);

                xLocation = (1024 - textSize.Width) / 2;
                yLocation = (768 - textSize.Height) / 2;

                //g.DrawString(_text, f, myBackBrush,
                //  xLocation + 2,
                //   yLocation + 1);

                //g.DrawString(_text, f, myForeBrush, xLocation,
                //   yLocation);


                //Brush b2 = new SolidBrush(Color.FromArgb(150, 255, 255, 255));
                //g.DrawString(_text, f1, b2, new PointF(_left, _top), StrFormat);
                Brush b2 = new SolidBrush(Color.FromArgb(150, 255, 255, 255));

                g.DrawString(_text, f, b2, _left + 1, _top + 2);
            }
            g.DrawString(_text, f, b, new PointF(_left, _top), StrFormat);
            g.Flush();
            //bitmap.Save(this._resultImage, ImageFormat.Jpeg);

            if (targetBitmap != null)
            {
                targetBitmap.Save(OutputStream, ImageFormat.Jpeg);
                targetBitmap.Dispose();
            }
            else
            {
                sourceBitmap.Save(OutputStream, ImageFormat.Jpeg);
                sourceBitmap.Dispose();
            }

            g.Dispose();
            return true;
        }

        /**/
        /// <summary>
        /// 生成水印效果图
        /// </summary>
        /// <returns>/returns>
        public bool Create(Stream OutputStream, ImageFormat Format)
        {
            Bitmap sourceBitmap = new Bitmap(this._width, this._height, PixelFormat.Format64bppArgb);

            Bitmap targetBitmap = null;

            Graphics g = Graphics.FromImage(sourceBitmap);

            //使用纯背景色
            if (this.imageBytes != null)
            {
                if (this.imageBytes.Length > 0)
                {
                    MemoryStream memoryStream = new MemoryStream(imageBytes);
                    sourceBitmap = new Bitmap(Image.FromStream(memoryStream));

                    if ((this._width > 0) && (this._height > 0))
                    {
                        targetBitmap = new Bitmap(sourceBitmap, this._width, this._height);
                        g = Graphics.FromImage(targetBitmap);
                    }
                    else
                    {
                        g = Graphics.FromImage(sourceBitmap);
                    }

                    memoryStream.Flush();
                    memoryStream.Close();
                }
            }
            else if (this._backgroundImage.Trim() != "")
            {
                sourceBitmap = new Bitmap(Image.FromFile(this._backgroundImage));
                g = Graphics.FromImage(sourceBitmap);
            }
            else
            {
                sourceBitmap = new Bitmap(this._width, this._height, PixelFormat.Format64bppArgb);
                g = Graphics.FromImage(sourceBitmap);
                g.Clear(this._bgColor);
            }

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;


            Font f = new Font(_fontFamily, _fontSize, _fontStyle);
            //Font f1 = new Font(_fontFamily, _fontSize+2, _fontStyle);
            SizeF size = g.MeasureString(_text, f);

            // 调整文字大小直到能适应图片尺寸

            if (targetBitmap != null)
            {
                while (_adaptable == true && size.Width > targetBitmap.Width)
                {
                    _fontSize--;
                    f = new Font(_fontFamily, _fontSize, _fontStyle);
                    size = g.MeasureString(_text, f);
                }
            }
            else
            {
                while (_adaptable == true && size.Width > sourceBitmap.Width)
                {
                    _fontSize--;
                    f = new Font(_fontFamily, _fontSize, _fontStyle);
                    size = g.MeasureString(_text, f);
                }
            }

            Brush b = new SolidBrush(Color.FromArgb(_alpha, _red, _green, _blue));
            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Near;

            if (this._top <= 0)
            {
                if (targetBitmap != null)
                {
                    this._top = (targetBitmap.Height / 3 - 1);
                }
                else
                {
                    this._top = (sourceBitmap.Height / 3 - 1);
                }
            }

            if (this._shadow)
            {
                SizeF textSize;
                //Graphics g;
                Brush myBackBrush = Brushes.Blue;
                Brush myForeBrush = Brushes.White;
                //Font myFont = new Font("Times New Roman", (float)this.nudFontSize.Value, FontStyle.Regular);
                float xLocation, yLocation;

                //g = picDemoArea.CreateGraphics();
                //g.Clear(Color.White);

                textSize = g.MeasureString(_text, f);

                xLocation = (1280 - textSize.Width) / 2;
                yLocation = (1024 - textSize.Height) / 2;

                //g.DrawString(_text, f, myBackBrush,
                //  xLocation + 2,
                //   yLocation + 1);

                //g.DrawString(_text, f, myForeBrush, xLocation,
                //   yLocation);


                //Brush b2 = new SolidBrush(Color.FromArgb(150, 255, 255, 255));
                //g.DrawString(_text, f1, b2, new PointF(_left, _top), StrFormat);
                Brush b2 = new SolidBrush(Color.FromArgb(150, 255, 255, 255));

                g.DrawString(_text, f, b2, xLocation + 1, _top + 2);
            }
            SizeF textSize1 = g.MeasureString(_text, f);
            float x_Location = (1280 - textSize1.Width) / 2;
            g.DrawString(_text, f, b, new PointF(x_Location, _top), StrFormat);
            g.Flush();
            //bitmap.Save(this._resultImage, ImageFormat.Jpeg);

            if (targetBitmap != null)
            {
                targetBitmap.Save(OutputStream, Format);
                targetBitmap.Dispose();
            }
            else
            {
                sourceBitmap.Save(OutputStream, Format);
                sourceBitmap.Dispose();
            }

            g.Dispose();

            return true;
        }
    }
}
