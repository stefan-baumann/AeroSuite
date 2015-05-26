using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AeroSuite.AnimationEngine.CustomAnimationTest
{
    public class MenuCloseButton
        : Control
    {
        public MenuCloseButton()
            : base()
        {
            this.Size = new Size(40, 40);

            //3 Lines: {Start Point, End Point}
            //Every Point is between (-1|-1) and (1|1)
            this.NormalState = new PointF[]
            {
                new PointF(-1, .8f), new PointF(1, .8f), //Topmost line
                new PointF(-1, 0), new PointF(1, 0), //Middle line
                new PointF(-1, -.8f), new PointF(1, -.8f) //Lowest line
            };

            this.ExtendedState = new PointF[]
            {
                new PointF(1, -1), new PointF(-1, 1), //Bottom right to top left
                new PointF(1, 0), new PointF(-1, 0), //Collapsed
                new PointF(1, 1), new PointF(-1, -1) //Top right to bottom left
            };

            this.PointProvider = new ValueProvider<PointF[]>(this.NormalState, ValueFactoryCreator.CreateArrayFactory<PointF>(ValueFactories.PointFFactory), EasingMethods.ExponentialEaseOut);
            this.MiddleLineOpacityProvider = new ValueProvider<byte>(255, ValueFactories.ByteFactory, EasingMethods.ExponentialEaseInOut);

            this.DoubleBuffered = true;
            //For updates.
            new Timer() { Enabled = true, Interval = 1000/60 }.Tick += (s, e) =>
            {
                this.Invalidate();
            };
        }

        protected PointF[] NormalState;
        protected PointF[] ExtendedState;


        protected double durationFactor = .25;
        private bool extended = false;
        public bool Extended
        {
            get { return this.extended; }
            set
            {
                if (value != this.extended)
                {
                    this.PointProvider.StartTransition(value ? this.ExtendedState : this.NormalState, TimeSpan.FromSeconds(1 * durationFactor));
                    this.MiddleLineOpacityProvider.StartTransition(value ? (byte)0 : (byte)255, TimeSpan.FromSeconds(.25 * durationFactor));
                    this.extended = value;
                }
            }
        }

        protected ValueProvider<PointF[]> PointProvider { get; private set; }
        protected ValueProvider<byte> MiddleLineOpacityProvider { get; set; }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            PointF[] currentState = this.PointProvider.CurrentValue;
            using (Pen linePen = new Pen(this.ForeColor, Math.Min(this.Width, this.Height) / 40f * 4))
            {
                pevent.Graphics.DrawLine(linePen, this.Project(currentState[0]), this.Project(currentState[1]));
                pevent.Graphics.DrawLine(linePen, this.Project(currentState[4]), this.Project(currentState[5]));

                using (Pen middleLinePen = new Pen(Color.FromArgb(this.MiddleLineOpacityProvider.CurrentValue, this.ForeColor), linePen.Width))
                {
                    pevent.Graphics.DrawLine(middleLinePen, this.Project(currentState[2]), this.Project(currentState[3]));
                }
            }

            base.OnPaint(pevent);
        }

        protected PointF Project(PointF relativePoint, float spacing = .1f)
        {
            PointF center = new PointF(this.Width / 2f, this.Height / 2f);
            return new PointF(center.X + (center.X * relativePoint.X) - ((center.X * spacing) * relativePoint.X), center.Y + (center.Y * relativePoint.Y) - ((center.Y * spacing) * relativePoint.Y));
        }

        protected override void OnClick(EventArgs e)
        {
            //this.Extended = !this.Extended;
            base.OnClick(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Extended = !this.Extended ^ e.Button != MouseButtons.Left;
            base.OnMouseDown(e);
        }
    }
}
