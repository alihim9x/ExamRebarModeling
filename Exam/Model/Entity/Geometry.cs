using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Geometry
    {
        public Autodesk.Revit.DB.XYZ Origin { get; set; }
        public Autodesk.Revit.DB.XYZ OriginForRebar { get; set; }
        public Autodesk.Revit.DB.XYZ BasisX { get; set; }
        public Autodesk.Revit.DB.XYZ BasisY { get; set; }
        public Autodesk.Revit.DB.XYZ BasisZ { get; set; }
        public double LengthX { get; set; }
        public double LengthY { get; set; }
        public double LengthZ { get; set; }
    }
}
