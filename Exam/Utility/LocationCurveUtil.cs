using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;

namespace Utility
{
    public static class LocationCurveUtil
    {
        private static RevitData revitData
        {
            get
            {
                return RevitData.Instance;
            }
        }
        private static ModelData modelData
        {
            get
            {
                return ModelData.Instance;
            }
        }
        public static Model.Entity.LocationCurve GetLocationCurve(this Model.Entity.Element framing, double cover, double barDiameter)
        {
            var locationCurveRebar = new Model.Entity.LocationCurve();
            var bFraming = framing.Geometry.LengthX;
            var hFraming = framing.Geometry.LengthY;
            locationCurveRebar.T1 = framing.TransformLocationCurve(cover, barDiameter);
            Autodesk.Revit.DB.Transform tfT2 = null;
            tfT2 = Autodesk.Revit.DB.Transform.CreateTranslation(framing.Geometry.BasisY * -40.0.Milimet2Feet());
            locationCurveRebar.T2 = locationCurveRebar.T1.CreateTransformed(tfT2);
            Autodesk.Revit.DB.Transform tfB1 = null;
            tfB1 = Autodesk.Revit.DB.Transform.CreateTranslation(framing.Geometry.BasisY * -(hFraming-2*(cover+barDiameter/2)).Milimet2Feet());
            locationCurveRebar.B1 = locationCurveRebar.T1.CreateTransformed(tfB1);
            Autodesk.Revit.DB.Transform tfB2 = null;
            tfB2 = Autodesk.Revit.DB.Transform.CreateTranslation(framing.Geometry.BasisY * 40.0.Milimet2Feet());
            locationCurveRebar.B2 = locationCurveRebar.B1.CreateTransformed(tfB2);
            
            //var beamEtt = framing.GetEntityElement();
            //double d18Type = revitData.RebarBarTypes.SingleOrDefault(x => x.BarDiameter == barDiameter.Milimet2Feet()).BarDiameter;
            //double coverToCenter = cover.Milimet2Feet() + d18Type / 2; //To Center of bar
            //var yPos = framing.AsValue("y Justification").ValueNumber;
            //var yOffset = framing.AsValue("y Offset Value").ValueNumber;
            //var zPos = framing.AsValue("z Justification").ValueNumber;
            //var zOffset = framing.AsValue("z Offset Value").ValueNumber;

            //Autodesk.Revit.DB.Transform tf = null;
            //switch (yPos)
            //{

            //    case 1:
            //    case 2:
            //        {
            //            if (zPos == 0)
            //            {
            //                tf = Autodesk.Revit.DB.Transform.CreateTranslation(-(coverToCenter - zOffset.Milimet2Feet()) * beamEtt.Geometry.BasisY - ((beamEtt.Geometry.LengthX / 2).Milimet2Feet() - coverToCenter) * beamEtt.Geometry.BasisX);
            //            }
            //            else if (zPos == 3)
            //            {
            //                tf = Autodesk.Revit.DB.Transform.CreateTranslation((beamEtt.Geometry.LengthY.Milimet2Feet() - coverToCenter + zOffset.Milimet2Feet()) * beamEtt.Geometry.BasisY - ((beamEtt.Geometry.LengthX / 2).Milimet2Feet() - coverToCenter) * beamEtt.Geometry.BasisX);
            //            }
            //            else
            //            {
            //                tf = Autodesk.Revit.DB.Transform.CreateTranslation(((beamEtt.Geometry.LengthY / 2).Milimet2Feet() - coverToCenter + zOffset.Milimet2Feet()) * beamEtt.Geometry.BasisY - ((beamEtt.Geometry.LengthX / 2).Milimet2Feet() - coverToCenter) * beamEtt.Geometry.BasisX);
            //            }
            //            break;
            //        }
            //    case 0:
            //        {
            //            if (zPos == 0)
            //            {
            //                tf = Autodesk.Revit.DB.Transform.CreateTranslation(-(coverToCenter - zOffset.Milimet2Feet()) * beamEtt.Geometry.BasisY - (beamEtt.Geometry.LengthX.Milimet2Feet() - coverToCenter) * beamEtt.Geometry.BasisX);
            //            }
            //            else if (zPos == 3)
            //            {
            //                tf = Autodesk.Revit.DB.Transform.CreateTranslation(-(+beamEtt.Geometry.LengthX.Milimet2Feet() - coverToCenter) * beamEtt.Geometry.BasisX + ((beamEtt.Geometry.LengthY).Milimet2Feet() - coverToCenter + zOffset.Milimet2Feet()) * beamEtt.Geometry.BasisY);
            //            }
            //            else
            //            {
            //                tf = Autodesk.Revit.DB.Transform.CreateTranslation(-(+beamEtt.Geometry.LengthX.Milimet2Feet() - coverToCenter) * beamEtt.Geometry.BasisX + ((beamEtt.Geometry.LengthY / 2).Milimet2Feet() - coverToCenter + zOffset.Milimet2Feet()) * beamEtt.Geometry.BasisY);
            //            }
            //            break;
            //        }
            //    default:
            //        {
            //            if (zPos == 0)
            //            {
            //                tf = Autodesk.Revit.DB.Transform.CreateTranslation((-coverToCenter + zOffset.Milimet2Feet()) * beamEtt.Geometry.BasisY + coverToCenter * beamEtt.Geometry.BasisX);
            //            }
            //            else if (zPos == 3)
            //            {
            //                tf = Autodesk.Revit.DB.Transform.CreateTranslation(coverToCenter * beamEtt.Geometry.BasisX + (beamEtt.Geometry.LengthY.Milimet2Feet() - coverToCenter + zOffset.Milimet2Feet()) * beamEtt.Geometry.BasisY);
            //            }
            //            else
            //            {
            //                tf = Autodesk.Revit.DB.Transform.CreateTranslation(coverToCenter * beamEtt.Geometry.BasisX + ((beamEtt.Geometry.LengthY / 2).Milimet2Feet() - coverToCenter + zOffset.Milimet2Feet()) * beamEtt.Geometry.BasisY);
            //            }
            //            break;
            //        }

            //}
            //var locationCurve = (framing.Location as Autodesk.Revit.DB.LocationCurve).Curve.CreateTransformed(tf);

            return locationCurveRebar;

        }
    }
}
