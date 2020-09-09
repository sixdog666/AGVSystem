using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AGVWpfSystem {
    class ArrowShape {
    }


    /// <summary>
    /// 矩形类
    /// </summary>
    public sealed class CRectangle : VtShape {
        #region Overrides
        protected override Geometry DefiningGeometry {
            get {
                // Create a StreamGeometry for describing the shape
                StreamGeometry geometry = new StreamGeometry();
                geometry.FillRule = FillRule.EvenOdd;

                using (StreamGeometryContext context = geometry.Open()) {
                    InternalDrawArrowGeometry(context);
                }

                // Freeze the geometry for performance benefits
                geometry.Freeze();

                return geometry;
            }
        }

        #endregion
        #region Privates
        private void InternalDrawArrowGeometry(StreamGeometryContext context) {
            //因为线宽的不同会导致区域显示不全，因此在计算时需考虑线宽
            Point ptStart = new Point(this.StrokeThickness / 2, this.StrokeThickness / 2);
            Point pt2 = new Point(this.Width - this.StrokeThickness / 2, this.StrokeThickness / 2);
            Point pt3 = new Point(this.Width - this.StrokeThickness / 2, this.Height - this.StrokeThickness / 2);
            Point pt4 = new Point(this.StrokeThickness / 2, this.Height - this.StrokeThickness / 2);
            Point ptEnd = new Point(this.StrokeThickness / 2, 0);
            context.BeginFigure(ptStart, true, false);
            context.LineTo(pt2, true, true);
            context.LineTo(pt3, true, true);
            context.LineTo(pt4, true, true);
            context.LineTo(ptEnd, true, true);
        }

        #endregion
    }
    /// <summary>
    /// 椭圆类
    /// </summary>
    public sealed class CEllipse : VtShape {
        #region Overrides
        protected override Geometry DefiningGeometry {
            get {
                // Create a StreamGeometry for describing the shape
                StreamGeometry geometry = new StreamGeometry();
                geometry.FillRule = FillRule.EvenOdd;

                using (StreamGeometryContext context = geometry.Open()) {
                    InternalDrawArrowGeometry(context);
                }

                // Freeze the geometry for performance benefits
                geometry.Freeze();

                return geometry;
            }
        }

        #endregion
        #region Privates
        private void InternalDrawArrowGeometry(StreamGeometryContext context) {
            //因为线宽的不同会导致区域显示不全，因此在计算时需考虑线宽
            double w = this.Width - this.StrokeThickness * 2;
            double h = this.Height - this.StrokeThickness * 2;
            if (w > 0 && h > 0) {
                Point pt = new Point(w / 2 + this.StrokeThickness, this.StrokeThickness);
                context.BeginFigure(pt, true, true);
                context.ArcTo(new Point(w / 2 + this.StrokeThickness, h + this.StrokeThickness), new Size(w / 2, h / 2), 0, true, SweepDirection.Counterclockwise, true, true);
                context.ArcTo(pt, new Size(w / 2, h / 2), 0, true, SweepDirection.Counterclockwise, true, true);
            }
        }

        #endregion
    }
    /// <summary>
    /// 直线类
    /// </summary>
    public sealed class CLine : VtShape {
        #region Dependency Properties

        public static readonly DependencyProperty X1Property = DependencyProperty.Register("X1", typeof(double), typeof(CLine), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty Y1Property = DependencyProperty.Register("Y1", typeof(double), typeof(CLine), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty X2Property = DependencyProperty.Register("X2", typeof(double), typeof(CLine), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty Y2Property = DependencyProperty.Register("Y2", typeof(double), typeof(CLine), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        #endregion

        #region CLR Properties
        [TypeConverter(typeof(LengthConverter))]
        public double X1 {
            get { return (double)base.GetValue(X1Property); }
            set { base.SetValue(X1Property, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double Y1 {
            get { return (double)base.GetValue(Y1Property); }
            set { base.SetValue(Y1Property, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double X2 {
            get { return (double)base.GetValue(X2Property); }
            set { base.SetValue(X2Property, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double Y2 {
            get { return (double)base.GetValue(Y2Property); }
            set { base.SetValue(Y2Property, value); }
        }
        #endregion
        #region Overrides
        protected override Geometry DefiningGeometry {
            get {
                // Create a StreamGeometry for describing the shape
                StreamGeometry geometry = new StreamGeometry();
                geometry.FillRule = FillRule.EvenOdd;

                using (StreamGeometryContext context = geometry.Open()) {
                    InternalDrawArrowGeometry(context);
                }

                // Freeze the geometry for performance benefits
                geometry.Freeze();

                return geometry;
            }
        }
        #endregion
        #region Privates
        public CLine(Canvas linePanel) {
            _panel = linePanel;
            _panel.Children.Add(_arrow.lineCap_start);
            _panel.Children.Add(_arrow.lineCap_end);
        }
        private void InternalDrawArrowGeometry(StreamGeometryContext context) {

            double theta = Math.Atan2(Y1 - Y2, X1 - X2);
            double sint = Math.Sin(theta);
            double cost = Math.Cos(theta);

            Point pt1 = new Point(X1, this.Y1);
            Point pt2 = new Point(X2, this.Y2);

            Point pt3 = new Point(
                X2 + (_arrow.HeadWidth * cost - _arrow.HeadHeight * sint),
                Y2 + (_arrow.HeadWidth * sint + _arrow.HeadHeight * cost));

            Point pt4 = new Point(
                X2 + (_arrow.HeadWidth * cost + _arrow.HeadHeight * sint),
                Y2 - (_arrow.HeadHeight * cost - _arrow.HeadWidth * sint));

            Point pt5 = new Point(
              X1 - (_arrow.TailWidth * cost - _arrow.TailHeight * sint),
              Y1 - (_arrow.TailWidth * sint + _arrow.TailHeight * cost));

            Point pt6 = new Point(
                X1 - (_arrow.TailWidth * cost + _arrow.TailHeight * sint),
                Y1 + (_arrow.TailHeight * cost - _arrow.TailWidth * sint));


            //this.StrokeStartLineCap = PenLineCap.Triangle;
            //this.StrokeEndLineCap = PenLineCap.Triangle;

            //lineCap_start = new Polygon();
            PointCollection pts_start = new PointCollection();
            pts_start.Add(pt5);
            pts_start.Add(pt1);
            pts_start.Add(pt6);
            PointCollection pts_end = new PointCollection();
            pts_end.Add(pt3);
            pts_end.Add(pt2);
            pts_end.Add(pt4);
            SetLineCap(pt1, pt2, pt1, pt2);//设置线帽

            context.BeginFigure(pt1, false, false);
            //context.LineTo(pt5, true, true);
            //context.LineTo(pt1, true, true);
            //context.LineTo(pt6, true, true);
            //context.LineTo(pt1, true, true);
            //绘制第一个点的线帽
            context.LineTo(pt2, true, true);//绘制直线
            //context.LineTo(pt3, true, true);
            //context.LineTo(pt2, true, true);
            //context.LineTo(pt4, true, true);
            //绘制第二个点的线帽
        }
        #endregion
    }
    /// <summary>
    /// 右直角三角形类
    /// </summary>
    public sealed class CRightTriangle : VtShape {
        #region Overrides
        protected override Geometry DefiningGeometry {
            get {
                // Create a StreamGeometry for describing the shape
                StreamGeometry geometry = new StreamGeometry();
                geometry.FillRule = FillRule.EvenOdd;

                using (StreamGeometryContext context = geometry.Open()) {
                    InternalDrawArrowGeometry(context);
                }

                // Freeze the geometry for performance benefits
                geometry.Freeze();

                return geometry;
            }
        }

        #endregion
        #region Privates

        private void InternalDrawArrowGeometry(StreamGeometryContext context) {
            double angle = Math.Atan(this.Height / (this.Width / 2));
            double offsetTop = this.StrokeThickness / Math.Cos(angle);
            double offsetLeft = this.StrokeThickness / Math.Tan(angle) + this.StrokeThickness / Math.Sign(angle);

            Point pt1 = new Point(this.Width - offsetLeft, 0);
            Point pt2 = new Point(0, this.Height - this.StrokeThickness);
            Point pt3 = new Point(this.Width - offsetLeft, this.Height - this.StrokeThickness);
            context.BeginFigure(pt1, true, true);
            context.LineTo(pt2, true, true);
            context.LineTo(pt3, true, true);
            context.LineTo(pt1, true, true);
        }

        #endregion
    }
    /// <summary>
    /// 左直角三角形类
    /// </summary>
    public sealed class CLeftTriangle : VtShape {
        #region Overrides
        protected override Geometry DefiningGeometry {
            get {
                // Create a StreamGeometry for describing the shape
                StreamGeometry geometry = new StreamGeometry();
                geometry.FillRule = FillRule.EvenOdd;

                using (StreamGeometryContext context = geometry.Open()) {
                    InternalDrawArrowGeometry(context);
                }

                // Freeze the geometry for performance benefits
                geometry.Freeze();

                return geometry;
            }
        }

        #endregion
        #region Privates

        private void InternalDrawArrowGeometry(StreamGeometryContext context) {
            double angle = Math.Atan(this.Height / (this.Width / 2));
            double offsetTop = this.StrokeThickness / Math.Cos(angle);
            double offsetLeft = this.StrokeThickness / Math.Tan(angle) + this.StrokeThickness / Math.Sign(angle);

            Point pt1 = new Point(0, 0);
            Point pt2 = new Point(0, this.Height - this.StrokeThickness);
            Point pt3 = new Point(this.Width - offsetLeft, this.Height - this.StrokeThickness);
            context.BeginFigure(pt1, true, true);
            context.LineTo(pt2, true, true);
            context.LineTo(pt3, true, true);
            context.LineTo(pt1, true, true);
        }

        #endregion
    }
    /// <summary>
    /// 等腰三角形类
    /// </summary>
    public sealed class CTriangle : VtShape {
        #region Overrides
        protected override Geometry DefiningGeometry {
            get {
                // Create a StreamGeometry for describing the shape
                StreamGeometry geometry = new StreamGeometry();
                geometry.FillRule = FillRule.EvenOdd;

                using (StreamGeometryContext context = geometry.Open()) {
                    InternalDrawArrowGeometry(context);
                }

                // Freeze the geometry for performance benefits
                geometry.Freeze();

                return geometry;
            }
        }

        #endregion
        #region Privates

        private void InternalDrawArrowGeometry(StreamGeometryContext context) {
            double angle = Math.Atan(this.Height / (this.Width / 2));
            double offsetTop = this.StrokeThickness / Math.Cos(angle);
            double offsetLeft = this.StrokeThickness / Math.Tan(angle) + this.StrokeThickness / Math.Sign(angle);

            Point pt1 = new Point(this.Width / 2, this.StrokeThickness);
            Point pt2 = new Point(offsetLeft, this.Height - this.StrokeThickness);
            Point pt3 = new Point(this.Width - offsetLeft, this.Height - this.StrokeThickness);
            context.BeginFigure(pt1, true, true);
            context.LineTo(pt2, true, true);
            context.LineTo(pt3, true, true);
            context.LineTo(pt1, true, true);
        }

        #endregion
    }

    /// <summary>
    /// 点
    /// </summary>
    public sealed class CPoint : VtShape {
        #region Overrides
        protected override Geometry DefiningGeometry {
            get {
                // Create a StreamGeometry for describing the shape
                StreamGeometry geometry = new StreamGeometry();
                geometry.FillRule = FillRule.EvenOdd;

                using (StreamGeometryContext context = geometry.Open()) {
                    InternalDrawArrowGeometry(context);
                }

                // Freeze the geometry for performance benefits
                geometry.Freeze();

                return geometry;
            }
        }

        #endregion
        #region Privates

        private void InternalDrawArrowGeometry(StreamGeometryContext context) {
            double radiu=1.5;
            this.Height = radiu*2+2;
            this.Width = radiu*2+2;
            Point pt1 = new Point(0+1, radiu+1);
            Point pt2 = new Point(2*radiu+1, radiu+1);
            Size size = new Size(radiu, radiu);
            context.BeginFigure(pt1, true, true);

            context.ArcTo(pt2,size,180,false,SweepDirection.Clockwise,true,true);
            context.ArcTo(pt1, size, 180,false, SweepDirection.Clockwise, true, true);
        }

        #endregion
    }
    /// <summary>
    /// 带箭头的线
    /// </summary>
    public sealed class CArrowLine : VtShape {
        #region Overrides
        protected override Geometry DefiningGeometry {
            get {
                // Create a StreamGeometry for describing the shape
                StreamGeometry geometry = new StreamGeometry();
                geometry.FillRule = FillRule.Nonzero;

                using (StreamGeometryContext context = geometry.Open()) {
                    InternalDrawArrowGeometry(context);
                }

                // Freeze the geometry for performance benefits
                geometry.Freeze();

                return geometry;
            }
        }

        #endregion
        #region Privates

        private void InternalDrawArrowGeometry(StreamGeometryContext context) {
            this. Height = 8;
            Point pt1 = new Point(0,4);
            Point pt2 = new Point(Width-1, 4);
            Point pt3 = new Point(Width - 5, 0);
            Point pt4 = new Point(Width - 5, 8);
            Point pt5= new Point(Width-1 , 4);
           
            context.BeginFigure(pt1, true, false);
            context.LineTo(pt2, true, true);
            context.LineTo(pt3, true, true);
            context.LineTo(pt4,false,false);
            context.LineTo(pt5, true, true);
            
        }

        #endregion
    }
    /// <summary>
    /// 菱形类
    /// </summary>
    public sealed class CDiamond : VtShape {
        #region Overrides
        protected override Geometry DefiningGeometry {
            get {
                // Create a StreamGeometry for describing the shape
                StreamGeometry geometry = new StreamGeometry();
                geometry.FillRule = FillRule.EvenOdd;

                using (StreamGeometryContext context = geometry.Open()) {
                    InternalDrawArrowGeometry(context);
                }

                // Freeze the geometry for performance benefits
                geometry.Freeze();

                return geometry;
            }
        }
        #endregion
        #region Privates
        private void InternalDrawArrowGeometry(StreamGeometryContext context) {
            double angle = Math.Atan(this.Height / (this.Width / 2));
            Point pt1 = new Point(this.Width / 2, this.StrokeThickness);
            Point pt2 = new Point(this.StrokeThickness, this.Height / 2);
            Point pt3 = new Point(this.Width / 2, this.Height - this.StrokeThickness);
            Point pt4 = new Point(this.Width - this.StrokeThickness, this.Height / 2);
            context.BeginFigure(pt1, true, true);
            context.LineTo(pt1, true, true);
            context.LineTo(pt2, true, true);
            context.LineTo(pt3, true, true);
            context.LineTo(pt4, true, true);
        }

        #endregion
    }
    /// <summary>
    /// 菱形类
    /// </summary>
    public sealed class CFiveStar : VtShape {
        #region Overrides
        protected override Geometry DefiningGeometry {
            get {
                // Create a StreamGeometry for describing the shape
                StreamGeometry geometry = new StreamGeometry();
                geometry.FillRule = FillRule.EvenOdd;

                using (StreamGeometryContext context = geometry.Open()) {
                    InternalDrawArrowGeometry(context);
                }

                // Freeze the geometry for performance benefits
                geometry.Freeze();

                return geometry;
            }
        }
        #endregion
        #region Privates
        private Point Intersection(Point pt1, Point pt2, Point pt3, Point pt4) {

            double A1 = pt1.Y - pt2.Y;
            double B1 = pt2.X - pt1.X;
            double C1 = pt1.Y * (pt2.X - pt1.X) - pt1.X * (pt2.Y - pt1.Y);
            double A2 = pt3.Y - pt4.Y, B2 = pt4.X - pt3.X, C2 = pt3.Y * (pt4.X - pt3.X) - pt3.X * (pt4.Y - pt3.Y);
            //A1 * x + B1 * y = C1
            //A2 * x + B2 * y = C2
            double D = A1 * B2 - B1 * A2, Dx = C1 * B2 - B1 * C2, Dy = A1 * C2 - A2 * C1;
            double x = Dx / (double)D, y = Dy / (double)D;
            return new Point((int)x, (int)y);
        }
        private Point RotateTheta(Point pt, Point center, double theta) {
            double x = center.X + (pt.X - center.X) * Math.Cos((theta * Math.PI / 180)) - (pt.Y - center.Y) * Math.Sin((theta * Math.PI / 180));
            double y = center.Y + (pt.X - center.X) * Math.Sin((theta * Math.PI / 180)) + (pt.Y - center.Y) * Math.Cos((theta * Math.PI / 180));
            return new Point(x, y);
        }
        private void InternalDrawArrowGeometry(StreamGeometryContext context) {
            Point ptCenter = new Point(this.Width / 2, this.Height / 2);
            double radius = this.Width / 2;
            Point[] pts = new Point[10];
            pts[0] = new Point(ptCenter.X, (int)(ptCenter.Y - radius));
            for (int i = 1; i < 5; i++) {
                pts[2 * i] = RotateTheta(pts[2 * (i - 1)], ptCenter, 72.0);
                pts[2 * i].X = (int)(ptCenter.X + (pts[2 * i].X - ptCenter.X));
                pts[2 * i].Y = (int)(ptCenter.Y + (pts[2 * i].Y - ptCenter.Y));
            }
            //通过直线求交点的方式来取得1,3,5,7,9点坐标
            pts[1] = Intersection(pts[0], pts[4], pts[2], pts[8]);
            pts[3] = Intersection(pts[0], pts[4], pts[2], pts[6]);
            pts[5] = Intersection(pts[2], pts[6], pts[4], pts[8]);
            pts[7] = Intersection(pts[0], pts[6], pts[4], pts[8]);
            pts[9] = Intersection(pts[0], pts[6], pts[2], pts[8]);

            context.BeginFigure(pts[0], true, true);
            context.LineTo(pts[1], true, true);
            context.LineTo(pts[2], true, true);
            context.LineTo(pts[3], true, true);
            context.LineTo(pts[4], true, true);
            context.LineTo(pts[5], true, true);
            context.LineTo(pts[6], true, true);
            context.LineTo(pts[7], true, true);
            context.LineTo(pts[8], true, true);
            context.LineTo(pts[9], true, true);
        }

        #endregion
    }
    /// <summary>
    /// 三次贝塞尔曲线类
    /// </summary>
    public sealed class CBezierCurve : VtShape {
        #region Dependency Properties
        //public static readonly DependencyProperty X1Property = DependencyProperty.Register("X1", typeof(double), typeof(CLine), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        //public static readonly DependencyProperty Y1Property = DependencyProperty.Register("Y1", typeof(double), typeof(CLine), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        //public static readonly DependencyProperty X2Property = DependencyProperty.Register("X2", typeof(double), typeof(CLine), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        //public static readonly DependencyProperty Y2Property = DependencyProperty.Register("Y2", typeof(double), typeof(CLine), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty BezierPointsProperty = DependencyProperty.Register("BezierPoints", typeof(List<Point>), typeof(CBezierCurve), new FrameworkPropertyMetadata(new List<Point>(), FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        //public static readonly DependencyProperty BezierTmpPointProperty = DependencyProperty.Register("BezierTmpPoint", typeof(Point), typeof(CBezierCurve), new FrameworkPropertyMetadata(new Point(0,0), FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        #endregion

        #region CLR Properties
        [TypeConverter(typeof(double))]
        public List<Point> BezierPoints {
            get { return (List<Point>)base.GetValue(BezierPointsProperty); }
            set { BezierPoints.Clear(); base.SetValue(BezierPointsProperty, value); }
        }
        //[TypeConverter(typeof(double))]
        //public Point BezierTmpPoint
        //{
        //    get { return (Point)base.GetValue(BezierTmpPointProperty); }
        //    set { base.SetValue(BezierTmpPointProperty, value); }
        //}
        #endregion
        #region Overrides

        protected override Geometry DefiningGeometry {
            get {
                // Create a StreamGeometry for describing the shape
                StreamGeometry geometry = new StreamGeometry();
                geometry.FillRule = FillRule.EvenOdd;

                using (StreamGeometryContext context = geometry.Open()) {
                    InternalDrawArrowGeometry(context);
                }

                // Freeze the geometry for performance benefits
                geometry.Freeze();

                return geometry;
            }
        }
        #endregion
        #region Privates

        private List<Point> _lstCtrPoints = new List<Point>();
        public bool _bDrawing = false;//是否正在绘制
        public Point BezierTmpPoint = new Point(0, 0);
        private void InternalDrawArrowGeometry(StreamGeometryContext context) {
            _lstCtrPoints.Clear();
            List<Point> lstPoints = new List<Point>();
            List<Point> lstCtrPoints = new List<Point>();
            foreach (Point pt in BezierPoints) {
                lstPoints.Add(new Point(pt.X, pt.Y));
            }
            if (_bDrawing == true && (BezierTmpPoint.X > 0 || BezierTmpPoint.Y > 0)) {
                lstPoints.Add(new Point(BezierTmpPoint.X, BezierTmpPoint.Y));

                for (int i = 0; i < lstPoints.Count; i++) {
                    lstCtrPoints.AddRange(Control1(lstPoints, i));
                    if (i < lstPoints.Count - 1) {
                        _lstCtrPoints.AddRange(Control1(lstPoints, i));
                    }
                }
            }
            else if (_bDrawing == false) {
                for (int i = 0; i < lstPoints.Count; i++) {
                    lstCtrPoints.AddRange(Control1(lstPoints, i));
                    _lstCtrPoints.AddRange(Control1(lstPoints, i));
                }
            }
            if (lstPoints != null && lstPoints.Count > 1) {
                Point ptStart = lstPoints[0];
                Point ptEnd = lstPoints[lstPoints.Count - 1];
                //先绘制开始线帽
                SetLineCap(lstPoints[lstPoints.Count - 2], ptEnd, ptStart, lstPoints[1]);
                context.BeginFigure(ptStart, true, false);
                //再绘制曲线
                for (int i = 1; i < lstPoints.Count; i++) {
                    context.BezierTo(lstCtrPoints[i * 2 - 1], lstCtrPoints[i * 2], lstPoints[i], true, true);
                }
            }

        }
        private List<Point> Control1(List<Point> list, int n) {
            List<Point> point = new List<Point>();
            point.Add(new Point());
            point.Add(new Point());
            if (n == 0) {
                point[0] = list[0];
            }
            else {
                point[0] = Average(list[n - 1], list[n]);
            }
            if (n == list.Count - 1) {
                point[1] = list[list.Count - 1];
            }
            else {
                point[1] = Average(list[n], list[n + 1]);
            }
            Point ave = Average(point[0], point[1]);
            Point sh = Sub(list[n], ave);
            point[0] = Mul(Add(point[0], sh), list[n], 0.6);
            point[1] = Mul(Add(point[1], sh), list[n], 0.6);

            return point;
        }
        private Point Average(Point x, Point y) {
            return new Point((x.X + y.X) / 2, (x.Y + y.Y) / 2);
        }
        private Point Add(Point x, Point y) {
            return new Point(x.X + y.X, x.Y + y.Y);
        }
        private Point Sub(Point x, Point y) {
            return new Point(x.X - y.X, x.Y - y.Y);
        }
        private Point Mul(Point x, Point y, double d) {
            Point temp = Sub(x, y);
            temp = new Point(temp.X * d, temp.Y * d);
            temp = Add(y, temp);
            return temp;
        }
        #endregion
        #region Public
        public CBezierCurve(Canvas linePanel) {
            _panel = linePanel;
            _panel.Children.Add(_arrow.lineCap_start);
            _panel.Children.Add(_arrow.lineCap_end);
        }
        public void GetVectorRect(ref Point TopLeft, ref Point BottomRight) {
            if (BezierPoints.Count > 0) {
                TopLeft.X = BezierPoints[0].X;
                TopLeft.Y = BezierPoints[0].Y;
                BottomRight.X = BezierPoints[0].X;
                BottomRight.Y = BezierPoints[0].Y;
                #region 曲线顶点
                foreach (Point pt in BezierPoints) {
                    if (pt.X < TopLeft.X) {
                        TopLeft.X = pt.X;
                    }
                    if (pt.Y < TopLeft.Y) {
                        TopLeft.Y = pt.Y;
                    }
                    if (pt.X > BottomRight.X) {
                        BottomRight.X = pt.X;
                    }
                    if (pt.Y > BottomRight.Y) {
                        BottomRight.Y = pt.Y;
                    }
                }
                #endregion
                #region 曲线控制点
                foreach (Point pt in _lstCtrPoints) {
                    if (pt.X < TopLeft.X) {
                        TopLeft.X = pt.X;
                    }
                    if (pt.Y < TopLeft.Y) {
                        TopLeft.Y = pt.Y;
                    }
                    if (pt.X > BottomRight.X) {
                        BottomRight.X = pt.X;
                    }
                    if (pt.Y > BottomRight.Y) {
                        BottomRight.Y = pt.Y;
                    }
                }
                #endregion
            }
        }
        public void Clear() {
            if (BezierPoints.Count > 0) {
                BezierPoints.Clear();
            }
            if (_lstCtrPoints.Count > 0) {
                _lstCtrPoints.Clear();
            }
        }
        #endregion
    }

    /// <summary>
    /// 线帽样式
    /// </summary>
    public enum EnumArrowStyle {
        NULL = 0,
        STYLE1 = 1,
        STYLE2 = 2,
        STYLE3 = 3,
        STYLE4 = 4,
        STYLE5 = 5,
        STYLE6 = 6,
        STYLE7 = 7,
        STYLE8 = 8,
        STYLE9 = 9,
        STYLE10 = 10,
        STYLE11 = 11,
        STYLE12 = 12
    }
    /// <summary>
    /// 线型样式
    /// </summary>
    public enum EnumDottedStyle {
        NULL = 0,
        STYLE1 = 1,
        STYLE2 = 2,
        STYLE3 = 3,
        STYLE4 = 4,
        STYLE5 = 5
    }
    /// <summary>
    /// 形状类，为形状设置线型、线帽
    /// </summary>
    public class VtShape : Shape {
        #region Arrow Style
        public struct Arrow {
            public double HeadWidth;
            public double HeadHeight;
            public double TailWidth;
            public double TailHeight;
            public Polygon lineCap_start;
            public Polygon lineCap_end;
            public Arrow(double _headWidth, double _headHeight, double _tailWidth, double _tailHeight, Polygon lineCapstart, Polygon lineCapend) {
                HeadWidth = _headWidth;
                HeadHeight = _headHeight;
                TailWidth = _tailWidth;
                TailHeight = _tailHeight;
                lineCap_start = lineCapstart;
                lineCap_end = lineCapend;
            }
        }
        protected Arrow _arrow = new Arrow(0, 0, 0, 0, new Polygon(), new Polygon());
        public static readonly DependencyProperty ArrowStyleProperty = DependencyProperty.Register("ArrowStyle", typeof(EnumArrowStyle), typeof(VtShape), new FrameworkPropertyMetadata(EnumArrowStyle.NULL, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        protected Canvas _panel = null;
        [TypeConverter(typeof(EnumArrowStyle))]
        public EnumArrowStyle ArrowStyle {
            get { return (EnumArrowStyle)base.GetValue(ArrowStyleProperty); }
            set {
                base.SetValue(ArrowStyleProperty, value);
                #region 设置箭头大小
                switch (ArrowStyle) {
                    case EnumArrowStyle.NULL:
                        _arrow.HeadWidth = 0; _arrow.HeadHeight = 0; _arrow.TailWidth = 0; _arrow.TailHeight = 0;
                        break;
                    case EnumArrowStyle.STYLE1:
                        _arrow.HeadWidth = 30; _arrow.HeadHeight = 20; _arrow.TailWidth = 0; _arrow.TailHeight = 0;
                        break;
                    case EnumArrowStyle.STYLE2:
                        _arrow.HeadWidth = 30; _arrow.HeadHeight = 20; _arrow.TailWidth = 0; _arrow.TailHeight = 0;
                        break;
                    case EnumArrowStyle.STYLE3:
                        _arrow.HeadWidth = 30; _arrow.HeadHeight = 20; _arrow.TailWidth = 0; _arrow.TailHeight = 0;
                        break;
                    case EnumArrowStyle.STYLE4:
                        _arrow.HeadWidth = 30; _arrow.HeadHeight = 20; _arrow.TailWidth = 0; _arrow.TailHeight = 0;
                        break;
                    case EnumArrowStyle.STYLE5:
                        _arrow.HeadWidth = 0; _arrow.HeadHeight = 0; _arrow.TailWidth = 30; _arrow.TailHeight = 20;
                        break;
                    case EnumArrowStyle.STYLE6:
                        _arrow.HeadWidth = 0; _arrow.HeadHeight = 0; _arrow.TailWidth = 30; _arrow.TailHeight = 20;
                        break;
                    case EnumArrowStyle.STYLE7:
                        _arrow.HeadWidth = 0; _arrow.HeadHeight = 0; _arrow.TailWidth = 30; _arrow.TailHeight = 20;
                        break;
                    case EnumArrowStyle.STYLE8:
                        _arrow.HeadWidth = 0; _arrow.HeadHeight = 0; _arrow.TailWidth = 30; _arrow.TailHeight = 20;
                        break;
                    case EnumArrowStyle.STYLE9:
                        _arrow.HeadWidth = 30; _arrow.HeadHeight = 20; _arrow.TailWidth = 30; _arrow.TailHeight = 20;
                        break;
                    case EnumArrowStyle.STYLE10:
                        _arrow.HeadWidth = 30; _arrow.HeadHeight = 20; _arrow.TailWidth = 30; _arrow.TailHeight = 20;
                        break;
                    case EnumArrowStyle.STYLE11:
                        _arrow.HeadWidth = 30; _arrow.HeadHeight = 20; _arrow.TailWidth = 30; _arrow.TailHeight = 20;
                        break;
                    case EnumArrowStyle.STYLE12:
                        _arrow.HeadWidth = 30; _arrow.HeadHeight = 20; _arrow.TailWidth = 30; _arrow.TailHeight = 20;
                        break;
                }
                #endregion
            }
        }
        protected enum LineCapType {
            LineCap_Start = 0,
            LineCap_End = 1
        }
        protected void SetLineCap(Point pt1_start, Point pt1_end, Point pt2_start, Point pt2_end) {
            _arrow.lineCap_start.Points = new PointCollection();
            _arrow.lineCap_end.Points = new PointCollection();
            _arrow.lineCap_start.Fill = ((Polygon)_arrow.lineCap_start).Stroke = this.Stroke; // Brushes.Yellow;
            _arrow.lineCap_end.Fill = ((Polygon)_arrow.lineCap_end).Stroke = this.Stroke;//= Brushes.Yellow;
            _arrow.lineCap_start.StrokeThickness = _arrow.lineCap_end.StrokeThickness = this.StrokeThickness;
            switch (ArrowStyle) {
                case EnumArrowStyle.NULL:
                    break;
                case EnumArrowStyle.STYLE1:
                    SetTriangle2Cap(LineCapType.LineCap_End, pt1_start, pt1_end, _arrow.lineCap_start, _arrow.lineCap_end);
                    break;
                case EnumArrowStyle.STYLE2:
                    SetTriangleCap(LineCapType.LineCap_End, pt1_start, pt1_end, _arrow.lineCap_start, _arrow.lineCap_end);
                    break;
                case EnumArrowStyle.STYLE3:
                    SetRectCap(LineCapType.LineCap_End, pt1_start, pt1_end, _arrow.lineCap_start, _arrow.lineCap_end);
                    break;
                case EnumArrowStyle.STYLE4:
                    SetTriangle3Cap(LineCapType.LineCap_End, pt1_start, pt1_end, _arrow.lineCap_start, _arrow.lineCap_end);
                    break;
                case EnumArrowStyle.STYLE5:
                    SetTriangle2Cap(LineCapType.LineCap_Start, pt2_start, pt2_end, _arrow.lineCap_start, _arrow.lineCap_end);
                    break;
                case EnumArrowStyle.STYLE6:
                    SetTriangleCap(LineCapType.LineCap_Start, pt2_start, pt2_end, _arrow.lineCap_start, _arrow.lineCap_end);
                    break;
                case EnumArrowStyle.STYLE7:
                    SetRectCap(LineCapType.LineCap_Start, pt2_start, pt2_end, _arrow.lineCap_start, _arrow.lineCap_end);
                    break;
                case EnumArrowStyle.STYLE8:
                    SetTriangle3Cap(LineCapType.LineCap_Start, pt2_start, pt2_end, _arrow.lineCap_start, _arrow.lineCap_end);
                    break;
                case EnumArrowStyle.STYLE9:
                    SetTriangle2Cap(LineCapType.LineCap_End, pt1_start, pt1_end, _arrow.lineCap_start, _arrow.lineCap_end);
                    SetTriangle2Cap(LineCapType.LineCap_Start, pt2_start, pt2_end, _arrow.lineCap_start, _arrow.lineCap_end);
                    break;
                case EnumArrowStyle.STYLE10:
                    SetTriangleCap(LineCapType.LineCap_End, pt1_start, pt1_end, _arrow.lineCap_start, _arrow.lineCap_end);
                    SetTriangleCap(LineCapType.LineCap_Start, pt2_start, pt2_end, _arrow.lineCap_start, _arrow.lineCap_end);
                    break;
                case EnumArrowStyle.STYLE11:
                    SetRectCap(LineCapType.LineCap_End, pt1_start, pt1_end, _arrow.lineCap_start, _arrow.lineCap_end);
                    SetRectCap(LineCapType.LineCap_Start, pt2_start, pt2_end, _arrow.lineCap_start, _arrow.lineCap_end);
                    break;
                case EnumArrowStyle.STYLE12:
                    SetTriangle3Cap(LineCapType.LineCap_End, pt1_start, pt1_end, _arrow.lineCap_start, _arrow.lineCap_end);
                    SetTriangle3Cap(LineCapType.LineCap_Start, pt2_start, pt2_end, _arrow.lineCap_start, _arrow.lineCap_end);
                    break;
            }
        }

        /// <summary>
        /// 封闭的三角形线帽
        /// </summary>
        /// <param name="pt_start"></param>
        /// <param name="pt_end"></param>
        /// <param name="cap_start"></param>
        /// <param name="cap_end"></param>
        private void SetTriangleCap(LineCapType lineCapType, Point pt_start, Point pt_end, Polygon cap_start, Polygon cap_end) {

            double theta = Math.Atan2(pt_start.Y - pt_end.Y, pt_start.X - pt_end.X);
            double sint = Math.Sin(theta);
            double cost = Math.Cos(theta);
            if (lineCapType == LineCapType.LineCap_Start) {
                cap_start.Points.Add(
                    new Point(
                   pt_start.X - (_arrow.TailWidth * cost - _arrow.TailHeight * sint),
                   pt_start.Y - (_arrow.TailWidth * sint + _arrow.TailHeight * cost))
                   );
                cap_start.Points.Add(new Point(pt_start.X + 20 * cost, pt_start.Y + 20 * sint));
                cap_start.Points.Add(new Point(
                     pt_start.X - (_arrow.TailWidth * cost + _arrow.TailHeight * sint),
                     pt_start.Y + (_arrow.TailHeight * cost - _arrow.TailWidth * sint))
                     );
            }
            if (lineCapType == LineCapType.LineCap_End) {
                cap_end.Points.Add(
                            new Point(
                                 pt_end.X + (_arrow.HeadWidth * cost - _arrow.HeadHeight * sint),
                                 pt_end.Y + (_arrow.HeadWidth * sint + _arrow.HeadHeight * cost))
                         );
                cap_end.Points.Add(new Point(pt_end.X - 20 * cost, pt_end.Y - 20 * sint));
                cap_end.Points.Add(
                 new Point(
                     pt_end.X + (_arrow.HeadWidth * cost + _arrow.HeadHeight * sint),
                      pt_end.Y - (_arrow.HeadHeight * cost - _arrow.HeadWidth * sint))
               );
            }
        }
        /// <summary>
        /// 非封闭的三角形线帽
        /// </summary>
        /// <param name="pt_start">端点1</param>
        /// <param name="pt_end">端点2</param>
        /// <param name="cap_start">端点1形状</param>
        /// <param name="cap_end">端点2形状</param>
        private void SetTriangle2Cap(LineCapType lineCapType, Point pt_start, Point pt_end, Polygon cap_start, Polygon cap_end) {
            double theta = Math.Atan2(pt_start.Y - pt_end.Y, pt_start.X - pt_end.X);
            double sint = Math.Sin(theta);
            double cost = Math.Cos(theta);
            if (lineCapType == LineCapType.LineCap_Start) {
                cap_start.Points.Add(
                    new Point(
                   pt_start.X - (_arrow.TailWidth * cost - _arrow.TailHeight * sint),
                   pt_start.Y - (_arrow.TailWidth * sint + _arrow.TailHeight * cost))
                   );
                cap_start.Points.Add(new Point(pt_start.X + 20 * cost, pt_start.Y + 20 * sint));
                cap_start.Points.Add(new Point(
                     pt_start.X - (_arrow.TailWidth * cost + _arrow.TailHeight * sint),
                     pt_start.Y + (_arrow.TailHeight * cost - _arrow.TailWidth * sint))
                     );
                cap_start.Points.Add(new Point(pt_start.X - 5 * cost, pt_start.Y - 5 * sint));
                cap_start.Points.Add(
                   new Point(
                  pt_start.X - (_arrow.TailWidth * cost - _arrow.TailHeight * sint),
                  pt_start.Y - (_arrow.TailWidth * sint + _arrow.TailHeight * cost))
                  );
            }
            if (lineCapType == LineCapType.LineCap_End) {
                cap_end.Points.Add(
                            new Point(
                                 pt_end.X + (_arrow.HeadWidth * cost - _arrow.HeadHeight * sint),
                                 pt_end.Y + (_arrow.HeadWidth * sint + _arrow.HeadHeight * cost))
                         );
                cap_end.Points.Add(new Point(pt_end.X, pt_end.Y));
                cap_end.Points.Add(
                 new Point(
                     pt_end.X + (_arrow.HeadWidth * cost + _arrow.HeadHeight * sint),
                      pt_end.Y - (_arrow.HeadHeight * cost - _arrow.HeadWidth * sint))
               );
                cap_end.Points.Add(new Point(pt_end.X - 25 * cost, pt_end.Y - 25 * sint));
                cap_end.Points.Add(
                           new Point(
                                pt_end.X + (_arrow.HeadWidth * cost - _arrow.HeadHeight * sint),
                                pt_end.Y + (_arrow.HeadWidth * sint + _arrow.HeadHeight * cost))
                        );
            }
        }
        /// <summary>
        /// 非封闭的三角形线帽
        /// </summary>
        /// <param name="pt_start">端点1</param>
        /// <param name="pt_end">端点2</param>
        /// <param name="cap_start">端点1形状</param>
        /// <param name="cap_end">端点2形状</param>
        private void SetTriangle3Cap(LineCapType lineCapType, Point pt_start, Point pt_end, Polygon cap_start, Polygon cap_end) {
            double theta = Math.Atan2(pt_start.Y - pt_end.Y, pt_start.X - pt_end.X);
            double sint = Math.Sin(theta);
            double cost = Math.Cos(theta);
            if (lineCapType == LineCapType.LineCap_Start) {
                cap_start.Points.Add(
                    new Point(
                   pt_start.X - (_arrow.TailWidth * cost - _arrow.TailHeight * sint),
                   pt_start.Y - (_arrow.TailWidth * sint + _arrow.TailHeight * cost))
                   );
                cap_start.Points.Add(new Point(pt_start.X, pt_start.Y));
                cap_start.Points.Add(new Point(
                     pt_start.X - (_arrow.TailWidth * cost + _arrow.TailHeight * sint),
                     pt_start.Y + (_arrow.TailHeight * cost - _arrow.TailWidth * sint))
                     );
                cap_start.Points.Add(new Point(pt_start.X - 1 * cost, pt_start.Y - 1 * sint));
                cap_start.Points.Add(
                   new Point(
                  pt_start.X - (_arrow.TailWidth * cost - _arrow.TailHeight * sint),
                  pt_start.Y - (_arrow.TailWidth * sint + _arrow.TailHeight * cost))
                  );
            }
            if (lineCapType == LineCapType.LineCap_End) {
                cap_end.Points.Add(
                            new Point(
                                 pt_end.X + (_arrow.HeadWidth * cost - _arrow.HeadHeight * sint),
                                 pt_end.Y + (_arrow.HeadWidth * sint + _arrow.HeadHeight * cost))
                         );
                cap_end.Points.Add(new Point(pt_end.X, pt_end.Y));
                cap_end.Points.Add(
                 new Point(
                     pt_end.X + (_arrow.HeadWidth * cost + _arrow.HeadHeight * sint),
                      pt_end.Y - (_arrow.HeadHeight * cost - _arrow.HeadWidth * sint))
               );
                cap_end.Points.Add(new Point(pt_end.X - 1 * cost, pt_end.Y - 1 * sint));
                cap_end.Points.Add(
                           new Point(
                                pt_end.X + (_arrow.HeadWidth * cost - _arrow.HeadHeight * sint),
                                pt_end.Y + (_arrow.HeadWidth * sint + _arrow.HeadHeight * cost))
                        );
            }
        }
        /// <summary>
        /// 封闭矩形线帽
        /// </summary>
        /// <param name="pt_start">端点1</param>
        /// <param name="pt_end">端点2</param>
        /// <param name="cap_start">端点1形状</param>
        /// <param name="cap_end">端点2形状</param>
        private void SetRectCap(LineCapType lineCapType, Point pt_start, Point pt_end, Polygon cap_start, Polygon cap_end) {
            double theta = Math.Atan2(pt_start.Y - pt_end.Y, pt_start.X - pt_end.X);
            double sint = Math.Sin(theta);
            double cost = Math.Cos(theta);
            if (lineCapType == LineCapType.LineCap_Start) {
                cap_start.Points.Add(
                    new Point(
                   pt_start.X - (_arrow.TailWidth * cost - _arrow.TailHeight * sint),
                   pt_start.Y - (_arrow.TailWidth * sint + _arrow.TailHeight * cost))
                   );
                cap_start.Points.Add(new Point(pt_start.X, pt_start.Y));
                cap_start.Points.Add(new Point(
                     pt_start.X - (_arrow.TailWidth * cost + _arrow.TailHeight * sint),
                     pt_start.Y + (_arrow.TailHeight * cost - _arrow.TailWidth * sint))
                     );
                cap_start.Points.Add(new Point(pt_start.X - 60 * cost, pt_start.Y - 60 * sint));
                cap_start.Points.Add(
                   new Point(
                  pt_start.X - (_arrow.TailWidth * cost - _arrow.TailHeight * sint),
                  pt_start.Y - (_arrow.TailWidth * sint + _arrow.TailHeight * cost))
                  );
            }
            if (lineCapType == LineCapType.LineCap_End) {
                cap_end.Points.Add(
                            new Point(
                                 pt_end.X + (_arrow.HeadWidth * cost - _arrow.HeadHeight * sint),
                                 pt_end.Y + (_arrow.HeadWidth * sint + _arrow.HeadHeight * cost))
                         );
                cap_end.Points.Add(new Point(pt_end.X + 60 * cost, pt_end.Y + 60 * sint));
                cap_end.Points.Add(
                 new Point(
                     pt_end.X + (_arrow.HeadWidth * cost + _arrow.HeadHeight * sint),
                      pt_end.Y - (_arrow.HeadHeight * cost - _arrow.HeadWidth * sint))
               );
                cap_end.Points.Add(new Point(pt_end.X, pt_end.Y));
                cap_end.Points.Add(
                           new Point(
                                pt_end.X + (_arrow.HeadWidth * cost - _arrow.HeadHeight * sint),
                                pt_end.Y + (_arrow.HeadWidth * sint + _arrow.HeadHeight * cost))
                        );
            }
        }
        #endregion
        #region Dotted Style
        private EnumDottedStyle _DottedStyle = EnumDottedStyle.NULL;
        public EnumDottedStyle DottedStyle {
            set { _DottedStyle = value; this.StrokeDashArray = SetDottedStyle(_DottedStyle); }
            get { return _DottedStyle; }
        }
        protected DoubleCollection SetDottedStyle(EnumDottedStyle style) {
            DoubleCollection dotted = new DoubleCollection();
            if (style == EnumDottedStyle.NULL) {
                dotted = null;
                return dotted;
            }
            if (dotted == null) {
                dotted = new DoubleCollection();
            }
            dotted.Clear();
            switch (style) {
                case EnumDottedStyle.STYLE1:
                    dotted.Add(3); dotted.Add(1);
                    break;
                case EnumDottedStyle.STYLE2:
                    dotted.Add(1); dotted.Add(1); dotted.Add(4); dotted.Add(1);
                    break;
                case EnumDottedStyle.STYLE3:
                    dotted.Add(7); dotted.Add(2);
                    break;
                case EnumDottedStyle.STYLE4:
                    dotted.Add(5); dotted.Add(1);
                    break;
                case EnumDottedStyle.STYLE5:
                    dotted.Add(1); dotted.Add(1);
                    break;
            }
            return dotted;
        }
        #endregion
        #region Overrides
        protected override Geometry DefiningGeometry {
            get {
                // Create a StreamGeometry for describing the shape
                StreamGeometry geometry = new StreamGeometry();
                geometry.FillRule = FillRule.EvenOdd;
                geometry.Freeze();
                return geometry;
            }
        }
        #endregion
    }


}
