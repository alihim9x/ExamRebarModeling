using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace SingleData
{
    public class FormData : NotifyClass
    {
        public static FormData Instance
        {
            get
            {
                return SingleTon.Instance.FormData;
            }
        }
        private Model.Form.CreateRebarForm createRebarForm;
        public Model.Form.CreateRebarForm CreateRebarForm
        {
            get
            {
                if(createRebarForm == null)
                {
                    createRebarForm = new Model.Form.CreateRebarForm
                    {
                        DataContext = this
                    };
                }
                return createRebarForm;
            }
        }
        private Model.ViewModel.ElementView elementView;
        public Model.ViewModel.ElementView ElementView
        {
            get
            {
                return elementView;
            }
            set
            {
                elementView = value;
                OnPropertyChanged();
            }
        }
         
    }
}
