using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class Rebar
    {
        public Model.Entity.Element HostElement { get; set; }
        public RebarShapeType RebarShapeType { get; set; }
        public RebarDirection RebarDirection { get; set; }

        public Autodesk.Revit.DB.XYZ Location { get; set; }
       
        public RebarLayerType RebarLayerType { get; set; }
        public int Quantity { get; set; }
        public int Index { get; set; }
        public double Length { get; set; }
        public List<Autodesk.Revit.DB.Curve> Curves { get; set; }
    }
}
