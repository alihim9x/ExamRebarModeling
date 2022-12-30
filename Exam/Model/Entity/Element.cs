using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class Element
    {
        public Autodesk.Revit.DB.Element RevitElement { get; set; }
        
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
        private Geometry geometry;
        public Geometry Geometry
        {
            get
            {
                if(geometry == null)
                {
                    geometry = this.GetGeometry(25,18);
                }
                return geometry;
            }
        }
        public Model.Entity.Setting Setting { get; set; }
        private LocationCurve locationCurve;
        public LocationCurve LocationCurve
        {
            get
            {
                if(locationCurve == null)
                {
                    locationCurve = this.GetLocationCurve(25,18);
                }
                return locationCurve;
            }
        }

        

    }
}
