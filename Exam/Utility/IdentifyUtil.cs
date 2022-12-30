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
            identify.Type = revitElem.GetTypeId().GetElement().Name;
            identify.Name = revitElem.LookupParameter("ElementName").AsString();
            switch(ettElem.ElementType)
            {
                case Model.Entity.ElementType.Column:
                    identify.Level = revitElem.LevelId.GetElement().Name;
                    break;
                case Model.Entity.ElementType.Framing:
                    identify.Level = revitElem.LookupParameter("Reference Level").AsElementId().GetElement().Name;
                    break;
            }
            return identify;
        }
    }
}
