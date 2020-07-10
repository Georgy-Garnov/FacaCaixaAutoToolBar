
using Corel.Interop.VGCore;

namespace FacaCaixaAutoToolBar
{
    public static class CDRExtensions
    {
        /// <summary>
        /// Start a command Group, active Optimization, desactive Events
        /// </summary>
        /// <param name="corelApp"></param>
        public static void BeginDraw(this Application corelApp)
        {
            corelApp.ActiveDocument.Unit = cdrUnit.cdrMillimeter;
            corelApp.ActiveDocument.BeginCommandGroup();
            corelApp.Optimization = true;
            corelApp.EventsEnabled = false;
        }
        /// <summary>
        /// Finish a command Group, deactive Optimization, active Events , Reflesh UI
        /// </summary>
        /// <param name="corelApp"></param>
        public static void EndDraw(this Application corelApp)
        {
            corelApp.ActiveDocument.EndCommandGroup();
            corelApp.Optimization = false;
            corelApp.Refresh();
            corelApp.EventsEnabled = true;
        }
    }
}
