using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using SingleData;

namespace Utility
{
    public static class InputFormUtil
    {
        private static RevitData revitData
        {
            get
            {
                return RevitData.Instance;
            }
        }
        private static FormData formData
        {
            get
            {
                return FormData.Instance;
            }
        }
        private static ModelData modelData
        {
            get
            {
                return ModelData.Instance;
            }
        }
        public static void SelectElement()
        {
            var form = formData.InputForm;
            form.Hide();
            var sel = revitData.Selection;
            Func<Autodesk.Revit.DB.Element, bool> framingFilter = x => (Autodesk.Revit.DB.BuiltInCategory)x.Category.Id.IntegerValue 
            == Autodesk.Revit.DB.BuiltInCategory.OST_StructuralFraming;
            try
            {
                formData.ElementView = sel.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element).GetRevitElement().GetEntityElement().GetElementView();
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {

            }
            form.ShowDialog();
            
        }
        public static void AddRebar()
        {
            var rebar = new Model.Entity.Rebar();
            rebar.Element = formData.ElementView.EttElement;
            var rebarView = new Model.ViewModel.RebarView { Rebar = rebar };
            modelData.Rebars.Add(rebar);
            formData.RebarViews.Add(rebarView);
            formData.ElementView.EttElement.Rebars.Add(rebar);
        }
        public static void DeleteRebars(this Model.ViewModel.RebarView rebarView)
        {
            formData.RebarViews.Remove(rebarView);
            modelData.Rebars.Remove(rebarView.Rebar);
        }
        public static void AddStirrup()
        {
            var stirrup = new Model.Entity.Stirrup();
            stirrup.Element = formData.ElementView.EttElement;
            var stirrupView = new Model.ViewModel.StirrupView { Stirrup = stirrup };
            modelData.Stirrups.Add(stirrup);
            formData.StirrupViews.Add(stirrupView);
            formData.ElementView.EttElement.Stirrups.Add(stirrup);
        }
        public static void DeleteStirrup(this Model.ViewModel.StirrupView stirrupView)
        {
            formData.StirrupViews.Remove(stirrupView);
            modelData.Stirrups.Remove(stirrupView.Stirrup);
        }
    }
}
