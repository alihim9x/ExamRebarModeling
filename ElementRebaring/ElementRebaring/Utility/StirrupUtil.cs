using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;


namespace Utility
{
    public static class StirrupUtil
    {
        private static ModelData modelData
        {
            get
            {
                return ModelData.Instance;
            }
        }
        public static double GetOffsetZ(this Model.Entity.Stirrup stirrup)
        {
            var geoHost = stirrup.Element.Geometry;
            var lengthHost = geoHost.LengthZ;
            var setting = ModelData.Instance.Setting;
            var cover = setting.ConcreteCover;
            var stirrupArrayLengthRatio = setting.StirrupArrayLengthRatio;
            var offsetStirrupFromEdge = setting.OffsetStirrupFromEdge;
            var stirrupDiameter = stirrup.RebarBarType.BarDiameter;
            double offsetZ = 0;
            switch(stirrup.StirrupPosition)
            {
                case Model.Entity.StirrupPosition.Left:
                case Model.Entity.StirrupPosition.Whole:
                    offsetZ = offsetStirrupFromEdge + stirrupDiameter / 2 ;
                    break;
                case Model.Entity.StirrupPosition.Middle:
                    offsetZ = lengthHost / stirrupArrayLengthRatio + stirrupDiameter / 2;
                    break;
                case Model.Entity.StirrupPosition.Right:
                    offsetZ = lengthHost - lengthHost / stirrupArrayLengthRatio + stirrupDiameter / 2 ;
                    break;
            }
            return offsetZ;

        }
        public static Autodesk.Revit.DB.XYZ GetOrigin(this Model.Entity.Stirrup stirrup)
        {
            var stirrupDiameter = stirrup.RebarBarType.BarDiameter;
            var setting = modelData.Setting;
            var cover = setting.ConcreteCover;
            var offsetStirrupFromEdge = setting.OffsetStirrupFromEdge;
            var sectionIndex = stirrup.SectionIndex;
            var host = stirrup.Element;
            var geoHost = host.Geometry;
           
            
            var vecZ = geoHost.BasisZ;
            var vecX = geoHost.BasisX;
            var vecY = geoHost.BasisY;
            var originHost = geoHost.Origin;
            var lengthHost = geoHost.LengthZ;
            var hHost = geoHost.LengthY;
            var bHost = geoHost.LengthX;

            var rebarsHost = host.Rebars;
            var topRebars = rebarsHost.Where(x => x.RebarLayer == Model.Entity.RebarLayer.Top);
            var botRebars = rebarsHost.Where(x => x.RebarLayer == Model.Entity.RebarLayer.Bottom);
            var noTopRebars = 0;
            var noBotRebars = 0;
            foreach (var item in topRebars)
            {
                noTopRebars += item.Quantity;
            }
            var spacingTopRebars = (bHost - 2 * (cover + stirrupDiameter + (topRebars.First().RebarBarType.BarDiameter /2) )) / (noTopRebars - 1);


            double offsetZ = 0;
            switch(stirrup.StirrupPosition)
            {
                case Model.Entity.StirrupPosition.Left:
                case Model.Entity.StirrupPosition.Whole:
                    offsetZ = cover + offsetStirrupFromEdge;
                    break;
                case Model.Entity.StirrupPosition.Middle:
                    offsetZ = lengthHost / 4;
                    break;
                case Model.Entity.StirrupPosition.Right:
                    offsetZ = lengthHost - lengthHost / 4;
                    break;
            }

            double offsetY = cover + stirrupDiameter / 2;
            double offsetX = cover + stirrupDiameter / 2;
            switch (stirrup.StirrupFunction)
            {
                case Model.Entity.StirrupFunction.C_Type:
                    offsetX = cover + (spacingTopRebars * (sectionIndex - 1));
                    break;
            }
            var originStirrup = originHost + offsetZ * vecZ + offsetX * vecX + offsetY * vecY;
            return originStirrup;
        }
        public static double GetSpacing (this Model.Entity.Stirrup stirrup)
        {
            var setting = ModelData.Instance.Setting;
            var offsetStirrupFromEdge = setting.OffsetStirrupFromEdge;
            var stirrupRatio = setting.StirrupArrayLengthRatio;
            var geoHost = stirrup.Element.Geometry;
            var lengthHost = geoHost.LengthZ;
            double spacing = 0;
            switch(stirrup.StirrupPosition)
            {
                case Model.Entity.StirrupPosition.Left:
                case Model.Entity.StirrupPosition.Right:
                    spacing = lengthHost / stirrupRatio;
                    break;
                case Model.Entity.StirrupPosition.Middle:
                    spacing = lengthHost - (2 * (lengthHost / stirrupRatio + offsetStirrupFromEdge));
                    break;
                case Model.Entity.StirrupPosition.Whole:
                    spacing = lengthHost - (2 * offsetStirrupFromEdge);
                    break;
            }
            return spacing;
        }
        public static Autodesk.Revit.DB.Structure.Rebar CreateStirrup(this Model.Entity.Stirrup stirrup)
        {
            var activeView = RevitData.Instance.ActiveView;
            var doc = RevitData.Instance.Document;
            var setting = modelData.Setting;
            var cover = setting.ConcreteCover;
            var offsetStirrupFromEdge = setting.OffsetStirrupFromEdge;
            var stirrupArrayLengthRatio = setting.StirrupArrayLengthRatio;
            var stirrupDiameter = stirrup.RebarBarType.BarDiameter;
            var hostEttElem = stirrup.Element;
            var hostRvElem = hostEttElem.RevitElement;           
            var rebarFunc = stirrup.StirrupFunction;
            var rebarBarType = stirrup.RebarBarType;
            var originStirrup = stirrup.Origin;
            var geoHost = stirrup.Element.Geometry;
            var bHost = geoHost.LengthX;
            var hHost = geoHost.LengthY;
            var lengthHost = geoHost.LengthZ;
            
            
            // b d cạnh H
            // c e cạnh B
            double bStirrup = 0;
            double cStirrup = 0;
            double dStirrup = 0;
            double eStirrup = 0;
            //Autodesk.Revit.DB.XYZ vecX = null;
            //Autodesk.Revit.DB.XYZ vecY = null;
            switch(rebarFunc)
            {
                case Model.Entity.StirrupFunction.Closed_Type:
                    stirrup.RebarShape = modelData.RebarShapes.Single(x => x.Name == "M_T2");
                    bStirrup = hHost - (2 * cover);
                    dStirrup = bStirrup;
                    cStirrup = bHost - (2 * cover);
                    eStirrup = cStirrup;
                    //vecX = geoHost.BasisY;
                    //vecY = geoHost.BasisX;
                    break;
                case Model.Entity.StirrupFunction.C_Type:
                    stirrup.RebarShape = modelData.RebarShapes.Single(x => x.Name == "M_01");
                    bStirrup = hHost - (2 * cover);
                    //vecX = geoHost.BasisX;
                    //vecY = geoHost.BasisY;
                    break;
            }
            var rebarShape = stirrup.RebarShape;
            var vecX = geoHost.BasisX;
            var vecY = geoHost.BasisY;
            var vecZ = geoHost.BasisZ;

            var vecX1 = geoHost.BasisY;
            var vecY1 = geoHost.BasisX;
            var vecZ1 = geoHost.BasisZ;

            Autodesk.Revit.DB.Structure.Rebar newStirrup = Autodesk.Revit.DB.Structure.Rebar.CreateFromRebarShape(doc, rebarShape, rebarBarType, hostRvElem,
               originStirrup,vecX1, vecY1);
            newStirrup.SetValue("B", bStirrup);
            newStirrup.SetValue("D", dStirrup);
            newStirrup.SetValue("C", cStirrup);
            newStirrup.SetValue("E", eStirrup);

            doc.Regenerate();
            var bBoxNewStirrup = newStirrup.get_BoundingBox(null);
            var min = bBoxNewStirrup.Min;
            var max = bBoxNewStirrup.Max;
            var centroidbBox = (min + max) / 2;

            var offsetZ = stirrup.OffsetZ;
            var centroidSectionRevitElem = geoHost.Origin + (vecX * bHost / 2) + (vecY * hHost / 2) + (vecZ1 * offsetZ);

            Autodesk.Revit.DB.ElementTransformUtils.MoveElement(doc, newStirrup.Id, -centroidbBox + centroidSectionRevitElem + vecX * stirrupDiameter/4 + vecY * stirrupDiameter/4);
            Autodesk.Revit.DB.Structure.RebarShapeDrivenAccessor rsda = newStirrup.GetShapeDrivenAccessor();
            double arrayLength = 0;
            switch(stirrup.StirrupPosition)
            {
                case Model.Entity.StirrupPosition.Left:
                    arrayLength = lengthHost / stirrupArrayLengthRatio - offsetStirrupFromEdge;
                    rsda.SetLayoutAsMaximumSpacing(stirrup.Spacing, arrayLength, false, true, false);
                    break;
                case Model.Entity.StirrupPosition.Right:
                    arrayLength = lengthHost / stirrupArrayLengthRatio - offsetStirrupFromEdge;
                    rsda.SetLayoutAsMaximumSpacing(stirrup.Spacing, arrayLength, false, true, true);
                    break;
                case Model.Entity.StirrupPosition.Middle:
                    arrayLength = lengthHost - (2*(lengthHost / stirrupArrayLengthRatio));
                    rsda.SetLayoutAsMaximumSpacing(stirrup.Spacing, arrayLength, false, true, false);
                    break;
                case Model.Entity.StirrupPosition.Whole:
                    arrayLength = lengthHost - 2 * (offsetStirrupFromEdge);
                    rsda.SetLayoutAsMaximumSpacing(stirrup.Spacing, arrayLength, false, true, true);
                    break;
            }
            return null;

        }
    }
}
