using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using SingleData;
using Autodesk.Revit.DB.Structure;

namespace Model.Entity
{
    public class Element : NotifyClass
    {
        public Autodesk.Revit.DB.Element RevitElement { get; set; }
        private Geometry geometry;
        public Geometry Geometry
        {
            get
            {
                if (geometry == null)
                {
                    geometry = this.GetGeometry();
                }
                return geometry;
            }
        }
        private Identify identify;
        public Identify Identify
        {
            get
            {
                if(identify == null)
                {
                    identify = this.GetIdentify();
                }
                return identify;
            }
            
        }
        private ElementType? elementType;
        public ElementType? ElementType
        {
            get
            {
                if(elementType == null)
                {
                    elementType = this.GetElementType();
                }
                return elementType;
            }
        }

        // Không nên dùng RebarCoverType để set bêtông bảo vệ vì phải trừ hao rất nhiều (thép đai, thép chủ ...) nên thép dễ bị xê dịch khi vẽ
        // ở đây xài modelData.Setting.ConcreteCover là được rồi
        public IEnumerable<RebarCoverType> RebarCoverTypes
        {
            get
            {
                return ModelData.Instance.RebarCoverTypes;
            }
        }
        private RebarCoverType rebarCoverType;
        public RebarCoverType RebarCoverType
        {
            get
            {
                return rebarCoverType;
            }
            set
            {
                if (rebarCoverType == null)
                rebarCoverType = value; OnPropertyChanged();
            }
        }
       
        public List<Rebar> Rebars { get; set; } = new List<Rebar>();
        public List<Stirrup> Stirrups { get; set; } = new List<Stirrup>();
    }
}
