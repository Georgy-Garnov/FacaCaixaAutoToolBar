using System;
using System.Linq;
using Corel.Interop.VGCore;
using win = System.Windows;

namespace FacaCaixaAutoToolBar
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class FacaBase
    {

        //comprimento = x | largura = y | altura = z

        
        public static Document newDoc;
        protected double height;
        protected double width;
        protected double length;
        protected double volume;
        private win.Point initialPosition;
        protected double securityTabSize = 8;
       
        
        protected bool isSimetric { get; set; }
        private double penWidth = 0.4;
       

        protected double initialPositionX { get { return initialPosition.X; } set { initialPosition.Y = value; } }
        protected double initialPositionY { get { return initialPosition.X; } set { initialPosition.Y = value; } }

        public enum LineStyle
        {
            NormalBlack,
            DashBlack,
            DashRed,
        }

        protected Layer layerFaca;

        public FacaBase() { }

        protected abstract void DrawVol();
        protected void drawVol()
        {
            Layer layerVolume = ControlUI.corelApp.ActiveDocument.ActivePage.CreateLayer("Volume");
            layerVolume.CreateArtisticText(0, -5, "Volume: " + this.volume + " " + ControlUI.corelApp.ActiveDocument.Unit.ToString(), cdrTextLanguage.cdrBrazilianPortuguese, cdrTextCharSet.cdrCharSetDefault, "Arial", 14.0f, cdrTriState.cdrFalse, cdrTriState.cdrFalse, cdrFontLine.cdrNoFontLine, cdrAlignment.cdrLeftAlignment);
            layerVolume.Printable = false;
            
        }
        /// <summary>
        /// Liga um array de Points para criação de um Shape
        /// </summary>
        /// <param name="pontos">Array de Points a serem ligados</param>
        /// <param name="inicio">Posição inicial do shape utilize 0 se não sabe</param>
        /// <param name="lineStyle">Estilo da linha usada no Shape</param>
        /// <returns></returns>
        protected Shape connectPoints(win.Point[] pontos, double inicio, LineStyle lineStyle)
        {

            SubPath subpath;
            Curve curve;
            curve = ControlUI.corelApp.CreateCurve(ControlUI.corelApp.ActiveDocument);
            subpath = curve.CreateSubPath(pontos[0].X + this.initialPositionX, pontos[0].Y + this.initialPositionY);
            for (int i = 1; i < pontos.Length; i++)
                subpath.AppendLineSegment(pontos[i].X + this.initialPositionX, pontos[i].Y + this.initialPositionY);
            Shape group = layerFaca.CreateCurve(curve);
            switch (lineStyle)
            {
                case LineStyle.NormalBlack:
                    group.Outline.SetProperties(penWidth, null, null, null, null, cdrTriState.cdrUndefined, cdrTriState.cdrTrue, cdrOutlineLineCaps.cdrOutlineUndefinedLineCaps, cdrOutlineLineJoin.cdrOutlineUndefinedLineJoin, -9999, 0, -1, 0, 0);

                    break;
                case LineStyle.DashBlack:
                    group.Outline.SetProperties(penWidth, this.fastOutlineStyle(), null, null, null, cdrTriState.cdrUndefined, cdrTriState.cdrTrue, cdrOutlineLineCaps.cdrOutlineUndefinedLineCaps, cdrOutlineLineJoin.cdrOutlineUndefinedLineJoin, -9999, 0, -1, 0, 0);

                    break;
                case LineStyle.DashRed:
                    Color colorRed = new Color();
                    colorRed.CMYKAssign(0, 100, 0, 0);
                    group.Outline.SetProperties(penWidth, this.fastOutlineStyle(), colorRed, null, null, cdrTriState.cdrUndefined, cdrTriState.cdrTrue, cdrOutlineLineCaps.cdrOutlineUndefinedLineCaps, cdrOutlineLineJoin.cdrOutlineUndefinedLineJoin, -9999, 0, -1, 0, 0);

                    break;
            }
            return group;

        }
        protected void Draw()
        {
            
            if (ControlUI.corelApp.Documents.Count == 0)
            {
                newDoc = ControlUI.corelApp.Application.CreateDocument();
            }
            else
            {
                newDoc = ControlUI.corelApp.Application.ActiveDocument;
            }
            //newDoc.LayerChange += new DIDrawDocumentEvents_LayerChangeEventHandler(newDoc_LayerChange);
            //newDoc.LayerChange += new DIVGDocumentEvents_LayerChangeEventHandler(newDoc_LayerChange);
            newDoc.Unit = cdrUnit.cdrMillimeter;
            newDoc.Rulers.HUnits = cdrUnit.cdrMillimeter;
            newDoc.Rulers.VUnits = cdrUnit.cdrMillimeter;
            //double pageWidth;
            //double pageHeight;
            //newDoc.ActivePage.GetSize(out pageWidth, out pageHeight);
            initialPosition = new win.Point();
            initialPosition.X = 16.0;
            initialPosition.Y = 32.0;
            Layers ls = ControlUI.corelApp.ActivePage.AllLayers;
            foreach (Layer l in ls)
            {
                if (l.Name == "Faca")
                {

                    layerFaca = l;
                    break;
                }
            }
            if (layerFaca == null)
                layerFaca = ControlUI.corelApp.ActivePage.CreateLayer("Faca");
           // createBonus630TM();

        }


        //void newDoc_LayerChange(Layer Layer)
        //{
        //    if (Layer.Name == "bonus630TM")
        //    {
        //        if (!Layer.Visible)
        //            Layer.Visible = true;
        //        if (Layer.Printable)
        //            Layer.Printable = false;
        //        if (Layer.Editable)
        //            Layer.Editable = false;

        //    }

        //}


        //private void createBonus630TM()
        //{

        //    Layer lb = null;
        //    // Layers ls = FacaBase.app.ActivePage.AllLayers;
        //    Layers ls = FacaBase.app.ActiveDocument.MasterPage.AllLayers;
        //    foreach (Layer l in ls)
        //    {

        //        if (l.Name == "bonus630TM")
        //        {
        //            lb = l;
        //            break;
        //        }
        //    }
        //    if (lb == null)
        //        //lb = newDoc.ActivePage.CreateLayer("bonus630TM");
        //        lb = newDoc.MasterPage.CreateLayer("bonus630TM");
        //    lb.Visible = true;
        //    lb.Editable = false;
        //    lb.Printable = false;
        //    Shapes shapes = lb.Shapes;
        //    bool exist = false;
        //    foreach (Shape shape in shapes)
        //    {
        //        if (shape.Name == "bonus630TM")
        //        {
        //            exist = true;
        //        }
        //    }
        //    if (!exist)
        //    {
        //        Color cor = new Color();
        //        cor.CMYKAssign(60, 0, 0, 50);

        //        //global::System.Windows.MessageBox.Show(newDoc.ActivePage.BottomY.ToString());
        //        Shape textBonus = lb.CreateArtisticText(newDoc.ActivePage.LeftX, newDoc.ActivePage.BottomY - 5, "Gerador de Corte e Vinco Bonus630," + System.Environment.NewLine + "Está aplicação é gratuita não pague por ela," + System.Environment.NewLine + " não se preocupe este texto não será impresso!", cdrTextLanguage.cdrBrazilianPortuguese
        //               , cdrTextCharSet.cdrCharSetDefault, "Verdana", 12.0f, cdrTriState.cdrFalse, cdrTriState.cdrFalse, cdrFontLine.cdrNoFontLine, cdrAlignment.cdrNoAlignment);
        //        textBonus.Name = "bonus630TM";
        //        textBonus.Fill.ApplyUniformFill(cor);
        //    }


        //}
        private double convertToDouble(string numStr)
        {
            if (numStr.Contains(','))
                numStr = numStr.Replace(',', '.');
            return Convert.ToDouble(numStr);
        }

        public void SetValues(string height, string width, string length)
        {
            this.height = 0;
            this.width = 0;
            this.length = 0;
            if(!string.IsNullOrEmpty(height))
                this.height = convertToDouble(height);
            if(!string.IsNullOrEmpty(width))
                this.width = convertToDouble(width);
             if(!string.IsNullOrEmpty(length))
                 this.length = convertToDouble(length);

            //Fazendo está alteração para garantir o formato correto nas abas
            if(this.width>this.length)
            {
                double temp = this.width;
                this.width = this.length;
                this.length = temp;
            }

        }

        /// <summary>
        /// Desenha uma linha pontinhada preta, passando ponto inicial e final
        /// </summary>
        /// <param name="sX">Posição x inicial no plano</param>
        /// <param name="sY">Posição y inicial no plano</param>
        /// <param name="eX">Posição x final no plano</param>
        /// <param name="eY">Posição y final no plano</param>
        /// <returns>Retorna um Shape em forma de linha pontinhada preta</returns>
        public Shape drawLineDashBlack(double sX, double sY, double eX, double eY)
        {
            Shape line = layerFaca.CreateLineSegment(sX + this.initialPositionX, sY + this.initialPositionY, eX + this.initialPositionX, eY + this.initialPositionY);
            line.Outline.SetProperties(penWidth, this.fastOutlineStyle(), null, null, null, cdrTriState.cdrUndefined, cdrTriState.cdrTrue, cdrOutlineLineCaps.cdrOutlineUndefinedLineCaps, cdrOutlineLineJoin.cdrOutlineUndefinedLineJoin, -9999, 0, -1, 0, 0);
            return line;
        }
        public Shape drawLineDashBlack(Line line)
        {
            return drawLineDashBlack(line.sX, line.sY, line.eX, line.eY);
        }
        public Shape drawLineDashBlack(win.Point start, win.Point end)
        {
            return drawLineDashBlack(start.X, start.Y, end.X, end.Y);
        }



        public Shape drawLineDashRed(double sX, double sY, double eX, double eY)
        {
            Shape line = layerFaca.CreateLineSegment(sX + this.initialPositionX, sY + this.initialPositionY, eX + this.initialPositionX, eY + this.initialPositionY);
            Color colorRed = new Color();
            colorRed.CMYKAssign(0, 100, 0, 0);
            line.Outline.SetProperties(penWidth, this.fastOutlineStyle(), colorRed, null, null, cdrTriState.cdrUndefined, cdrTriState.cdrTrue, cdrOutlineLineCaps.cdrOutlineUndefinedLineCaps, cdrOutlineLineJoin.cdrOutlineUndefinedLineJoin, -9999, 0, -1, 0, 0);
            return line;
        }
        public Shape drawLineDashRed(Line linha)
        {
            return drawLineDashRed(linha.sX, linha.sY, linha.eX, linha.eY);
        }
        public Shape drawLineDashRed(win.Point inicio, win.Point fim)
        {
            return drawLineDashRed(inicio.X, inicio.Y, fim.X, fim.Y);
        }



        public Shape drawLine(double sX, double sY, double eX, double eY)
        {
            Shape line = layerFaca.CreateLineSegment(sX + this.initialPositionX, sY + this.initialPositionY, eX + this.initialPositionX, eY + this.initialPositionY);
            line.Outline.Width = penWidth;
            return line;
        }
        public Shape drawLine(Line line)
        {
            return drawLine(line.sX, line.sY, line.eX, line.eY);
        }
        public Shape drawLine(win.Point start, win.Point end)
        {
            return drawLine(start.X, start.Y, end.X, end.Y);
        }




        private OutlineStyle fastOutlineStyle()
        {
            object[] dashparam = { 5, 5 };
            OutlineStyle penDashBlack = ControlUI.corelApp.CreateOutlineStyle(2, dashparam);
            penDashBlack.DashCount = 2;
            penDashBlack.set_DashLength(1, 5);
            return penDashBlack;
        }
        protected abstract void DrawBody();
        protected abstract void DrawTabCoverSide();
        protected abstract void DrawAreaArte();
       
    }
}
