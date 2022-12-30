using System;
using System.Linq;
using System.Collections.Generic;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Structure;
using SingleData;
using Autodesk.Revit.Attributes;
using Utility;

namespace FormworkCalculation
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            #region Initial
            Singleton.Instance = new Singleton();
            var revitData = RevitData.Instance;
            var modelData = ModelData.Instance;

            revitData.UIApplication = commandData.Application;
            var sel = revitData.Selection;
            var uidoc = revitData.UIDocument;
            var doc = revitData.Document;
            var app = revitData.Application;
            var formData = FormData.Instance;
            //revitData.ExternalEvent = ExternalEvent.Create(revitData.ExternalEventHandler);
            var tx = revitData.Transaction;
            tx.Start();
            #endregion
            //var ettElem = sel.PickObject(ObjectType.Element).GetRevitElement().GetEntityElement();

            //Model.Entity.Rebar rebar = new Model.Entity.Rebar()
            //{
            //    Element = ettElem,
            //    HasHook = false,
            //    RebarLayer = Model.Entity.RebarLayer.Top,
            //    LayerIndex = 1,
            //    Quantity = 2,

            //    RebarBarType = RevitData.Instance.RebarBarTypes.SingleOrDefault(x => x.Name == "D18"),
            //    RebarFunction = Model.Entity.RebarFunction.Straight,
            //};
            //Model.Entity.Rebar rebar1 = new Model.Entity.Rebar()
            //{
            //    Element = ettElem,
            //    HasHook = false,
            //    RebarLayer = Model.Entity.RebarLayer.Top,
            //    LayerIndex = 1,
            //    Quantity = 2,

            //    RebarBarType = RevitData.Instance.RebarBarTypes.SingleOrDefault(x => x.Name == "D18"),
            //    RebarFunction = Model.Entity.RebarFunction.Left_Reinforce,
            //};
            //Model.Entity.Rebar rebar2 = new Model.Entity.Rebar()
            //{
            //    Element = ettElem,
            //    HasHook = false,
            //    RebarLayer = Model.Entity.RebarLayer.Top,
            //    LayerIndex = 2,
            //    Quantity = 2,
            //    RebarBarType = RevitData.Instance.RebarBarTypes.SingleOrDefault(x => x.Name == "D18"),
            //    RebarFunction = Model.Entity.RebarFunction.Left_Reinforce,
            //};
            //Model.Entity.Rebar rebar3 = new Model.Entity.Rebar()
            //{
            //    Element = ettElem,
            //    HasHook = false,
            //    RebarLayer = Model.Entity.RebarLayer.Top,
            //    LayerIndex = 2,
            //    Quantity = 2,
            //    RebarBarType = RevitData.Instance.RebarBarTypes.SingleOrDefault(x => x.Name == "D18"),
            //    RebarFunction = Model.Entity.RebarFunction.Right_Reinforce,
            //};
            //Model.Entity.Rebar rebar4 = new Model.Entity.Rebar()
            //{
            //    Element = ettElem,
            //    HasHook = false,
            //    RebarLayer = Model.Entity.RebarLayer.Top,
            //    LayerIndex = 1,
            //    Quantity = 2,

            //    RebarBarType = RevitData.Instance.RebarBarTypes.SingleOrDefault(x => x.Name == "D18"),
            //    RebarFunction = Model.Entity.RebarFunction.Right_Reinforce,
            //};

            #region  Test CreateFromRebarShape
            //var origin = ettElem.Geometry.Origin - 500.0.milimeter2Feet() * ettElem.Geometry.BasisZ + 500.0.milimeter2Feet() * ettElem.Geometry.BasisY;
            //var xVec = ettElem.Geometry.BasisY;
            //var yVec = ettElem.Geometry.BasisX ;
            //var rebar1 = Rebar.CreateFromRebarShape(doc, modelData.RebarShapes.Single(x=>x.Name == "M_T2"), RevitData.Instance.RebarBarTypes.SingleOrDefault(x => x.Name == "D18"),
            //    ettElem.RevitElement, origin, xVec,
            //    yVec);
            #endregion


            //Model.Entity.Stirrup stirrupC = new Model.Entity.Stirrup()
            //{
            //    Element = ettElem,
            //    SectionIndex = 3,
            //    StirrupPosition = Model.Entity.StirrupPosition.Left,
            //    StirrupFunction = Model.Entity.StirrupFunction.C_Type,
            //    RebarBarType = RevitData.Instance.RebarBarTypes.SingleOrDefault(x=>x.Name == "D8"),
            //};
            //Model.Entity.Stirrup stirrupR = new Model.Entity.Stirrup()
            //{
            //    Element = ettElem,
            //    SectionIndex = 1,
            //    StirrupPosition = Model.Entity.StirrupPosition.Left,
            //    Spacing = 150.0.milimeter2Feet(),
            //    StirrupFunction = Model.Entity.StirrupFunction.Closed_Type,
            //    RebarBarType = RevitData.Instance.RebarBarTypes.SingleOrDefault(x => x.Name == "D10"),
            //};

            //ettElem.Rebars.Add(rebar);
            //ettElem.Rebars.Add(rebar1);
            //ettElem.Rebars.Add(rebar2);
            //ettElem.Rebars.Add(rebar3);
            //ettElem.Rebars.Add(rebar4);
            //ettElem.Stirrups.Add(stirrupR);
            //RebarUtil.CreateRebar(rebar);
            //RebarUtil.CreateRebar(rebar1);
            //RebarUtil.CreateRebar(rebar2);
            //RebarUtil.CreateRebar(rebar3);
            //RebarUtil.CreateRebar(rebar4);

            //StirrupUtil.CreateStirrup(stirrupR);

            //var setting = new Model.Entity.Setting();
            //var form = formData.InputForm;
            //form.ShowDialog();

            tx.Commit();
            return Result.Succeeded;
        }
    }
}