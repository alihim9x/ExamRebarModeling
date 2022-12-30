using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class Setting : NotifyClass
    {
        // Lớp bêtông bảo vệ
        private double concreteCover;
        public double ConcreteCover
        {
            get
            {
                if (concreteCover == 0)
                {
                    concreteCover = 25.0.milimeter2Feet();
                }
                return concreteCover;
            }
            set
            {
                if (concreteCover == value) return;
                concreteCover = value; OnPropertyChanged();
            }
        }
        // Khoảng cách giữa 2 lớp thép
        private double layerDistance;
        public double LayerDistance
        {
            get
            {
                if (layerDistance == 0)
                {
                    layerDistance = 30.0.milimeter2Feet();
                }
                return layerDistance;
            }
            set
            {
                if (layerDistance == value) return;
                layerDistance = value; OnPropertyChanged();
            }
        }
        private int topReinforcedLayerLengthRatio;
        public int TopReinforcedLayerLengthRatio
        {
            get
            {
                if (topReinforcedLayerLengthRatio == 0)
                {
                    topReinforcedLayerLengthRatio = 4;
                }
                return topReinforcedLayerLengthRatio;
            }
            set
            {
                if (topReinforcedLayerLengthRatio == value) return;
                topReinforcedLayerLengthRatio = value; OnPropertyChanged();
            }
        }
        private int stirrupArrayLengthRatio;
        public int StirrupArrayLengthRatio
        {
            get
            {
                if(stirrupArrayLengthRatio == 0)
                {
                    stirrupArrayLengthRatio = 4;
                }
                return stirrupArrayLengthRatio;
            }
            set
            {
                if (stirrupArrayLengthRatio == value) return;
                stirrupArrayLengthRatio = value; OnPropertyChanged();
            }
        }

        private int bottomReinforcedLayerLengthRatio;
        public int BottomReinforcedLayerLengthRatio
        {
            get
            {
                if (bottomReinforcedLayerLengthRatio == 0)
                {
                    bottomReinforcedLayerLengthRatio = 5;
                }
                return bottomReinforcedLayerLengthRatio;
            }
            set
            {
                if (bottomReinforcedLayerLengthRatio == value) return;
                bottomReinforcedLayerLengthRatio = value; OnPropertyChanged();
            }
        }

        private int stirrupPositionRatio;
        public int StirrupPositionRatio
        {
            get
            {
                if (stirrupPositionRatio == 0)
                {
                    stirrupPositionRatio = 4;
                }
                return stirrupPositionRatio;
            }
            set
            {
                if (stirrupPositionRatio == value) return;
                stirrupPositionRatio = value; OnPropertyChanged();
            }
        }
        private double offsetStirrupFromEdge;
        public double OffsetStirrupFromEdge
        {
            get
            {
                if(offsetStirrupFromEdge == 0)
                {
                    offsetStirrupFromEdge = 50.0.milimeter2Feet();
                }
                return offsetStirrupFromEdge;
            }
            set
            {
                if (offsetStirrupFromEdge == value) return;
                offsetStirrupFromEdge = value;
                OnPropertyChanged();
            }
        }
    }
}
