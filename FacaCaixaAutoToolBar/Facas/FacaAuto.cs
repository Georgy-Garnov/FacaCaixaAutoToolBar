using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using win = System.Windows;
using Corel.Interop.VGCore;
namespace FacaCaixaAutoToolBar
{
    class FacaAuto : FacaRectBase, IFaca
    {

        public const string name = "Automática";
        public const bool simetric = false;

        public FacaAuto()
            : base()
        {
            // this.NumFaces = 4;
            this.isSimetric = false;

        }

        public void Draw()
        {
            try
            {
                ControlUI.corelApp.BeginDraw();
                base.Draw();
                base.DrawBody();
                //this.DrawVol();
                this.DrawTab();
                this.DrawTabBottomSide();
                this.DrawTabCoverSide();
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
        public void Mirror() { }
        public void UpDown() { }
        public void DrawVol()
        {
            base.DrawVol();
        }


        public void DrawTab()
        {

            double TanAngle = 0;
            if (width < length)
                TanAngle = Math.Tan(45 * Math.PI / 180);
            if (width > length)
                TanAngle = Math.Tan(30 * Math.PI / 180);
            if (width == length)
                TanAngle = Math.Tan(30 * Math.PI / 180);

            win.Point[] points = new win.Point[11];
            points[0] = new win.Point();
            points[0].X = 0; points[0].Y = 0;
            points[1] = new win.Point();
            points[1].X = Math.Tan(15 * Math.PI / 180) * 2 * width / 3;
            points[1].Y = -(width / 3) * 2;
            points[2] = new win.Point();
            points[2].X = (length / 2) - (width / 12);
            points[2].Y = -(width / 3) * 2;
            points[3] = new win.Point();
            points[3].X = length / 2;
            points[3].Y = -(width / 12) * 7;
            points[4] = new win.Point();
            points[4].X = length / 2; points[4].Y = -(width / 2);
            points[5] = new win.Point();
            if (width == length)
            {
                points[5].X = points[4].X; points[5].Y = points[4].Y;
            }
            else
            {
                points[5].X = length - TanAngle * (width / 2);
                points[5].Y = -(width / 2);
            }
            points[6] = new win.Point();
            points[6].X = (points[5].X) + width / 6 * TanAngle; points[6].Y = -(width / 3 * 2);
            points[7] = new win.Point();
            points[7].X = length - 2; points[7].Y = points[6].Y;
            points[8] = new win.Point();
            points[8].X = length - 2; points[8].Y = 2 * (-4 / Math.Tan(45 * Math.PI / 180));
            points[9] = new win.Point();
            points[9].X = length - 4; points[9].Y = -4 / Math.Tan(45 * Math.PI / 180);
            points[10] = new win.Point();
            points[10].X = length; points[10].Y = 0;
            Shape tabLockBottomLeft = this.connectPoints(points, length, LineStyle.NormalBlack);
            tabLockBottomLeft.Name = "Contorno Aba Trava Fundo Esquerda";
            Shape lineTrac = this.drawLineDashRed(points[5], points[9]);
            lineTrac.Name = "Picote Aba Trava Fundo Esquerda";
            tabLockBottomLeft.AddToSelection();
            lineTrac.AddToSelection();
            Shape tabFE = ControlUI.corelApp.ActiveSelection.Group();
            tabFE.Name = "Aba Trava Fundo Esquerda";
            for (int i = 0; i < points.Length; i++)
                points[i].X += length + width;
            Shape tabLockBottomRight = this.connectPoints(points, length, LineStyle.NormalBlack);
            tabLockBottomRight.Name = "Contorno Aba Trava Fundo Direita";
            Shape lineTrac2 = this.drawLineDashRed(points[5], points[9]);
            lineTrac2.Name = "Picote Aba Trava Fundo Direita";
            tabLockBottomRight.AddToSelection();
            lineTrac2.AddToSelection();
            Shape tabFD = ControlUI.corelApp.ActiveSelection.Group();
            tabFD.Name = "Aba Trava Fundo Direita";

        }

        public void DrawTabBottomSide()
        {
            win.Point[] points = new win.Point[4];

            points[0] = new win.Point();
            points[1] = new win.Point();
            points[2] = new win.Point();
            points[3] = new win.Point();
            points[0].X = length;
            points[1].X = (Math.Tan(15 * Math.PI / 180) * width / 2) + length;
            points[2].X = (length + width) - (Math.Tan(45 * Math.PI / 180) * width / 2);
            points[3].X = length + width;
            points[0].Y = 0;
            points[1].Y = -width / 2;
            points[2].Y = -width / 2;
            points[3].Y = 0;
            Shape abaEsquerda = this.connectPoints(points, length, LineStyle.NormalBlack);
            abaEsquerda.Name = "Aba Aux Fundo Esquerda";
            for (int i = 0; i < points.Length; i++)
                points[i].X += length + width;
            Shape abaDireita = this.connectPoints(points, length, LineStyle.NormalBlack);
            abaDireita.Name = "Aba Aux Fundo Direita";

        }
        protected override void DrawAreaArte()
        {
            base.DrawAreaArte();
            win.Point[] pontos = new win.Point[47];
        }

    }
}
