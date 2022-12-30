using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System.Collections.Generic;
using Utility;
using SingleData;

namespace Model.Entity
{
    public class Rebar : NotifyClass
    {
        public Element Element { get; set; }

        public List<RebarLayer> RebarLayers
        {
            get
            {
                return ModelData.Instance.RebarLayers;
            }
        }

        private RebarLayer rebarLayer;
        public RebarLayer RebarLayer
        {
            get
            {
                return rebarLayer;
            }
            set
            {
                if (rebarLayer == value) return;
                rebarLayer = value; OnPropertyChanged();
            }
        }

        private int indexInLayer;
        public int IndexInLayer
        {
            get
            {
                return indexInLayer;
            }
            set
            {
                if (indexInLayer == value) return;
                indexInLayer = value; OnPropertyChanged();
            }
        }

        private int layerIndex;
        public int LayerIndex
        {
            get
            {
                return layerIndex;
            }
            set
            {
                if (layerIndex == value) return;
                layerIndex = value; OnPropertyChanged();
            }
        }

        public List<RebarFunction> RebarFunctions
        {
            get
            {
                return ModelData.Instance.RebarFunctions;
            }
        }

        private RebarFunction rebarFunction;
        public RebarFunction RebarFunction
        {
            get
            {
                return rebarFunction;
            }
            set
            {
                if (rebarFunction == value) return;
                rebarFunction = value; OnPropertyChanged();
            }
        }

        private int quantity;
        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                if (quantity == value) return;
                quantity = value; OnPropertyChanged();
            }
        }

        public IEnumerable<RebarBarType> RebarBarTypes
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

        private bool hasHook;
        public bool HasHook
        {
            get
            {
                return hasHook;
            }
            set
            {
                if (hasHook == value) return;
                hasHook = value; OnPropertyChanged();
            }
        }
       
        private double spacing;
        public double Spacing
        {
            get
            {
                if (spacing == 0)
                {
                    spacing = this.GetSpacing();
                }
                return spacing;
            }
        }
        private double arrayLength;
        public double ArrayLength
        {
            get
            {
                if(arrayLength == 0)
                {
                    arrayLength = spacing * (quantity);
                }
                return arrayLength;
            }
            
        }


        

        private List<Autodesk.Revit.DB.Curve> curves;
        public List<Autodesk.Revit.DB.Curve> Curves
        {
            get
            {
                if (curves == null)
                {
                    curves = this.GetCurves();
                }
                return curves;
            }
        }
    }
}
