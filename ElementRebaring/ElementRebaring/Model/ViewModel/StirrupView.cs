using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using SingleData;
namespace Model.ViewModel
{
    public class StirrupView : NotifyClass
    {
        public Model.Entity.Stirrup Stirrup { get; set; }
        public List<Model.Entity.StirrupFunction> StirrupFunctions
        {
            get
            {
                return ModelData.Instance.StirrupFunctions;
            }
        }
        public List<Model.Entity.StirrupPosition> StirrupPositions
        {
            get
            {
                return ModelData.Instance.StirrupPositions;
            }
        }       
        public List<Autodesk.Revit.DB.Structure.RebarBarType> RebarBarTypes
        {
            get
            {
                return ModelData.Instance.RebarBarTypes.ToList();
            }
        }
        private double spacingMM;
        public double SpacingMM
        {
            get
            {
                return spacingMM;
            }
            set
            {
                spacingMM = value;
                OnPropertyChanged();
                Stirrup.Spacing = spacingMM.milimeter2Feet();
            }
        }

            
           
    }
}
