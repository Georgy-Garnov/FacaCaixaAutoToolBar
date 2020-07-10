using System;
using win = System.Windows;
using Corel.Interop.VGCore;
namespace FacaCaixaAutoToolBar
{
    class FacaSemiAuto : FacaRectBase, IFaca
    {
        public const string name = "Semi-Automática";
        public const bool simetric = false;

        public FacaSemiAuto()
            : base()
        {

        }
        public void Draw()
        {
            try
            {
                ControlUI.corelApp.BeginDraw();
                base.Draw();
                base.DrawBody();
                //this.DrawVol();
                //this.DrawTab();
                this.DrawTabBottomSide();
                this.DrawTabCoverSide();
                this.DrawTabBottomFit();
            }
            catch { }
            finally
            {
                ControlUI.corelApp.EndDraw();

            }
        }

        public double CalcVolume()
        {
            return this.height * this.width * this.length; ;
        }

        private void DrawTabBottomSide()
        {
            double c = 2 * length + width;
            win.Point[] points = new win.Point[5];
            points[0] = new win.Point(); points[0].X = c; points[0].Y = 0;
            points[1] = new win.Point();
            points[1].X = c; points[1].Y = -width * 2 / 3;
            points[2] = new win.Point();
            points[2].X = c + width * 2 / 3 * Math.Tan(45 * Math.PI / 180); points[2].Y = -width * 2 / 3;
            points[3] = new win.Point();
            points[3].X = c + width / 2 * Math.Tan(45 * Math.PI / 180); points[3].Y = -width / 2;
            points[4] = new win.Point();
            points[4].X = c + width; points[4].Y = 0;
            Shape ae = this.connectPoints(points, length, LineStyle.NormalBlack);
            ae.Name = "Aba Aux Fundo Direito";
            points[0] = new win.Point();
            points[0].X = length; points[0].Y = 0;
            points[1] = new win.Point();
            points[1].X = length + width / 2 * Math.Tan(45 * Math.PI / 180); points[1].Y = -width / 2;
            points[2] = new win.Point();
            points[2].X = length + width - width * 2 / 3 * Math.Tan(45 * Math.PI / 180); points[2].Y = -width * 2 / 3;
            points[3] = new win.Point();
            points[3].X = length + width; points[3].Y = -width * 2 / 3;
            points[4] = new win.Point();
            points[4].X = length + width; points[4].Y = 0;
            Shape ad = this.connectPoints(points, length, LineStyle.NormalBlack);
            ad.Name = "Aba Aux Fundo Esquerdo";
        }

        private void DrawTabBottomFit()
        {
            double TanAngle = 0;
            if (width < length)
                TanAngle = Math.Tan(45 * Math.PI / 180);
            if (width > length)
                TanAngle = Math.Tan(20 * Math.PI / 180);
            if (width == length)
                TanAngle = Math.Tan(30 * Math.PI / 180);

            win.Point[] points = new win.Point[6];
            points[0] = new win.Point();
            points[0].X = 0; points[0].Y = 0;
            points[1] = new win.Point();
            points[1].X = width / 2 * TanAngle; points[1].Y = -width / 2;
            points[2] = new win.Point();
            points[2].X = width / 2 * TanAngle; points[2].Y = -width * 2 / 3;
            points[3] = new win.Point();
            points[3].X = length - points[2].X; points[3].Y = -width * 2 / 3;
            points[4] = new win.Point();
            points[4].X = length - points[2].X; points[4].Y = -width / 2;
            points[5] = new win.Point();
            points[5].X = length; points[5].Y = 0;

            for (int i = 0; i < points.Length; i++)
            {
                points[i].X = points[i].X + length + width;
            }
            Shape tabLockFemale = this.connectPoints(points, length, LineStyle.NormalBlack);
            tabLockFemale.Name = "Aba Trava Macho";


            points = new win.Point[8];
            points[0] = new win.Point();
            points[0].X = 0; points[0].Y = 0;
            //points[1] = new Point(0, -width / 2);
            points[1] = new win.Point();
            points[1].X = 0; points[1].Y = -width * 2 / 3;
            //points[2] = new Point(width / 2 * TanAngle, -width / 2);
            points[2] = new win.Point();
            points[2].X = width / 2 * TanAngle; points[2].Y = -width * 2 / 3;
            //points[3] = new Point(points[2].X, -width / 3);
            points[3] = new win.Point();
            points[3].X = points[2].X; points[3].Y = -width / 2;
            //points[4] = new Point(length - points[2].X, -width / 3);
            points[4] = new win.Point();
            points[4].X = length - points[2].X; points[4].Y = -width / 2;
            //points[5] = new Point(points[4].X, -width / 2);
            points[5] = new win.Point();
            points[5].X = points[4].X; points[5].Y = -width * 2 / 3;
            //points[6] = new Point(length, -width / 2);
            points[6] = new win.Point();
            points[6].X = length; points[6].Y = -width * 2 / 3;
            points[7] = new win.Point();
            points[7].X = length; points[7].Y = 0;

            Shape tabLockMale = this.connectPoints(points, 0, LineStyle.NormalBlack);
            tabLockMale.Name = "Aba Trava Femea";
        }

        public void Mirror() { }
        public void UpDown() { }
        public void DrawVol() { }
    }
}
