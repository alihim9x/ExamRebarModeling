using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;

namespace Utility
{
    public static class ElementUtil
    {
        public static Autodesk.Revit.DB.Element GetRevitElement (this Autodesk.Revit.DB.ElementId elemId)
        {
            return RevitData.Instance.Document.GetElement(elemId);
        }
        public static Autodesk.Revit.DB.Element GetRevitElement(this Autodesk.Revit.DB.Reference reference)
        {
            return RevitData.Instance.Document.GetElement(reference);
        }
        public static Model.Entity.Element GetEntityElement(this Autodesk.Revit.DB.Element elem)
        {
            var ettElem = ModelData.Instance.EttElements.SingleOrDefault(x => x.RevitElement.UniqueId == elem.UniqueId);
            if(ettElem == null)
            {
                ettElem = new Model.Entity.Element()
                {
                    RevitElement = elem
                };
                ModelData.Instance.EttElements.Add(ettElem);
            }
            return ettElem;
        }
        public static void CreateRebars(this Model.Entity.Element element)
        {
            foreach (var rebar in element.Rebars)
            {
                rebar.CreateRebar();
            }
            foreach (var stirrup in element.Stirrups)
            {
                stirrup.CreateStirrup();
            }
        }
        public static Model.Entity.ElementType GetElementType(this Model.Entity.Element ettElem)
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
        
    }
}
