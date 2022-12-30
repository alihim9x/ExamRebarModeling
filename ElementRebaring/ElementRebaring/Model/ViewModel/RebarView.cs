using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using SingleData;
namespace Model.ViewModel
{
    public class RebarView : NotifyClass
    {
        public Model.Entity.Rebar Rebar { get; set; }
        public List<Model.Entity.RebarLayer> RebarLayers
        {
            get
            {
                return ModelData.Instance.RebarLayers;
            }
        }
        public List<Model.Entity.RebarFunction> RebarFunctions
        {
            get
            {
                return ModelData.Instance.RebarFunctions;
            }
        }
        public List<Autodesk.Revit.DB.Structure.RebarBarType> RebarBarTypes
        {
            get
            {
                return ModelData.Instance.RebarBarTypes.ToList();
            }
        }

            
           
    }
}
