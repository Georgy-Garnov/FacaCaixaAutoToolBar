using c = Corel.Interop.VGCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Controls;

namespace FacaCaixaAutoToolBar.DataSource
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class FacaDataSource : BaseDataSource
    {
        FacaManager manager;
        Dictionary<string, bool> comboBoxList;
        IFaca objFaca;
        string[] ComboBoxItems = null;
        public FacaDataSource(c.DataSourceProxy proxy) : base(proxy)
        {
            manager = new FacaManager();
            comboBoxList = manager.ClassList();
            GenItems();
        }
        public string Items
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<container>");
                for (int i = 0; i < ComboBoxItems.Length; i++)
                {
                    sb.Append(ComboBoxItems[i]);
                }
                sb.Append("</container>");
                return sb.ToString();
            }
        }
        private int canDraw = 0;
        public int CanDraw
        {
            get { return canDraw; }
            set { canDraw = value; NotifyPropertyChanged(); }
        }

        private double height = 10000;

        public double Height
        {
            get { return height; }
            set { height = value; NotifyPropertyChanged(); }
        }
        private string width = "1000";

        public string Width
        {
            get { return width; }
            set { width = value; NotifyPropertyChanged(); }
        }
        private string lenght;

        public string Lenght
        {
            get { return lenght; }
            set { lenght = value; NotifyPropertyChanged(); }
        }
        private string volume = "0";
        public string Volume 
        { 
            get { return volume; }
            set { volume = value; NotifyPropertyChanged(); }
        }
       
        public void OnFacaSelected()
        {
            createInstance();
        }
        private string currentFaca;
        public string CurrentFaca
        {
            get { return currentFaca; }
            set
            {
                currentFaca = value;

                NotifyPropertyChanged();
            }
        }
      
        public void OnDraw()
        {
            objFaca.Draw();
        }
        private void GenItems()
        {
            ComboBoxItems = new string[comboBoxList.Count];
            int i = 0;
            foreach (var item in comboBoxList)
            {
                string _item = string.Format("<itemData type=\"statusText\" guid=\"{0}\" text=\"{1}\" colId=\"{2}\" />", Guid.NewGuid(), item.Key, i);
                ComboBoxItems[i] = _item;
                i++;
            }
            CurrentFaca = ComboBoxItems[0];
        }
        private void createInstance()
        {
            string name = currentFaca.Substring(currentFaca.IndexOf("text=\"") + 6);
            name = name.Substring(0, name.IndexOf("\" colId="));
            objFaca = manager.Inicialize(name);
            if (objFaca != null)
                CanDraw = 1;
            else
                CanDraw = 0;

        }
        private void setValues()
        {
            objFaca.SetValues("height", width, lenght);
        }
        private void calcVolume()
        {
            Volume = objFaca.CalcVolume().ToString();
        }
    }
}
