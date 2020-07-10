using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Corel.Interop.VGCore;
using win = System.Windows;

namespace FacaCaixaAutoToolBar
{

    /// <summary>
    /// Promove a base para criação de cortes para caixas com base retangulares, entrega método para criação do corpo da caixa e sua área
    /// </summary>
    public abstract class FacaRectBase : FacaBase
    {
        public FacaRectBase()
            : base()
         { }

        protected override void DrawVol()
        {
            this.volume = this.height * this.width * this.length;
            base.drawVol();
        }
        protected void Draw()
        {
            base.Draw();
        }
        protected override void DrawBody()
        {

            //for (int i = 0; i < numFaces + 1; i++)

            Shape linesVert1 = this.drawLineDashBlack(0, 0, 0, this.height);
            linesVert1.Name = "Vinco Lateral 1";
            Shape linesVert2 = this.drawLineDashBlack(length, 0, length, this.height);
            linesVert2.Name = "Vinco Lateral 2";
            Shape linesVert3 = this.drawLineDashBlack(width + length, 0, width + length, this.height);
            linesVert3.Name = "Vinco Lateral 3";
            Shape linesVert4 = this.drawLineDashBlack(width + 2 * length, 0, width + 2 * length, this.height);
            linesVert4.Name = "Vinco Lateral 4";
            Shape linesVert5 = this.drawLine(2 * width + 2 * length, 0, 2 * width + 2 * length, this.height);
            linesVert5.Name = "Contorno Corpo Lateral Direito";
            Shape lineBottom = this.drawLineDashBlack(0, 0, 2 * width + 2 * length, 0);
            lineBottom.Name = "Vinco Base";
            Shape lineTop1 = this.drawLineDashBlack(0, height, length, height);
            lineTop1.Name = "Vinco Aba Segurança";
            Shape lineTop2 = this.drawLineDashBlack(length, height, length + width, height);
            lineTop2.Name = "Vinco Aba Lateral Esquerda";
            Shape lineTop3 = this.drawLine(length + width, height, 2 * length + width, height);
            lineTop3.Name = "Contorno Superior Corpo";
            Shape lineTop4 = this.drawLineDashBlack(2 * length + width, height, 2 * length + 2 * width, height);
            lineTop4.Name = "Vinco Aba Lateral Direita";
            linesVert1.AddToSelection();
            linesVert2.AddToSelection();
            linesVert3.AddToSelection();
            linesVert4.AddToSelection();
            linesVert5.AddToSelection();
            lineBottom.AddToSelection();
            lineTop1.AddToSelection();
            lineTop2.AddToSelection();
            lineTop3.AddToSelection();
            lineTop4.AddToSelection();

            Shape body = ControlUI.corelApp.ActiveSelection.Group();
            body.Name = "Corpo";

            //desenha aba colagem
            win.Point[] points = new win.Point[4];
            points[0] = new win.Point(); points[0].X = 0; points[0].Y = 0;
            points[1] = new win.Point(); points[1].X = -securityTabSize; points[1].Y = 4;
            points[2] = new win.Point(); points[2].X = -securityTabSize; points[2].Y = height - 4;
            points[3] = new win.Point(); points[3].X = 0; points[3].Y = height;
            Shape tabGlue = this.connectPoints(points, 0, LineStyle.NormalBlack);
            tabGlue.Name = "Aba de Colagem";
           

            points = new win.Point[6];

            points[0] = new win.Point();
            points[1] = new win.Point();
            points[2] = new win.Point( );
            points[3] = new win.Point();
            points[4] = new win.Point( );
            points[5] = new win.Point();
            points[0].X=0;
            points[1].X=0;
            points[2].X=4;
            points[3].X=length - 4;
            points[4].X=length;
            points[5].X=length;
            points[0].Y= height;
            points[1].Y=height + width;
            points[2].Y= height + width + securityTabSize;
            points[3].Y=points[2].Y;
            points[4].Y=height + width;
            points[5].Y = height;



            Shape tabSecurity = this.connectPoints(points, 0, LineStyle.NormalBlack);
            tabSecurity.Name = "Contorno Aba Segurança";

            Shape tabSecurity2 = this.drawLineDashBlack(0, height + width, length, height + width);
            tabSecurity2.Name = "Vinco Aba Segurança";
            tabSecurity.AddToSelection();
            tabSecurity2.AddToSelection();
            Shape tabGroupSecurity = ControlUI.corelApp.ActiveSelection.Group();
            tabGroupSecurity.Name = "Aba Segurança";


           
        }

        protected override void DrawTabCoverSide()
        {


            win.Point[] points = new win.Point[7];

            points[0] = new win.Point();
            points[0].X = length; points[0].Y = height;
            points[1] = new win.Point();
            points[1].X = length + 2; points[1].Y = height + 2 * (Math.Tan(45 * Math.PI / 180));
            points[2] = new win.Point();
            points[2].X = length + 2; points[2].Y = height + (width * 2 / 3);
            points[3] = new win.Point();
            points[3].X = (length + 2) + width - width / 3 * Math.Tan(45 * Math.PI / 180); points[3].Y = height + (width * 2 / 3);
            points[4] = new win.Point();
            points[4].X = length + width - 2; points[4].Y = height + width / 3;
            points[5] = new win.Point();
            points[5].X = length + width; points[5].Y = height + width / 3;
            points[6] = new win.Point();
            points[6].X = length + width;points[6].Y= height;
            Shape tabCoverSideLeft = this.connectPoints(points, length, LineStyle.NormalBlack);
            tabCoverSideLeft.Name = "Aba Lateral Tampa Esquerda";
            points[0] = new win.Point();
            points[0].X = length + width + length; points[0].Y = height;
            points[1] = new win.Point();
            points[1].X = points[0].X; points[1].Y = height + width / 3;
            points[2] = new win.Point();
            points[2].X = points[0].X + 2; points[2].Y = points[1].Y;
            points[3] = new win.Point();
            points[3].X = points[0].X - 2 + width / 3 * Math.Tan(45 * Math.PI / 180); points[3].Y = height + width * 2 / 3;
            points[4] = new win.Point();
            points[4].X = points[0].X + width - 2; points[4].Y = points[3].Y;
            points[5] = new win.Point();
            points[5].X = points[4].X; points[5].Y = height + 2 * Math.Tan(45 * Math.PI / 180);
            points[6] = new win.Point();
            points[6].X = points[0].X + width; points[6].Y = height;
            Shape tabCoverSideRight = this.connectPoints(points, length, LineStyle.NormalBlack);
            tabCoverSideRight.Name = "Aba Lateral Tampa Direita";
        }
        protected override void DrawAreaArte()
        {
            
        }

    }
}
