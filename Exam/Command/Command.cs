using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using SingleData;
using Utility;
using Autodesk.Revit.DB.Structure;

namespace Exam
{
    [Transaction(TransactionMode.Manual)]
    public class Command :IExternalCommand

    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            #region Initial

            RevitData revitData = RevitData.Instance;
            revitData.UIApplication = commandData.Application;
            var sel = revitData.Selection;
            var doc = revitData.Document;
            var activeView = revitData.ActiveView;
            var tx = revitData.Transaction;
            var uidoc = revitData.UIDocument;
            var app = revitData.Application;
            var formData = FormData.Instance;
            var modelData = ModelData.Instance;

            tx.Start();
            #endregion


            var d18Type = revitData.RebarBarTypes.SingleOrDefault(x => x.Name == "D18");
            var hook90250 = revitData.RebarHookTypes.SingleOrDefault(x => x.Name == "90-250");
            var hook00 = revitData.RebarHookTypes.SingleOrDefault(x => x.Id == null);
            var rebarStype = RebarStyle.Standard;


            Func<Element, bool> filterBeam = x =>
            {
                
                var cateId = x.Category.Id.IntegerValue;
                if (cateId == (int)BuiltInCategory.OST_StructuralFraming) return true;
                return false;
            };
            var beamRV = sel.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, new FuncSelectionFilter(filterBeam)).GetElement();
            var beamEtt = beamRV.GetEntityElement();
            var bDir = beamEtt.Geometry.BasisX;
            double cover = 25.0.Milimet2Feet() + d18Type.BarDiameter / 2;
            double barDiameter = d18Type.BarDiameter;
            var yPos = beamRV.AsValue("y Justification").ValueNumber;
            var yOffset = beamRV.AsValue("y Offset Value").ValueNumber;
            var zPos = beamRV.AsValue("z Justification").ValueNumber;
            var zOffset = beamRV.AsValue("z Offset Value").ValueNumber;

            var locationCurve1 = beamEtt.GetLocationCurve(25, 18);

            //var elem = sel.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, new FuncSelectionFilter(filterBeam)).GetElement().GetEntityElement();
            //formData.ElementView = elem.GetElementView();
            //var form = formData.CreateRebarForm;
            //form.ShowDialog();

            //Curve a = Line.CreateBound(beamEtt.Geometry.OriginForRebar, beamEtt.Geometry.OriginForRebar - beamEtt.Geometry.BasisZ * 3000.0.Milimet2Feet());
            //Rebar newRebarTa = Rebar.CreateFromCurves(doc, rebarStype, d18Type, hook00, hook00,
            //    beamRV, bDir, new List<Curve> { a }, RebarHookOrientation.Left, RebarHookOrientation.Right, true, true);
            //RebarShapeDrivenAccessor rsdaTa = newRebarTa.GetShapeDrivenAccessor();
            //rsdaTa.SetLayoutAsFixedNumber(1, beamEtt.Geometry.LengthX.Milimet2Feet() - 2 * cover, true, true, true);

            Rebar newRebarT1 = Rebar.CreateFromCurves(doc, rebarStype, d18Type, hook00, hook00,
                beamRV, bDir, new List<Curve> { locationCurve1.T1 }, RebarHookOrientation.Left, RebarHookOrientation.Right, true, true);
            RebarShapeDrivenAccessor rsdaT1 = newRebarT1.GetShapeDrivenAccessor();
            rsdaT1.SetLayoutAsFixedNumber(2, beamEtt.Geometry.LengthX.Milimet2Feet() - 2 * cover, true, true, true);
            Rebar newRebarT2 = Rebar.CreateFromCurves(doc, rebarStype, d18Type, hook00, hook00,
                beamRV, bDir, new List<Curve> { locationCurve1.T2 }, RebarHookOrientation.Left, RebarHookOrientation.Right, true, true);
            RebarShapeDrivenAccessor rsdaT2 = newRebarT2.GetShapeDrivenAccessor();
            rsdaT2.SetLayoutAsFixedNumber(2, beamEtt.Geometry.LengthX.Milimet2Feet() - 2 * cover, true, true, true);
            Rebar newRebarB1 = Rebar.CreateFromCurves(doc, rebarStype, d18Type, hook00, hook00,
               beamRV, bDir, new List<Curve> { locationCurve1.B1 }, RebarHookOrientation.Left, RebarHookOrientation.Right, true, true);
            RebarShapeDrivenAccessor rsdaB1 = newRebarB1.GetShapeDrivenAccessor();
            rsdaB1.SetLayoutAsFixedNumber(2, beamEtt.Geometry.LengthX.Milimet2Feet() - 2 * cover, true, true, true);
            Rebar newRebarB2 = Rebar.CreateFromCurves(doc, rebarStype, d18Type, hook00, hook00,
               beamRV, bDir, new List<Curve> { locationCurve1.B2 }, RebarHookOrientation.Left, RebarHookOrientation.Right, true, true);
            RebarShapeDrivenAccessor rsdaB2 = newRebarB2.GetShapeDrivenAccessor();
            rsdaB2.SetLayoutAsFixedNumber(2, beamEtt.Geometry.LengthX.Milimet2Feet() - 2 * cover, true, true, true);
            tx.Commit();


            return Result.Succeeded;
        }

    }
}
