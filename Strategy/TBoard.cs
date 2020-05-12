using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Strategy
{
    public partial class TBoard : UserControl
    {
        public float Zoom = 40;
        PointF _ScrollPos;

        TGame Game = new TGame();

        public Matrix Transform
        {
            get 
            {
                var transform = new Matrix();
                transform.Translate(Width / 2, Height / 2);
                transform.Scale(Zoom, Zoom);
                transform.Translate(-ScrollPos.X*Game.Map.Width, -ScrollPos.Y*Game.Map.Height);
                return transform;
            }
        }


        private PointF scrollPos;
        public PointF ScrollPos
        {
            get { return scrollPos; }
            set 
            { 
                scrollPos.X = Math.Min(Math.Max(value.X, 0), 1); 
                scrollPos.Y = Math.Min(Math.Max(value.Y, 0), 1);

                OnScroll(null);

                Invalidate();
            }
        }

        public TBoard()
        {
            InitializeComponent();
            MouseWheel += MakeZoom;
            DoubleBuffered = true;
            PlayTimer.Start();
        }

        private void MakeZoom(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                Zoom *= 0.9f;
            }
            else
            {
                Zoom *= 1.1f;
            }
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Transform = Transform;
            e.Graphics.DrawImage(Game.Map, Point.Empty);
        }

        float ScrollStep = 1;

        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            if (Cursor.Position.X < Margin.Left)
            {
                ScrollPos -= new SizeF(ScrollStep, 0);
            }
            else if (Cursor.Position.X > Width - Margin.Right)
            {
                ScrollPos += new SizeF(ScrollStep, 0);
            }
        }
    }
}
