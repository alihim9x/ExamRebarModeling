using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleData
{
    public class ModelData
    {
        public static ModelData Instance { get { return SingleTon.Instance.ModelData; } }
        private List<Model.Entity.Element> ettElements;
        public List<Model.Entity.Element> EttElements
        {
            get
            {
                if(ettElements == null)
                {
                    ettElements = new List<Model.Entity.Element>();
                }
                return ettElements;
            }
            set
            {
                ettElements = value;
            }
        }
    }
}
