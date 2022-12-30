using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Utility;
using SingleData;

namespace Model.Entity
{
    public class Stirrup : NotifyClass
    {
        public Element Element { get; set; }

        private int sectionIndex;
        public int SectionIndex
        {
            get
            {
                return sectionIndex;
            }
            set
            {
                if (sectionIndex == value) return;
                sectionIndex = value; OnPropertyChanged();
            }
        }

        public List<StirrupFunction> StirrupFunctions
        {
            get
            {
                return ModelData.Instance.StirrupFunctions;
            }
        }

        private StirrupFunction stirrupFunction;
        public StirrupFunction StirrupFunction
        {
            get
            {
                return stirrupFunction;
            }
            set
            {
                if (stirrupFunction == value) return;
                stirrupFunction = value; OnPropertyChanged();
            }
        }

        public List<StirrupPosition> StirrupPositions
        {
            get
            {
                return ModelData.Instance.StirrupPositions;
            }
        }

        private StirrupPosition stirrupPosition;
        public StirrupPosition StirrupPosition
        {
            get
            {
                return stirrupPosition;
            }
            set
            {
                if (stirrupPosition == value) return;
                stirrupPosition = value; OnPropertyChanged();
            }
        }

        private double spacing;
        public double Spacing
        {
            get
            {
                return spacing;
            }
            set
            {
                spacing = value;
                OnPropertyChanged();
            }
        }

        private IEnumerable<RebarBarType> RebarBarTypes
        {
            get
            {
                return ModelData.Instance.RebarBarTypes;
            }
        }

        private RebarBarType rebarBarType;
        public RebarBarType RebarBarType
        {
            get
            {
                return rebarBarType;
            }
            set
            {
                if (rebarBarType == value) return;
                rebarBarType = value; OnPropertyChanged();
            }
        }

        private IEnumerable<RebarShape> RebarShapes
        {
            get
            {
                return ModelData.Instance.RebarShapes;
            }
        }
        private RebarShape rebarShape;
        public RebarShape RebarShape
        {
            get
            {
                return rebarShape;
            }
            set
            {
                if (rebarShape == value) return;
                rebarShape = value; OnPropertyChanged();
            }
        }
        private Autodesk.Revit.DB.XYZ origin;
        public Autodesk.Revit.DB.XYZ Origin
        {
           
            get
            {
                if(origin == null)
                {
                    origin = this.GetOrigin();
                }
                return origin;
            }
        }
        private double offsetZ;
        public double OffsetZ
        {
            get
            {
                if(offsetZ == 0)
                {
                    offsetZ = this.GetOffsetZ();
                }
                return offsetZ;
            }
        }
    }
}
