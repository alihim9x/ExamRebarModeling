using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;

namespace Utility
{
    public static class CreateRebarFormUtil
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
        public static void PickElement()
        {
            var form = formData.CreateRebarForm;
            form.Hide();
            var sel = revitData.Selection;
            try
            {
                formData.ElementView = sel.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element).GetElement().GetEntityElement().GetElementView();
            }
            catch(Autodesk.Revit.Exceptions.OperationCanceledException)
            {

            }
            form.ShowDialog();
        }
        
    }
}
