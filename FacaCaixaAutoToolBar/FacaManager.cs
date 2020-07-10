using System;
using System.Collections.Generic;
using System.Reflection;

namespace FacaCaixaAutoToolBar
{
    class FacaManager
    {

        private List<Type> correctTypes;
        private IFaca objIFaca;

        public FacaManager()
        {
            correctTypes = new List<Type>();
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type type in types)
            {
                if (typeof(IFaca).IsAssignableFrom(type) && !type.IsInterface)
                {
                    correctTypes.Add(type);
                }
            }
        }
        public Dictionary<string, bool> ClassList()
        {
            Dictionary<string, bool> list = new Dictionary<string, bool>();
            foreach (Type type in correctTypes)
            {
                FieldInfo fieldInfoName = type.GetField("name");
                FieldInfo fieldInfoSimetric = type.GetField("simetric");
                list.Add(fieldInfoName.GetValue(new object()).ToString(), (bool)fieldInfoSimetric.GetValue(new object()));
            }
            return list;
        }
        private IFaca generateNewInstance(string className)
        {
            foreach (Type type in correctTypes)
            {
                if (className == type.GetField("name").GetValue(new object()).ToString())
                {
                    objIFaca = Activator.CreateInstance(type) as IFaca;
                    return objIFaca;
                   
                }

            }
            return null;
        }
        public IFaca Inicialize(string nameClass)
        {
            return generateNewInstance(nameClass);
            
        }
        public string Version
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
    }
}
