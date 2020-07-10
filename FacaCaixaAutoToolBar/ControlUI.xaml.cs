using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using corel = Corel.Interop.VGCore;
using FacaCaixaAutoToolBar.DataSource;
using Corel.Interop.VGCore;
using System.Diagnostics;

namespace FacaCaixaAutoToolBar
{

    public partial class ControlUI : UserControl
    {
        public static corel.Application corelApp;
        private Styles.StylesController stylesController;
        
        public ControlUI(object app)
        {
            InitializeComponent();
            try
            {
                corelApp = app as corel.Application;
                stylesController = new Styles.StylesController(this.Resources, corelApp);
                DataSourceFactory dataSourceFactory = new DataSourceFactory();
                dataSourceFactory.AddDataSource("FacaDS", typeof(FacaDataSource));
                dataSourceFactory.Register();
                
            }
            catch
            {
                global::System.Windows.MessageBox.Show("VGCore Erro");
            }
            btn_Command.Click += (s, e) => {
              
                try
                {
                    DataSourceProxy dsp2 = corelApp.FrameWork.Application.DataContext.GetDataSource("FacaDS");
                    dsp2.InvokeMethod("OnDraw");
                }
                catch(Exception err)
                {
                    Debug.WriteLine(err.Message); 
                }
            
            };
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            stylesController.LoadThemeFromPreference();
        }

    }
}
