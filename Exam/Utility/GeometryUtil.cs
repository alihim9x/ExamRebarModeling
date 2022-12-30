using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Utility
{
    public static class GeometryUtil
    {
        public static Model.Entity.Geometry GetGeometry (this Model.Entity.Element ettElem, double cover, double diameter)
        {
            var geometry = new Model.Entity.Geometry();
            var revitElem = ettElem.RevitElement;

            var typeElem = ettElem.ElementType;
            var revitElemFI = revitElem as Autodesk.Revit.DB.FamilyInstance;
            var transform = revitElemFI.GetTotalTransform();
            
            switch (typeElem)
            {
                case Model.Entity.ElementType.Framing:
                    geometry.Origin = transform.Origin;
                    geometry.BasisX = transform.BasisY;
                    geometry.BasisY = transform.BasisZ;
                    geometry.BasisZ = transform.BasisX;
                    geometry.LengthX = revitElemFI.Symbol.AsValue("b").ValueNumber;
                    geometry.LengthY = revitElemFI.Symbol.AsValue("h").ValueNumber;
                    geometry.LengthZ = revitElemFI.AsValue("Cut Length").ValueNumber;
                    geometry.OriginForRebar = geometry.Origin + (geometry.BasisZ * (geometry.LengthZ.Milimet2Feet() / 2 - cover.Milimet2Feet())
                        + (geometry.BasisX * (geometry.LengthX.Milimet2Feet() / 2 - cover.Milimet2Feet())
                        + (geometry.BasisY * (geometry.LengthY.Milimet2Feet() / 2 - cover.Milimet2Feet()))));
                    break;
                default:
                    throw new Model.Exception.CaseNotCheckException();
            }
            return geometry;
        }
       
    }
}
