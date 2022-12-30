using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;
using Utility;
using Autodesk.Revit.DB.Structure;

namespace Utility
{
    public static class ElementUtil
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
        public static Autodesk.Revit.DB.Element GetElement(this Autodesk.Revit.DB.ElementId elemId)
        {
            return revitData.Document.GetElement(elemId);
        }
        public static Autodesk.Revit.DB.Element GetElement(this Autodesk.Revit.DB.Reference elemRef)
        {
            return revitData.Document.GetElement(elemRef);
        }
        public static Model.Entity.Element GetEntityElement(this Autodesk.Revit.DB.Element elem )
        {
            var ettElem = modelData.EttElements.SingleOrDefault(x => x.RevitElement.UniqueId == elem.UniqueId);
            if(ettElem == null)
            {
                ettElem = new Model.Entity.Element
                {
                    RevitElement = elem
                };
                modelData.EttElements.Add(ettElem);
            }
            return ettElem;
        }
        public static Model.Entity.ElementType GetElementType (this Model.Entity.Element ettElem)
        {
            var revitElem = ettElem.RevitElement;
            var cate = revitElem.Category;
            if (revitElem is Autodesk.Revit.DB.Floor)
                return Model.Entity.ElementType.Floor;
            if (revitElem is Autodesk.Revit.DB.Wall)
                return Model.Entity.ElementType.Wall;
            if (cate.Id.IntegerValue == (int)Autodesk.Revit.DB.BuiltInCategory.OST_StructuralFraming)
                return Model.Entity.ElementType.Framing;
            if (cate.Id.IntegerValue == (int)Autodesk.Revit.DB.BuiltInCategory.OST_StructuralColumns)
                return Model.Entity.ElementType.Column;
            return Model.Entity.ElementType.Undefined;
        }
        public static Autodesk.Revit.DB.Curve TransformLocationCurve(this Model.Entity.Element framingEtt, double cover, double barDiameter)
        {
           

           
            var framingRevit = framingEtt.RevitElement;
            double d18Type = barDiameter.Milimet2Feet();
            double coverToCenter = cover.Milimet2Feet() + d18Type / 2; //To Center of bar
            var yPos = framingRevit.AsValue("y Justification").ValueNumber;
            var yOffset = framingRevit.AsValue("y Offset Value").ValueNumber;
            var zPos = framingRevit.AsValue("z Justification").ValueNumber;
            var zOffset = framingRevit.AsValue("z Offset Value").ValueNumber;

            Autodesk.Revit.DB.Transform tf = null;
            switch (yPos)
            {

                case 1:
                case 2:
                    {
                        if (zPos == 0)
                        {
                            tf = Autodesk.Revit.DB.Transform.CreateTranslation(-(coverToCenter - zOffset.Milimet2Feet()) * framingEtt.Geometry.BasisY - ((framingEtt.Geometry.LengthX / 2).Milimet2Feet() - coverToCenter) * framingEtt.Geometry.BasisX);
                        }
                        else if (zPos == 3)
                        {
                            tf = Autodesk.Revit.DB.Transform.CreateTranslation((framingEtt.Geometry.LengthY.Milimet2Feet() - coverToCenter + zOffset.Milimet2Feet()) * framingEtt.Geometry.BasisY - ((framingEtt.Geometry.LengthX / 2).Milimet2Feet() - coverToCenter) * framingEtt.Geometry.BasisX);
                        }
                        else
                        {
                            tf = Autodesk.Revit.DB.Transform.CreateTranslation(((framingEtt.Geometry.LengthY / 2).Milimet2Feet() - coverToCenter + zOffset.Milimet2Feet()) * framingEtt.Geometry.BasisY - ((framingEtt.Geometry.LengthX / 2).Milimet2Feet() - coverToCenter) * framingEtt.Geometry.BasisX);
                        }
                        break;
                    }
                case 0:
                    {
                        if (zPos == 0)
                        {
                            tf = Autodesk.Revit.DB.Transform.CreateTranslation(-(coverToCenter - zOffset.Milimet2Feet()) * framingEtt.Geometry.BasisY - (framingEtt.Geometry.LengthX.Milimet2Feet() - coverToCenter) * framingEtt.Geometry.BasisX);
                        }
                        else if (zPos == 3)
                        {
                            tf = Autodesk.Revit.DB.Transform.CreateTranslation(-(+framingEtt.Geometry.LengthX.Milimet2Feet() - coverToCenter) * framingEtt.Geometry.BasisX + ((framingEtt.Geometry.LengthY).Milimet2Feet() - coverToCenter + zOffset.Milimet2Feet()) * framingEtt.Geometry.BasisY);
                        }
                        else
                        {
                            tf = Autodesk.Revit.DB.Transform.CreateTranslation(-(+framingEtt.Geometry.LengthX.Milimet2Feet() - coverToCenter) * framingEtt.Geometry.BasisX + ((framingEtt.Geometry.LengthY / 2).Milimet2Feet() - coverToCenter + zOffset.Milimet2Feet()) * framingEtt.Geometry.BasisY);
                        }
                        break;
                    }
                default:
                    {
                        if (zPos == 0)
                        {
                            tf = Autodesk.Revit.DB.Transform.CreateTranslation((-coverToCenter + zOffset.Milimet2Feet()) * framingEtt.Geometry.BasisY + coverToCenter * framingEtt.Geometry.BasisX);
                        }
                        else if (zPos == 3)
                        {
                            tf = Autodesk.Revit.DB.Transform.CreateTranslation(coverToCenter * framingEtt.Geometry.BasisX + (framingEtt.Geometry.LengthY.Milimet2Feet() - coverToCenter + zOffset.Milimet2Feet()) * framingEtt.Geometry.BasisY);
                        }
                        else
                        {
                            tf = Autodesk.Revit.DB.Transform.CreateTranslation(coverToCenter * framingEtt.Geometry.BasisX + ((framingEtt.Geometry.LengthY / 2).Milimet2Feet() - coverToCenter + zOffset.Milimet2Feet()) * framingEtt.Geometry.BasisY);
                        }
                        break;
                    }

            }
            var locationCurve = (framingRevit.Location as Autodesk.Revit.DB.LocationCurve).Curve.CreateTransformed(tf);

            return locationCurve;

        }
       
        

    }
}
