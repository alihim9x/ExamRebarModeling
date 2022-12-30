using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;

namespace Utility
{
    public static class IdentifyUtil
    {
        private static ModelData modelData
        {
            get
            {
                return ModelData.Instance;
            }
        }
        public static Model.Entity.Identify GetIdentify (this Model.Entity.Element ettElem)
        {
            var identify = new Model.Entity.Identify();
            var revitElem = ettElem.RevitElement;
            identify.Type = revitElem.GetTypeId().GetRevitElement().Name;
            switch(ettElem.ElementType)
            {
                case Model.Entity.ElementType.Column:
                    identify.Level = revitElem.LevelId.GetRevitElement().Name;
                    break;
                case Model.Entity.ElementType.Framing:
                    identify.Level = revitElem.LookupParameter("Reference Level").AsElementId().GetRevitElement().Name;
                    break;
            }
            identify.Name = revitElem.LookupParameter("TenCK").AsString();
            return identify;
        }

    }
}
