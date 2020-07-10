using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacaCaixaAutoToolBar
{
    /// <summary>
    /// Salva dois pontos no plano cartesiano
    /// </summary>
    public class Line
    {
        private double sx, ex, sy, ey;
        /// <summary>
        /// Salva dois pontos no plano cartesiano
        /// </summary>
        /// <param name="sX">Valor no eixo X inicial</param>
        /// <param name="sY">Valor no eixo Y inicial</param>
        /// <param name="eX">Valor no eixo X final</param>
        /// <param name="eY">Valor no eixo Y final</param>
        public Line(double sX, double sY, double eX, double eY)
        {
            this.sx = sX;
            this.sy = sY;
            this.ex = eX;
            this.ey = eY;
        }
        /// <summary>
        /// Get ou Set valor no eixo X inicial
        /// </summary>
        public double sX
        {
            get { return sx; }
            set { sx = value; }
        }
        /// <summary>
        /// Get ou Set valor no eixo X final
        /// </summary>
        public double eX
        {
            get { return ex; }
            set { ex = value; }
        }
        /// <summary>
        /// Get ou Set valor no eixo Y final
        /// </summary>
        public double eY
        {
            get { return ey; }
            set { ey = value; }
        }
        /// <summary>
        /// Get ou Set valor no eixo Y inicial
        /// </summary>
        public double sY
        {
            get { return sy; }
            set { sy = value; }
        }
    }
}
