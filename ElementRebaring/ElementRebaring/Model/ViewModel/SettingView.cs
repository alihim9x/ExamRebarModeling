using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;
using Utility;

namespace Model.ViewModel
{
    public class SettingView : NotifyClass
    {
        public Model.Entity.Setting Setting { get; set; }
        private double concreteCoverMM;
        public double ConcreteCoverMM
        {
            get
            {
                if(concreteCoverMM == 0)
                {
                    concreteCoverMM = Setting.ConcreteCover.feet2Milimeter();
                }
                return concreteCoverMM;
            }
            set
            {
                concreteCoverMM = value;
                OnPropertyChanged();
                Setting.ConcreteCover = concreteCoverMM.milimeter2Feet();
                
            }
        }
        private double layerDistanceMM;
        public double LayerDistanceMM
        {
            get
            {
                if(layerDistanceMM == 0)
                {
                    layerDistanceMM = Setting.LayerDistance.feet2Milimeter();
                }
                return layerDistanceMM;
            }
            set
            {
                layerDistanceMM = value;
                OnPropertyChanged();
                Setting.LayerDistance = layerDistanceMM.milimeter2Feet();
            }
        }
        private double topReinforcedLayerLengthRatio;
        public double TopReinforcedLayerLengthRatio
        {
            get
            {
                if(topReinforcedLayerLengthRatio == 0)
                {
                    topReinforcedLayerLengthRatio = Setting.TopReinforcedLayerLengthRatio;
                }
                return topReinforcedLayerLengthRatio;
            }
            set
            {
                topReinforcedLayerLengthRatio = value;
                OnPropertyChanged();
                Setting.TopReinforcedLayerLengthRatio = (int)topReinforcedLayerLengthRatio;
            }
        }
        private double bottomReinforcedLayerLengthRatio;
        public double BottomReinforcedLayerLengthRatio
        {
            get
            {
                if(bottomReinforcedLayerLengthRatio == 0)
                {
                    bottomReinforcedLayerLengthRatio = Setting.BottomReinforcedLayerLengthRatio;
                }
                return bottomReinforcedLayerLengthRatio;
            }
            set
            {
                bottomReinforcedLayerLengthRatio = value;
                OnPropertyChanged();
                Setting.BottomReinforcedLayerLengthRatio = (int)bottomReinforcedLayerLengthRatio;
            }
        }
        private double stirrupArrayLengthRatio;
        public double StirrupArrayLengthRatio
        { 
            get
            {
                if(stirrupArrayLengthRatio == 0)
                {
                    stirrupArrayLengthRatio = Setting.BottomReinforcedLayerLengthRatio;
                }
                return stirrupArrayLengthRatio;
            }
            set
            {
                stirrupArrayLengthRatio = value;
                OnPropertyChanged();
                Setting.StirrupArrayLengthRatio = (int)stirrupArrayLengthRatio;
            }
        }
        private double offsetStirrupFromEdge;
        public double OffsetStirrupFromEdge
        {
            get
            {
                if(offsetStirrupFromEdge == 0)
                {
                    offsetStirrupFromEdge = Setting.OffsetStirrupFromEdge;
                }
                return offsetStirrupFromEdge;
            }
            set
            {
                offsetStirrupFromEdge = value;
                OnPropertyChanged();
                Setting.OffsetStirrupFromEdge = offsetStirrupFromEdge;
            }
        }
        private double offsetStirrupFromEdgeMM;
        public double OffsetStirrupFromEdgeMM
        {
            get
            {
                if(offsetStirrupFromEdgeMM == 0)
                {
                    offsetStirrupFromEdgeMM = Setting.OffsetStirrupFromEdge.feet2Milimeter();
                }
                return offsetStirrupFromEdgeMM;
            }
            set
            {
                offsetStirrupFromEdgeMM = value;
                OnPropertyChanged();
                Setting.OffsetStirrupFromEdge = offsetStirrupFromEdgeMM.milimeter2Feet();
            }
        }
        
    }
}
