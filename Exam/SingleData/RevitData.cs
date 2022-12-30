using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Structure;
using Utility;

namespace SingleData
{
    public class RevitData
    {
        public static RevitData Instance { get { return SingleTon.Instance.RevitData; } }
        public virtual UIApplication UIApplication { get; set; }
        public virtual Application Application { get { return UIApplication.Application; } }
        public virtual UIDocument UIDocument { get { return UIApplication.ActiveUIDocument; } }
        public virtual Document Document { get { return UIDocument.Document; } }
        public virtual Selection Selection { get { return UIDocument.Selection; } }
        public virtual View ActiveView { get { return UIDocument.ActiveView; } }
        private Transaction transaction;
        public Transaction Transaction
        {
            get
            {
                if (transaction == null) transaction = new Transaction(Document, "RevitAddin"); return transaction;
            }
            set
            {
                transaction = value;
            }
        }
        public virtual BindingMap BindingMap { get { return Document.ParameterBindings; } }
        private List<Category> categories;
        public virtual List<Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    categories = new List<Category>();
                    var cates = Document.Settings.Categories;
                    foreach (Category item in cates)
                    {
                        categories.Add(item);
                    }
                }
                return categories;
            }
        }
        private IEnumerable<Element> instanceElements;
        public virtual IEnumerable<Element> InstanceElements
        {
            get
            {
                if (instanceElements == null)
                {
                    instanceElements = new FilteredElementCollector(Document).WhereElementIsNotElementType();
                }
                return instanceElements;
            }
        }
        private IEnumerable<Element> instanceElementsInView;
        public virtual IEnumerable<Element> InstanceElementsInView
        {
            get
            {
                if (instanceElementsInView == null)
                {
                    instanceElementsInView = new FilteredElementCollector(Document, ActiveView.Id).WhereElementIsNotElementType();
                }
                return instanceElementsInView;
            }
        }
        private IEnumerable<Level> levels;
        public virtual IEnumerable<Level> Levels
        {
            get
            {
                if (levels == null)
                {
                    levels = InstanceElements.Where(x => x is Level).Cast<Level>(); // Cast là ép kiểu từng đối tượng trong mảng
                }
                return levels;
            }
        }
        private IEnumerable<Element> typeElements;
        public virtual IEnumerable<Element> TypeElements
        {
            get
            {
                if (typeElements == null)
                {
                    typeElements = new FilteredElementCollector(Document).WhereElementIsElementType();
                }
                return typeElements;
            }
        }
        private IEnumerable<FloorType> floortypes;
        public virtual IEnumerable<FloorType> FloorTypes // Virtual trong trường hợp này là khi trong dự án ko gọi tới thì nó sẽ ko tạo ra
        {
            get
            {
                if (floortypes == null)
                {
                    floortypes = TypeElements.Where(x => x is FloorType).Cast<FloorType>();
                }
                return floortypes;
            }

        }
        private IEnumerable<WallType> walltypes;
        public virtual IEnumerable<WallType> WallTypes
        {
            get
            {
                if (walltypes == null)
                {
                    walltypes = TypeElements.Where(x => x is WallType).Cast<WallType>();
                }
                return walltypes;
            }
        }
        private IEnumerable<FamilySymbol> familysymbols;
        public virtual IEnumerable<FamilySymbol> FamilySymbols
        {
            get
            {
                if (familysymbols == null)
                {
                    familysymbols = TypeElements.Where(x => x is FamilySymbol).Cast<FamilySymbol>();
                }
                return familysymbols;
            }
        }
        private IEnumerable<FamilyInstance> familyinstances;
        public virtual IEnumerable<FamilyInstance> FamilyInstances
        {
            get
            {
                if (familyinstances == null)
                {
                    familyinstances = InstanceElements.Where(x => x is FamilyInstance).Cast<FamilyInstance>();
                }
                return familyinstances;
            }
        }
        private IEnumerable<FamilyInstance> familyinstancesinview;
        public virtual IEnumerable<FamilyInstance> FamilyInstancesInView
        {
            get
            {
                if (familyinstancesinview == null)
                {
                    familyinstancesinview = InstanceElementsInView.Where(x => x is FamilyInstance).Cast<FamilyInstance>();
                }
                return familyinstancesinview;
            }
        }
        private IEnumerable<FamilySymbol> columntypes;
        public virtual IEnumerable<FamilySymbol> ColumnTypes

        {
            get
            {
                if (columntypes == null)
                {
                    columntypes = FamilySymbols.Where(x => x.Category.Id.IntegerValue == (int)BuiltInCategory.OST_StructuralColumns);
                }
                return columntypes;
            }
        }
        private IEnumerable<FamilyInstance> columninstances;
        public virtual IEnumerable<FamilyInstance> ColumnInstances
        {
            get
            {
                if (columninstances == null)
                {
                    columninstances = FamilyInstances.Where(x => x.Category.Id.IntegerValue == (int)BuiltInCategory.OST_StructuralColumns);
                }
                return columninstances;
            }
        }
        private IEnumerable<FamilyInstance> framinginstances;
        public virtual IEnumerable<FamilyInstance> FramingInstances
        {
            get
            {
                if (framinginstances == null)
                {
                    framinginstances = FamilyInstances.Where(x => x.Category.Id.IntegerValue == (int)BuiltInCategory.OST_StructuralFraming);
                }
                return framinginstances;
            }
        }
        private IEnumerable<FamilyInstance> framinginstancesinview;
        public virtual IEnumerable<FamilyInstance> FramingInstancesInView
        {
            get
            {
                if (framinginstancesinview == null)
                {
                    framinginstancesinview = FamilyInstancesInView.Where(x => x.Category.Id.IntegerValue == (int)BuiltInCategory.OST_StructuralFraming);
                }
                return framinginstancesinview;
            }
        }
        private IEnumerable<FamilySymbol> framingtypes;
        public virtual IEnumerable<FamilySymbol> FramingTypes
        {
            get
            {
                if (framingtypes == null)
                {
                    framingtypes = FamilySymbols.Where(x => x.Category.Id.IntegerValue == (int)BuiltInCategory.OST_StructuralFraming);
                }
                return framingtypes;
            }
        }
        private IEnumerable<Floor> floorinstancesinview;
        public virtual IEnumerable<Floor> FloorInstancesInView
        {
            get
            {
                if (floorinstancesinview == null)
                {
                    floorinstancesinview = InstanceElementsInView.Where(x => x is Floor).Cast<Floor>();
                }
                return floorinstancesinview;
            }
        }
        private IEnumerable<View> views;
        public virtual IEnumerable<View> Views
        {
            get
            {
                if (views == null)
                {
                    views = InstanceElements.Where(x => x is View).Cast<View>();
                }
                return views;
            }
        }
        private IEnumerable<ViewFamilyType> viewfamilytypes;
        public virtual IEnumerable<ViewFamilyType> ViewFamilyTypes
        {
            get
            {
                if (viewfamilytypes == null)
                {
                    viewfamilytypes = TypeElements.Where(x => x is ViewFamilyType).Cast<ViewFamilyType>();
                }
                return viewfamilytypes;
            }

        }
        private IEnumerable<View> instanceviews;
        public virtual IEnumerable<View> InstanceViews
        {
            get
            {
                if (instanceviews == null)
                {
                    instanceviews = Views.Where(x => x is View && !(x as View).IsTemplate);
                }
                return instanceviews;
            }
        }
        private IEnumerable<View> templateviews;
        public virtual IEnumerable<View> TemplateViews
        {
            get
            {
                if (templateviews == null)
                {
                    templateviews = Views.Where(x => x is View && (x as View).IsTemplate);
                }
                return templateviews;
            }
        }
        private IEnumerable<Workset> userworksets;
        public virtual IEnumerable<Workset> UserWorksets
        {
            get
            {
                if (userworksets == null)
                {
                    userworksets = new FilteredWorksetCollector(Document).Where(x => x.Kind == WorksetKind.UserWorkset);
                }
                return userworksets;
            }
        }
        private IEnumerable<FillPatternElement> fillpatternelements;
        public virtual IEnumerable<FillPatternElement> FillPatternElements
        {
            get
            {
                if (fillpatternelements == null)
                {
                    fillpatternelements = InstanceElements.Where(x => x is FillPatternElement).Cast<FillPatternElement>();
                }
                return fillpatternelements;
            }
        }
        private IEnumerable<MultiReferenceAnnotationType> multirefeannotypes;
        public virtual IEnumerable<MultiReferenceAnnotationType> MultiRefAnnoTypes
        {
            get
            {
                if (multirefeannotypes == null)
                {
                    multirefeannotypes = TypeElements.Where(x => x is MultiReferenceAnnotationType).Cast<MultiReferenceAnnotationType>();
                }
                return multirefeannotypes;
            }
        }
        private IEnumerable<Rebar> rebars;
        public virtual IEnumerable<Rebar> Rebars
        {
            get
            {
                if (rebars == null)
                {
                    rebars = InstanceElements.Where(x => x is Rebar).Cast<Rebar>();
                }
                return rebars;
            }
        }
        private IEnumerable<RebarBarType> rebarBarTypes;
        public virtual IEnumerable<RebarBarType> RebarBarTypes
        {
            get
            {
                if(rebarBarTypes == null)
                {
                    rebarBarTypes = TypeElements.Where(x => x is RebarBarType).Cast<RebarBarType>();
                }
                return rebarBarTypes;
            }
        }
        private IEnumerable<RebarHookType> rebarHookTypes;
        public virtual IEnumerable<RebarHookType> RebarHookTypes
        {
            get
            {
                if(rebarHookTypes == null)
                {
                    rebarHookTypes = TypeElements.Where(x => x is RebarHookType).Cast<RebarHookType>();
                }
                return rebarHookTypes;
            }
        }

        //private IEnumerable<Rebar> columnrebars;
        //public virtual IEnumerable<Rebar> ColumnRebars
        //{
        //    get
        //    {
        //        if (columnrebars == null)
        //        {
        //            columnrebars = Rebars.Where(x => x.AsValue("Host Category").ValueNumber == (double)RebarHostCategory.StructuralColumn);
        //        }
        //        return columnrebars;
        //    }
        //}
        private IEnumerable<Rebar> rebarsinview;
        public virtual IEnumerable<Rebar> RebarsInView
        {
            get
            {
                if (rebarsinview == null)
                {
                    rebarsinview = instanceElementsInView.Where(x => x is Rebar).Cast<Rebar>();
                }
                return rebarsinview;
            }
        }

        //private IEnumerable<Model.Entity.Rebar> framingrebars;
        //public virtual IEnumerable<Model.Entity.Rebar> FramingRebars
        //{
        //    get
        //    {
        //        if (framingrebars == null)
        //        {
        //            framingrebars = MyownRebars.Where(x => x.AsValue("Host Category").ValueNumber == (double)RebarHostCategory.StructuralFraming);
        //        }
        //        return framingrebars;
        //    }
        //}
        //private IEnumerable<Model.Entity.Rebar> myownrebars;
        //public virtual IEnumerable<Model.Entity.Rebar> MyownRebars
        //{
        //    get
        //    {
        //        if(myownrebars == null)
        //        {
        //            myownrebars = InstanceElements.Where(x => x is Rebar || x is RebarInSystem)
        //                .Select(x=>
        //                {
        //                    if (x is Rebar) return new Model.Entity.Rebar(x as Rebar);
        //                    return new Model.Entity.Rebar(x as RebarInSystem);
        //                });
        //        }
        //        return myownrebars;
        //    }
        //}

        //private IEnumerable<Model.Entity.Rebar> floorrebars;

        //public virtual IEnumerable<Model.Entity.Rebar> FloorRebars
        //{
        //    get
        //    {
        //        if (floorrebars == null)
        //        {
        //            floorrebars = MyownRebars.Where(x => x.AsValue("Host Category").ValueNumber == (double)RebarHostCategory.Floor);
        //        }

        //        return floorrebars;


        //    }
        //}
        //private IEnumerable<ViewSheet> sheets;
        //public virtual IEnumerable<ViewSheet> Sheets
        //{
        //    get
        //    {
        //        if(sheets == null)
        //        {
        //            sheets = InstanceElements.Where(x => x is ViewSheet)
        //                .Where(x=>x.AsValue("Appears In Sheet List").ValueNumber == 1).Cast<ViewSheet>();
        //        }
        //        return sheets;
        //    }
        //}
    }
}
