using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SingleData;
using Utility;

namespace Model.Form
{
    /// <summary>
    /// Interaction logic for InputForm.xaml
    /// </summary>
    public partial class InputForm : Window
    {
        public InputForm()
        {
            InitializeComponent();
        }

        private void SelectElement_Click(object sender, RoutedEventArgs e)
        {
            InputFormUtil.SelectElement();
        }


        private void AddRebar_Click(object sender, RoutedEventArgs e)
        {
            InputFormUtil.AddRebar();
        }

        private void CreateRebars_Click(object sender, RoutedEventArgs e)
        {
            var elementView = FormData.Instance.ElementView.EttElement;
            ElementUtil.CreateRebars(elementView);
        }
        private void DeleterRebars_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            var rebarView = button.DataContext as Model.ViewModel.RebarView;
            InputFormUtil.DeleteRebars(rebarView);
        }

        private void AddStirrup_Click(object sender, RoutedEventArgs e)
        {
            InputFormUtil.AddStirrup();
            
        }
        private void DeleterStirrup_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            var stirrupView = button.DataContext as Model.ViewModel.StirrupView;
            InputFormUtil.DeleteStirrup(stirrupView);
        }
    }
}
