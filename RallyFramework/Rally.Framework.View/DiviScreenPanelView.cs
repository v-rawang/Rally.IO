using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rally.Framework.View
{
    public partial class DiviScreenPanelView : Panel
    {
        public DiviScreenPanelView()
        {
            InitializeComponent();
        }

        public void Draw<T>(IDictionary<string, T> Screens) where T :Control
        {
            int width = this.Width;
            int heigth = this.Height;
            int screenCount = Screens.Count;

            //若宽度小于高度，则转换
            if (width < heigth)
            {
                int temp = width;
                width = heigth;
                heigth = temp;
            }

            //默认screen宽度为height
            int square = heigth;

            //i表示Panel被分成了几行
            for (int i = 1; i < 100; i++)
            {
                //每行高度
                square = Convert.ToInt16(Math.Floor(heigth * 1.0 / i));

                //若可以存放所有的摄像头，则返回
                if (Math.Floor(width * 1.0 / square) * i >= screenCount)
                {
                    break;
                }
            }

            int row = 0;
            int column = 0;

            foreach (string key in Screens.Keys)
            {
                Screens[key].Location = new Point(column * square, row * square);
                Screens[key].Width = square;
                Screens[key].Width = square;
                Screens[key].Size = new Size(square, square);

                this.Controls.Add(Screens[key]);

                column++;
                //若当前行放不下 下一个视频，则换行
                if (column * square > width - square)
                {
                    row++;
                    column = 0;
                }
            }

            //foreach (string key in Screens.Keys)
            //{
            //    this.Controls.Add(Screens[key]);
            //}
        }

        public void Draw<T>(IDictionary<string, T> Screens, Func<T, object> ExtensionFunction) where T : Control
        {
            //foreach (string key in Screens.Keys)
            //{
            //    if (ExtensionFunction != null)
            //    {
            //        ExtensionFunction(Screens[key]);
            //    }

            //    this.Controls.Add(Screens[key]);
            //}

            int width = this.Width;
            int heigth = this.Height;
            int screenCount = Screens.Count;

            //若宽度小于高度，则转换
            if (width < heigth)
            {
                int temp = width;
                width = heigth;
                heigth = temp;
            }

            //默认screen宽度为height
            int square = heigth;

            //i表示Panel被分成了几行
            for (int i = 1; i < 100; i++)
            {
                //每行高度
                square = Convert.ToInt16(Math.Floor(heigth * 1.0 / i));

                //若可以存放所有的摄像头，则返回
                if (Math.Floor(width * 1.0 / square) * i >= screenCount)
                {
                    break;
                }
            }

            int row = 0;
            int column = 0;

            foreach (string key in Screens.Keys)
            {
                Screens[key].Location = new Point(column * square, row * square);
                Screens[key].Width = square;
                Screens[key].Width = square;
                Screens[key].Size = new Size(square, square);

                if (ExtensionFunction != null)
                {
                    ExtensionFunction(Screens[key]);
                }

                this.Controls.Add(Screens[key]);

                column++;
                //若当前行放不下 下一个视频，则换行
                if (column * square > width - square)
                {
                    row++;
                    column = 0;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
