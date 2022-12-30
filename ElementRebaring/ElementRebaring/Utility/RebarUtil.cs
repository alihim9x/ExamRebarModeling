using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;

namespace Utility
{
    public static class RebarUtil
    {
        private static ModelData modelData
        {
            get
            {
                return ModelData.Instance;
            }
        }
        // Không cần phương thức này, ở đây là nên xử lý Origin Point của Rebar, tham khảo phương thức GetMainCurve
        public static Autodesk.Revit.DB.Curve TransformCurve(this Autodesk.Revit.DB.Curve originCurve, Model.Entity.Rebar rebar)
        {
            var ettElem = rebar.Element;
            var geoEttElem = ettElem.Geometry;
            var originEtt = geoEttElem.Origin;
            var cover = ettElem.RebarCoverType.CoverDistance;
            //var cover = 25.0.milimeter2Feet();
            var h = geoEttElem.LengthY;
            var lengthElem = geoEttElem.LengthZ;
            var barDiameter = rebar.RebarBarType.BarDiameter;
            Autodesk.Revit.DB.Transform tf = null;
            switch (rebar.RebarLayer)
            {
                case Model.Entity.RebarLayer.Bottom:
                    if (rebar.LayerIndex == 1)
                    {
                        tf = Autodesk.Revit.DB.Transform.CreateTranslation(((cover + barDiameter / 2) * geoEttElem.BasisY) - ((cover + barDiameter / 2) * geoEttElem.BasisX));
                    }
                    else if (rebar.LayerIndex == 2)
                    {
                        tf = Autodesk.Revit.DB.Transform.CreateTranslation(((cover + barDiameter / 2 + 30.0.milimeter2Feet()) * geoEttElem.BasisY) - ((cover + barDiameter / 2) * geoEttElem.BasisX));
                    }
                    break;
                case Model.Entity.RebarLayer.Middle:
                    tf = Autodesk.Revit.DB.Transform.CreateTranslation((h / 2 * geoEttElem.BasisY) - ((cover + barDiameter / 2) * geoEttElem.BasisX));
                    break;
                case Model.Entity.RebarLayer.Top:
                    if (rebar.LayerIndex == 1)
                    {
                        tf = Autodesk.Revit.DB.Transform.CreateTranslation(((h - cover - barDiameter / 2) * geoEttElem.BasisY) - ((cover + barDiameter / 2) * geoEttElem.BasisX));
                    }
                    else if (rebar.LayerIndex == 2)
                    {
                        tf = Autodesk.Revit.DB.Transform.CreateTranslation(((h - cover - barDiameter / 2 - 30.0.milimeter2Feet()) * geoEttElem.BasisY) - ((cover + barDiameter / 2) * geoEttElem.BasisX));
                        //tf = Autodesk.Revit.DB.Transform.CreateTranslation(((500.0.milimeter2Feet()) * geoEttElem.BasisY) - ((cover + barDiameter / 2) * geoEttElem.BasisX));

                    }
                    //tf = Autodesk.Revit.DB.Transform.CreateTranslation(((h - cover - barDiameter / 2) * geoEttElem.BasisY) - ((cover + barDiameter / 2) * geoEttElem.BasisX));
                    break;
            }
            var transfomedCurve = originCurve.CreateTransformed(tf);
            return transfomedCurve;

        }
        public static Autodesk.Revit.DB.XYZ GetOriginRebar (this Model.Entity.Rebar rebar)
        {
            var host = rebar.Element;
            var topRebars = host.Rebars.Where(x => x.RebarLayer == Model.Entity.RebarLayer.Top);
            var botRebars = host.Rebars.Where(x => x.RebarLayer == Model.Entity.RebarLayer.Bottom);
            var straightRebarsSameLayer = host.Rebars.Where(x => x.RebarLayer == rebar.RebarLayer && x.LayerIndex == rebar.LayerIndex && x.RebarFunction == Model.Entity.RebarFunction.Straight).ToList();
            var straightRebarsSameLayerTest = straightRebarsSameLayer.FirstOrDefault();
            var stirrup = host.Stirrups.Where(x=>x.StirrupFunction == Model.Entity.StirrupFunction.Closed_Type).First();
            var stirrupDiameter = stirrup.RebarBarType.BarDiameter;
            
            var setting = modelData.Setting;
            var cover = setting.ConcreteCover;
            var topRebarRatio = setting.TopReinforcedLayerLengthRatio;
            var botRebarRatio = setting.BottomReinforcedLayerLengthRatio;
            var layerDis = setting.LayerDistance;

            var geoHost = rebar.Element.Geometry;
            var lengthHost = geoHost.LengthZ;
            var bHost = geoHost.LengthX;
            var hHost = geoHost.LengthY;
            var vecX = geoHost.BasisX;
            var vecY = geoHost.BasisY;
            var vecZ = geoHost.BasisZ;

            var barDiameter = rebar.RebarBarType.BarDiameter;
            var quantities = rebar.Quantity;
            double offsetX = 0;
            if (rebar.RebarFunction == Model.Entity.RebarFunction.Straight)
            {
                offsetX = cover + barDiameter / 2 + stirrupDiameter;
            }
            else
            {
                if (straightRebarsSameLayerTest == null)
                {
                    offsetX = cover + barDiameter / 2 + stirrupDiameter;
                }
                else
                {
                    offsetX = cover + barDiameter / 2 + stirrupDiameter + rebar.Spacing;
                }
            }

            //double offsetX = cover + barDiameter/2;
            double offsetY = 0;
            switch(rebar.RebarLayer)
            {
                case Model.Entity.RebarLayer.Bottom:
                    offsetY = cover + barDiameter / 2 + (layerDis + barDiameter) * (rebar.LayerIndex - 1) + stirrup.RebarBarType.BarDiameter;
                    break;
                case Model.Entity.RebarLayer.Middle:
                    offsetY = hHost / 2;
                    break;
                case Model.Entity.RebarLayer.Top:
                    offsetY = hHost - (cover + barDiameter / 2) - (layerDis + barDiameter) * (rebar.LayerIndex - 1) - stirrup.RebarBarType.BarDiameter;
                    break;
            }
            double offsetZ = 0;
            switch(rebar.RebarFunction)
            {
                case Model.Entity.RebarFunction.Straight:
                    offsetZ = cover + barDiameter / 2;
                    break;
                case Model.Entity.RebarFunction.Right_Reinforce:
                    offsetZ = lengthHost - lengthHost / topRebarRatio;
                    break;
                case Model.Entity.RebarFunction.Left_Reinforce:
                    offsetZ = cover + barDiameter / 2;
                    break;
                case Model.Entity.RebarFunction.Middle_Reinforce:
                    offsetZ = lengthHost / botRebarRatio;
                    break;
            }
            var originRebar = geoHost.Origin + offsetX * vecX + offsetY * vecY + offsetZ * vecZ;
            return originRebar;
            
        }
        public static Autodesk.Revit.DB.Curve GetMainCurve(this Model.Entity.Rebar rebar)
        {
            #region Kien
            //var ettElem = rebar.Element;
            //var geoEttElem = ettElem.Geometry;
            //var originEtt = geoEttElem.Origin;
            //var cover = ettElem.RebarCoverType.CoverDistance;
            //var lengthElem = geoEttElem.LengthZ;
            //Autodesk.Revit.DB.Curve mainCurve = null;
            //Autodesk.Revit.DB.Curve originCurve = null;
            //var rebarFunc = rebar.RebarFunction;
            //if (rebarFunc == Model.Entity.RebarFunction.Straight)
            //{
            //    originCurve = Autodesk.Revit.DB.Line.CreateBound(originEtt + (cover * geoEttElem.BasisZ),
            //    (originEtt + (lengthElem - cover) * geoEttElem.BasisZ));

            //    mainCurve = originCurve.TransformCurve(rebar);
            //}
            //else if (rebarFunc == Model.Entity.RebarFunction.Right_Reinforce)
            //{
            //    //originCurve = Autodesk.Revit.DB.Line.CreateBound(originEtt + (3 / 4 * lengthElem * geoEttElem.BasisZ),
            //    //    (originEtt + (lengthElem - cover) * geoEttElem.BasisZ));
            //    originCurve = Autodesk.Revit.DB.Line.CreateBound(originEtt + (3 * lengthElem / 4) * geoEttElem.BasisZ,
            //        (originEtt + ((lengthElem - cover) * geoEttElem.BasisZ)));
            //    mainCurve = originCurve.TransformCurve(rebar);

            //}
            //else if (rebarFunc == Model.Entity.RebarFunction.Left_Reinforce)
            //{
            //    originCurve = Autodesk.Revit.DB.Line.CreateBound(originEtt + (cover * geoEttElem.BasisZ),
            //    (originEtt + ((lengthElem / 4) * geoEttElem.BasisZ)));
            //    mainCurve = originCurve.TransformCurve(rebar);

            //}
            //else if (rebarFunc == Model.Entity.RebarFunction.Middle_Reinforce)
            //{
            //    originCurve = Autodesk.Revit.DB.Line.CreateBound(originEtt + (lengthElem / 5 * geoEttElem.BasisZ),
            //        originEtt + ((lengthElem - lengthElem / 5) * geoEttElem.BasisZ));
            //    mainCurve = originCurve.TransformCurve(rebar);

            //}
            #endregion
            var setting = modelData.Setting;
            var cover = setting.ConcreteCover;
            var topRebarRatio = setting.TopReinforcedLayerLengthRatio;
            var botRebarRatio = setting.BottomReinforcedLayerLengthRatio;
            var layerDis = setting.LayerDistance;
            var geoHost = rebar.Element.Geometry;
            var lengthHost = geoHost.LengthZ;
            var bHost = geoHost.LengthX;
            var hHost = geoHost.LengthY;
            var vecX = geoHost.BasisX;
            var vecY = geoHost.BasisY;
            var vecZ = geoHost.BasisZ;
            var barDiameter = rebar.RebarBarType.BarDiameter;
            var originRebar = rebar.GetOriginRebar();
            double lengthCurve = 0;
            switch(rebar.RebarFunction)
            {
                case Model.Entity.RebarFunction.Straight:
                    lengthCurve = lengthHost - (cover + barDiameter / 2) * 2;
                    break;
                case Model.Entity.RebarFunction.Right_Reinforce:
                case Model.Entity.RebarFunction.Left_Reinforce:
                    lengthCurve = (lengthHost / topRebarRatio) - (cover + barDiameter / 2);
                    break;
                case Model.Entity.RebarFunction.Middle_Reinforce:
                    lengthCurve = lengthHost - ( 2 * ( lengthHost / botRebarRatio));
                    break;
            }
            var mainCurve = Autodesk.Revit.DB.Line.CreateBound(originRebar, originRebar + lengthCurve * vecZ);

            return mainCurve;
        }
        public static List<Autodesk.Revit.DB.Curve> GetCurves(this Model.Entity.Rebar rebar)
        {
            #region Kien
            //var cover = rebar.Element.RebarCoverType.CoverDistance;
            //var mainCurve = rebar.GetMainCurve();
            //var geoEtt = rebar.Element.Geometry;
            //var h = geoEtt.LengthY;
            //List<Autodesk.Revit.DB.Curve> curves = new List<Autodesk.Revit.DB.Curve> { mainCurve };
            //Autodesk.Revit.DB.XYZ firstPoint = mainCurve.GetEndPoint(0);
            //Autodesk.Revit.DB.XYZ endPoint = mainCurve.GetEndPoint(1);
            //Autodesk.Revit.DB.Curve leftHook = null;
            //Autodesk.Revit.DB.Curve rightHook = null;
            //switch (rebar.RebarLayer)
            //{
            //    case Model.Entity.RebarLayer.Bottom:
            //        switch (rebar.RebarFunction)
            //        {
            //            case Model.Entity.RebarFunction.Straight:
            //                if (rebar.HasHook == true)
            //                {
            //                    if (rebar.LayerIndex == 1)
            //                    {
            //                        leftHook = Autodesk.Revit.DB.Line.CreateBound(firstPoint, firstPoint + (geoEtt.BasisY * 250.0.milimeter2Feet()));
            //                        rightHook = Autodesk.Revit.DB.Line.CreateBound(endPoint, endPoint + (geoEtt.BasisY * 250.0.milimeter2Feet()));
            //                        curves.Add(leftHook); curves.Add(rightHook);
            //                    }
            //                    else if (rebar.LayerIndex == 2)
            //                    {
            //                        leftHook = Autodesk.Revit.DB.Line.CreateBound(firstPoint, firstPoint + (geoEtt.BasisY * (250 - 30).milimeter2Feet()));
            //                        rightHook = Autodesk.Revit.DB.Line.CreateBound(endPoint, endPoint + (geoEtt.BasisY * (250 - 30).milimeter2Feet()));
            //                        curves.Add(leftHook); curves.Add(rightHook);
            //                    }
            //                }
            //                break;
            //        }
            //        break;
            //    case Model.Entity.RebarLayer.Top:
            //        switch (rebar.RebarFunction)
            //        {
            //            case Model.Entity.RebarFunction.Straight:
            //                if (rebar.HasHook == true)
            //                {
            //                    if (rebar.LayerIndex == 1)
            //                    {
            //                        leftHook = Autodesk.Revit.DB.Line.CreateBound(firstPoint, firstPoint - ((h - 2 * cover) * geoEtt.BasisY));
            //                        rightHook = Autodesk.Revit.DB.Line.CreateBound(endPoint, endPoint - ((h - 2 * cover) * geoEtt.BasisY));
            //                        curves.Add(leftHook); curves.Add(rightHook);
            //                    }
            //                    else if (rebar.LayerIndex == 2)
            //                    {
            //                        leftHook = Autodesk.Revit.DB.Line.CreateBound(firstPoint, firstPoint - ((h - 2 * cover - 30.0.milimeter2Feet()) * geoEtt.BasisY));
            //                        rightHook = Autodesk.Revit.DB.Line.CreateBound(endPoint, endPoint - ((h - 2 * cover - 30.0.milimeter2Feet()) * geoEtt.BasisY));
            //                        curves.Add(leftHook); curves.Add(rightHook);
            //                    }

            //                }
            //                break;
            //            case Model.Entity.RebarFunction.Right_Reinforce:
            //                if (rebar.HasHook == true)
            //                {
            //                    if (rebar.LayerIndex == 1)
            //                    {
            //                        rightHook = Autodesk.Revit.DB.Line.CreateBound(endPoint, endPoint - ((h - 2 * cover) * geoEtt.BasisY));
            //                        curves.Add(rightHook);
            //                    }
            //                    else if (rebar.LayerIndex == 2)
            //                    {
            //                        rightHook = rightHook = Autodesk.Revit.DB.Line.CreateBound(endPoint, endPoint - ((h - 2 * cover - 30.0.milimeter2Feet()) * geoEtt.BasisY));
            //                        curves.Add(rightHook);
            //                    }
            //                }
            //                break;
            //            case Model.Entity.RebarFunction.Left_Reinforce:
            //                if (rebar.HasHook == true)
            //                {
            //                    if (rebar.LayerIndex == 1)
            //                    {
            //                        leftHook = Autodesk.Revit.DB.Line.CreateBound(firstPoint, firstPoint - ((h - 2 * cover) * geoEtt.BasisY));
            //                        curves.Add(leftHook);
            //                    }
            //                    else if (rebar.LayerIndex == 2)
            //                    {
            //                        leftHook = leftHook = Autodesk.Revit.DB.Line.CreateBound(firstPoint, firstPoint - ((h - 2 * cover - 30.0.milimeter2Feet()) * geoEtt.BasisY));
            //                        curves.Add(leftHook);
            //                    }
            //                }
            //                break;
            //        }
            //        break;
            //}

            #endregion
            var setting = modelData.Setting;
            var cover = setting.ConcreteCover;
            var topRebarRatio = setting.TopReinforcedLayerLengthRatio;
            var botRebarRatio = setting.BottomReinforcedLayerLengthRatio;
            var layerDis = setting.LayerDistance;
            var geoHost = rebar.Element.Geometry;
            var lengthHost = geoHost.LengthZ;
            var bHost = geoHost.LengthX;
            var hHost = geoHost.LengthY;
            var vecXHost = geoHost.BasisX;
            var vecYHost = geoHost.BasisY;
            var vecZHost = geoHost.BasisZ;
            var barDiameter = rebar.RebarBarType.BarDiameter;
            var mainCurve = rebar.GetMainCurve();
            Autodesk.Revit.DB.XYZ vecYTf = null;
            double offsetY = 0;
            if(!rebar.HasHook || rebar.RebarLayer == Model.Entity.RebarLayer.Middle ||rebar.RebarFunction == Model.Entity.RebarFunction.Middle_Reinforce)
            {
                return new List<Autodesk.Revit.DB.Curve> { mainCurve };
            }
            switch(rebar.RebarLayer)
            {
                case Model.Entity.RebarLayer.Top:
                    vecYTf = -vecYHost;
                    //offsetY = hHost - ((2 * cover) + (barDiameter / 2)) - layerDis * (rebar.LayerIndex - 1);
                    break;
                case Model.Entity.RebarLayer.Bottom:
                    vecYTf = vecYHost;
                    //offsetY = 250.0.milimeter2Feet();
                    break;
            }
            offsetY = hHost - 2 * (cover + barDiameter / 2) - (layerDis + barDiameter) * (rebar.LayerIndex - 1);

            Autodesk.Revit.DB.XYZ firstPoint = mainCurve.GetEndPoint(0);
            Autodesk.Revit.DB.XYZ endPoint = mainCurve.GetEndPoint(1);
            Autodesk.Revit.DB.Curve leftHook = null;
            Autodesk.Revit.DB.Curve rightHook = null;
            switch(rebar.RebarFunction)
            {
                case Model.Entity.RebarFunction.Straight:
                    leftHook = Autodesk.Revit.DB.Line.CreateBound(firstPoint + offsetY * vecYTf, firstPoint);
                    rightHook = Autodesk.Revit.DB.Line.CreateBound(endPoint, endPoint + offsetY * vecYTf);
                    break;
                case Model.Entity.RebarFunction.Left_Reinforce:
                    leftHook = Autodesk.Revit.DB.Line.CreateBound(firstPoint + offsetY * vecYTf, firstPoint);
                    break;
                case Model.Entity.RebarFunction.Right_Reinforce:
                    rightHook = Autodesk.Revit.DB.Line.CreateBound(endPoint, endPoint + offsetY * vecYTf);
                    break;
            }
            switch(rebar.RebarFunction)
            {
                case Model.Entity.RebarFunction.Straight:
                    return new List<Autodesk.Revit.DB.Curve> { leftHook, mainCurve, rightHook };
                case Model.Entity.RebarFunction.Right_Reinforce:
                    return new List<Autodesk.Revit.DB.Curve> { mainCurve, rightHook };
                case Model.Entity.RebarFunction.Left_Reinforce:
                    return new List<Autodesk.Revit.DB.Curve> { leftHook, mainCurve };
            }

            throw new Model.Exception.CaseNotCheckException();
        }
        public static double GetSpacing(this Model.Entity.Rebar rebar)
        {
            var setting = modelData.Setting;
            var cover = setting.ConcreteCover;

            var stirrup = rebar.Element.Stirrups.First();
            var stirrupDiameter = stirrup.RebarBarType.BarDiameter;

            var quantity = rebar.Quantity;
            var diameter = rebar.RebarBarType.BarDiameter;
            var rebarLayer = rebar.RebarLayer;
            var layerIndex = rebar.LayerIndex;

            var host = rebar.Element;
            var rebarSameLayer = host.Rebars.Where(x => x.RebarLayer == rebarLayer && x.LayerIndex == layerIndex && (x.RebarFunction == Model.Entity.RebarFunction.Straight || x.RebarFunction == rebar.RebarFunction));
            var noRebarSameLayer = 0;
            foreach (var item in rebarSameLayer)
            {
                noRebarSameLayer += item.Quantity;
            }
            
            //var cover = rebar.Element.RebarCoverType.CoverDistance;
            var b = rebar.Element.Geometry.LengthX;
            double spacing = 0;
            // spacing = width / (quantity-1): bị thiếu chia cho số lượng
            //var spacing = (b - (2 * (cover+(diameter/2))))/ (quantity-1); 
            switch(rebar.RebarFunction)
            {
                case Model.Entity.RebarFunction.Straight:
                    spacing = (b -  (2 * (cover + stirrupDiameter  + (diameter / 2)))) / quantity;
                    break;
                default:
                    spacing = (b -  (2 * (cover + stirrupDiameter +  (diameter / 2)))) / (noRebarSameLayer - 1);
                    break;

            }
           

            return spacing;
        }
        public static Autodesk.Revit.DB.Structure.Rebar CreateRebar(this Model.Entity.Rebar rebar)
        {
            var doc = RevitData.Instance.Document;
            var quantities = rebar.Quantity;

            // không nên sử dụng Phương thức vì sẽ bị lặp lại đoạn lệnh nhiều lần, nên sử dụng dạng thuộc tính
            // Cập nhập phần thuộc tính bên kiểu dữ liệu Rebar
            //var spacing = rebar.GetSpacing();
            var spacing = rebar.Spacing;
            //var arrayLength = rebar.ArrayLength;

            double arrayLength = 0;
            switch(rebar.RebarFunction)
            {
                case Model.Entity.RebarFunction.Straight:
                    arrayLength = rebar.ArrayLength;
                    break;
                default:
                    arrayLength = spacing * (quantities - 1);
                    break;
            }


            var rebarStyle = Autodesk.Revit.DB.Structure.RebarStyle.Standard;
            var rebarBarType = rebar.RebarBarType;

            // nếu hook00 muốn lấy giá trị null thì chỉ cần gán nó bằng null
            //Autodesk.Revit.DB.Structure.RebarHookType hook00 = null;
            var hook00 = RevitData.Instance.RebarHookTypes.SingleOrDefault(x => x.Id == null);

            var hostElem = rebar.Element.RevitElement;
            var bDir = rebar.Element.Geometry.BasisX;

            // không nên sử dụng Phương thức vì sẽ bị lặp lại đoạn lệnh nhiều lần, nên sử dụng dạng thuộc tính
            // Cập nhập phần thuộc tính bên kiểu dữ liệu Rebar
            //var curves = rebar.GetCurves();

            var curves = rebar.Curves;

            Autodesk.Revit.DB.Structure.RebarHookOrientation startHook = Autodesk.Revit.DB.Structure.RebarHookOrientation.Left;
            Autodesk.Revit.DB.Structure.RebarHookOrientation endHook = Autodesk.Revit.DB.Structure.RebarHookOrientation.Right;

            //Test curve
            //foreach (var item in curves)
            //{
            //    doc.Create.NewDetailCurve(RevitData.Instance.ActiveView, item);
            //}

            // tham biến Normal định nghĩa phương rải thép, ở đây điểm đặt của Curve đầu tiên sát gốc tọa độ
            // nên phương rải trùng với bDir chứ không phải -bDir
            Autodesk.Revit.DB.Structure.Rebar newRebar = Autodesk.Revit.DB.Structure.Rebar.CreateFromCurves(doc, rebarStyle, rebarBarType, null, null,
                hostElem, bDir, curves, startHook, endHook, true, true);
            Autodesk.Revit.DB.Structure.RebarShapeDrivenAccessor rsda = newRebar.GetShapeDrivenAccessor();
            rsda.SetLayoutAsFixedNumber(quantities, arrayLength, true, true, true);
            return null;
        }
    }
}
