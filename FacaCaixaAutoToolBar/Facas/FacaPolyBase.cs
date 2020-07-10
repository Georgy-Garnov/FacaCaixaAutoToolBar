using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Corel.Interop.VGCore;
using win = System.Windows;


namespace Bonus630.vsta.FacaCaixaAuto
{
    /// <summary>
    /// Promove a base para criação de cortes para caixas com base poligonais regulares, entrega método para criação do corpo da caixa e sua base
    /// </summary>
    public abstract class FacaPolyBase : FacaBase
    {
        private double radius;
        private int angle;
        private int fixRotationAngle;

        private int numFaces;
        protected int NumFaces { get { return numFaces; } set { if (value > 2)numFaces = value; else numFaces = 4; findAngleRadius(); } } 

        
        public FacaPolyBase():base()
        {
            
        }
        public void Draw()
        {

        }
        private void findAngleRadius()
        {
          
            switch (this.numFaces)
            {
                case 3:
                    radius = width * Math.Sqrt(3) / 3;
                    fixRotationAngle = 90;
                break;
                case 4:
                    radius = width * Math.Sqrt(2) / 2;
                    fixRotationAngle = 45;
                break;
                case 5:
                    radius = 20 /(Math.Sqrt(10-2*Math.Sqrt(5)));
                    fixRotationAngle = 18;
                break;
                case 6: 
                    radius = width;
                    fixRotationAngle = 0;
                break;
                case 7:
                   // radius = width;
                   // fixRotationAngle = 0;
                break;
                case 8:
                   // radius = width;
                   // fixRotationAngle = 0;
                break;
                case 9:
                   // radius = width;
                   // fixRotationAngle = 0;
                break;
                case 10:
                    radius = (Math.Sqrt(5) - 1) / 2 * width;
                    fixRotationAngle = 0;
                break;
            }
            angle = 360 / this.numFaces;
        }
       

        protected override void DrawAreaArte() { }
        protected override void DrawTabCoverSide() { }
        protected override void DrawBody() 
        {
            for (int i = 0; i <= this.NumFaces; i++)
            {
                Shape linesVert1 = this.drawLineDashBlack(i*this.length, 0, i*this.length, this.height);
                linesVert1.Name = "Vinco Lateral "+(i+1);

            }
            Shape lineBottom = this.drawLineDashBlack(0, 0,this.NumFaces * length, 0);
            lineBottom.Name = "Vinco Base";
            Shape lineTop = this.drawLineDashBlack(0, this.height, this.NumFaces * length, this.height);
            lineTop.Name = "Vinco Topo";
            //desenha aba colagem
            win.Point[] points = new win.Point[4];
            points[0] = new win.Point(); points[0].X = 0; points[0].Y = 0;
            points[1] = new win.Point(); points[1].X = -securityTabSize; points[1].Y = 4;
            points[2] = new win.Point(); points[2].X = -securityTabSize; points[2].Y = height - 4;
            points[3] = new win.Point(); points[3].X = 0; points[3].Y = height;
            Shape tabGlue = this.connectPoints(points, 0, LineStyle.NormalBlack);
            tabGlue.Name = "Aba de Colagem";
        }
        //protected override void DrawBody(double newHeight, double newWidth, double newLength)
        //{
        //    this.height = newHeight;
        //    this.width = newWidth;
        //    this.length = newLength;
        //    this.DrawBody();
        //}
        protected override void DrawVol() { }
        /// <summary>
        /// Desenha a aba no formato do poligono regular de n faces
        /// </summary>
        /// <returns>Retorna o poligono</returns>
        protected Shape DrawPolyTab()
        {
            win.Point[] points = new win.Point[this.NumFaces + 1];
            for (int i = 0; i < this.NumFaces + 1; i++)
            {
                double rad = this.angle * i * Math.PI / 180;
                points[i] = new win.Point();
                points[i].X = this.radius * Math.Cos(rad); points[i].Y = this.radius * Math.Sin(rad);
            }
            Shape PolyTab = this.connectPoints(points, 0, LineStyle.NormalBlack);
            PolyTab.Rotate(fixRotationAngle);
            return PolyTab;
        }

    }
}
