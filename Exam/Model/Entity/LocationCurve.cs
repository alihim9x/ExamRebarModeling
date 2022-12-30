using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class LocationCurve
    {
        public Autodesk.Revit.DB.Curve T1 { get; set; }
        public Autodesk.Revit.DB.Curve T2 { get; set; }
        public Autodesk.Revit.DB.Curve B1 { get; set; }
        public Autodesk.Revit.DB.Curve B2 { get; set; }

    }
}
