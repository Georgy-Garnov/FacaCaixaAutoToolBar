using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using win = System.Windows;
using Corel.Interop.VGCore;
namespace Bonus630.vsta.FacaCaixaAuto
{
    class SextSimples : FacaPolyBase, IFaca 
    {
        public const string name = "Sextavada Fundo Simples";
        public const bool simetric = true;
        private win.Point[] points;

        public SextSimples()
            : base()
        {
            this.NumFaces =6;
             
        }
        public void Draw()
        {
            FacaBase.app.Optimization = true;
            base.Draw();
            base.DrawBody();
            this.DrawVol();
           // this.DrawTab();
            this.DrawTabBottomSide();
            this.DrawTabCoverSide();
            this.DrawTabBottomFit();
            this.drawTabQuad();
            this.drawTabTria();
            this.drawTabSex();
            FacaBase.app.Optimization = false;
            FacaBase.app.Refresh();
        }

       public double CalcVolume()
        {
            return 0101010101;
        }
            
        

        private void DrawTabBottomSide()
        {
           
        }

        private void DrawTabBottomFit()
        {
          
        }
        private void drawTabQuad()
        {
            //Corrigir estes pontos
            points = new win.Point[5];
            points[0] = new win.Point();
            points[0].X=0;points[0].Y= height;
            points[1] = new win.Point();
            points[1].X=2;points[1].Y= 2+height;
            points[2] = new win.Point();
            points[2].X=2;points[2].Y=(width / 2)+height ;
            points[3] = new win.Point();
            points[3].X=points[2].X+width; points[3].Y=points[2].Y;
            points[4] = new win.Point();
            points[4].X = width - points[2].X; points[4].Y = height;
           
          

            Shape tabLockMale = this.connectPoints(points, 0, LineStyle.NormalBlack);
            tabLockMale.Name = "Aba Trava Macho";
        }
        private void drawTabTria()
        {
            points = new win.Point[3];
            points[0] = new win.Point();
            points[0].X=length;points[0].Y= height;
            points[1] = new win.Point();
            points[1].X=length+length/2;points[1].Y= height+(length * Math.Sqrt(3)/2);
            points[2] = new win.Point();
            points[2].X=length*2;points[2].Y= height;
            Shape tabTri = this.connectPoints(points, 0, LineStyle.NormalBlack);
            tabTri.Name = "Aba triangular";

            for (int i = 0; i < points.Length; i++)
            {
                points[i].X += length;
            }
            Shape tabTri2 = this.connectPoints(points, 0, LineStyle.NormalBlack);
            tabTri2.Name = "Aba triangular 2";
            for (int i = 0; i < points.Length; i++)
            {
                points[i].X += length;
            }
            Shape tabTri3 = this.connectPoints(points, 0, LineStyle.NormalBlack);
            tabTri3.Name = "Aba triangular 3";
        }
        private void drawTabSex()
        {

            Shape tabSex = this.DrawPolyTab();
            tabSex.Name = "Aba Sextavada";
        }
        public void Mirror() { }
        public void UpDown() { }
        public void DrawVol() { }
    }
}
