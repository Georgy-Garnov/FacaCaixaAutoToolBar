using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bonus630.vsta.FacaCaixaAuto
{
    class ReducaoConcentrica : FacaBase,IFaca
    {
        public const string name = "Redução Concêntrica";
        public const bool simetric = false;
        public ReducaoConcentrica()
            : base()
        {
            
        }

        public void Draw()
        {
            FacaBase.app.Optimization = true;
            base.Draw();
            FacaBase.app.ActiveLayer.CreateEllipse2(0, 0, 60, 0, 0, 180, false);
            FacaBase.app.ActiveLayer.CreateEllipse2(0, 0, 220, 0, 0, 180, false);
            FacaBase.app.Optimization = false;
            FacaBase.app.Refresh();
        
        }
        public double CalcVolume()
        {
            return 010101010;
        }
        protected override void DrawAreaArte() { }
        protected override void DrawTabCoverSide() { }
        protected override void DrawBody() { }
        public void Mirror() { }
        public void UpDown() { }
        protected override void DrawVol()
        {
           
        }
    
    }
}
