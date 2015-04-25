using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AeroSuite.AnimationEngine
{
    public static class ValueFactories
    {
        #region Integer Types

        public static byte ByteFactory(byte startValue, byte targetValue, double progress)
        {
            return (byte)(startValue + (targetValue - startValue) * progress);
        }

        public static sbyte SByteFactory(sbyte startValue, sbyte targetValue, double progress)
        {
            return (sbyte)(startValue + (targetValue - startValue) * progress);
        }

        public static short ShortFactory(short startValue, short targetValue, double progress)
        {
            return (short)(startValue + (targetValue - startValue) * progress);
        }

        public static int IntegerFactory(int startValue, int targetValue, double progress)
        {
            return (int)(startValue + (targetValue - startValue) * progress);
        }

        public static long LongFactory(long startValue, long targetValue, double progress)
        {
            return (long)(startValue + (targetValue - startValue) * progress);
        }

        public static ushort UnsignedShortFactory(ushort startValue, ushort targetValue, double progress)
        {
            return (ushort)(startValue + (targetValue - startValue) * progress);
        }

        public static uint UnsignedIntegerFactory(uint startValue, uint targetValue, double progress)
        {
            return (uint)(startValue + (targetValue - startValue) * progress);
        }

        public static ulong UnsignedLongFactory(ulong startValue, ulong targetValue, double progress)
        {
            return (ulong)(startValue + (targetValue - startValue) * progress);
        }

        #endregion

        #region Floating Point Types

        public static float FloatFactory(float startValue, float targetValue, double progress)
        {
            return (float)(startValue + (targetValue - startValue) * progress);
        }

        public static double DoubleFactory(double startValue, double targetValue, double progress)
        {
            return (double)(startValue + (targetValue - startValue) * progress);
        }

        #endregion

        #region Drawing

        public static Point PointFactory(Point startValue, Point targetValue, double progress)
        {
            return new Point(IntegerFactory(startValue.X, targetValue.X, progress), IntegerFactory(startValue.Y, targetValue.Y, progress));
        }

        public static PointF PointFFactory(PointF startValue, PointF targetValue, double progress)
        {
            return new PointF(FloatFactory(startValue.X, targetValue.X, progress), FloatFactory(startValue.Y, targetValue.Y, progress));
        }

        public static Size SizeFactory(Size startValue, Size targetValue, double progress)
        {
            return new Size(IntegerFactory(startValue.Width, targetValue.Width, progress), IntegerFactory(startValue.Height, targetValue.Height, progress));
        }

        public static SizeF SizeFFactory(SizeF startValue, SizeF targetValue, double progress)
        {
            return new SizeF(FloatFactory(startValue.Width, targetValue.Width, progress), FloatFactory(startValue.Height, targetValue.Height, progress));
        }

        public static Rectangle RectangleFactory(Rectangle startValue, Rectangle targetValue, double progress)
        {
            return new Rectangle(IntegerFactory(startValue.X, targetValue.X, progress), IntegerFactory(startValue.Y, targetValue.Y, progress), IntegerFactory(startValue.Width, targetValue.Width, progress), IntegerFactory(startValue.Height, targetValue.Height, progress));
        }

        public static RectangleF RectangleFFactory(RectangleF startValue, RectangleF targetValue, double progress)
        {
            return new RectangleF(FloatFactory(startValue.X, targetValue.X, progress), FloatFactory(startValue.Y, targetValue.Y, progress), FloatFactory(startValue.Width, targetValue.Width, progress), FloatFactory(startValue.Height, targetValue.Height, progress));
        }

        public static Color ColorRgbFactory(Color startValue, Color targetValue, double progress)
        {
            return Color.FromArgb(IntegerFactory(startValue.A, targetValue.A, progress), IntegerFactory(startValue.R, targetValue.R, progress), IntegerFactory(startValue.G, targetValue.G, progress), IntegerFactory(startValue.B, targetValue.B, progress));
        }

        #endregion

        #region Other

        public static bool BooleanFactory(bool startValue, bool targetValue, double progress)
        {
            return (progress < .5) ? startValue : targetValue;
        }

        public static decimal DecimalFactory(decimal startValue, decimal targetValue, double progress)
        {
            return (decimal)(startValue + (targetValue - startValue) * (decimal)progress);
        }

        public static TimeSpan TimeSpanFactory(TimeSpan startValue, TimeSpan targetValue, double progress)
        {
            return startValue.Add(TimeSpan.FromMilliseconds(targetValue.Subtract(startValue).TotalMilliseconds * progress));
        }

        #endregion
    }
}
