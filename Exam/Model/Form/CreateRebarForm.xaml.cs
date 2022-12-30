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
using Utility;

namespace Model.Form
{
    /// <summary>
    /// Interaction logic for CreateRebarForm.xaml
    /// </summary>
    public partial class CreateRebarForm : Window
    {
        public CreateRebarForm()
        {
            InitializeComponent();

        }

        private void PickElement_CLick(object sender, RoutedEventArgs e)
        {
            CreateRebarFormUtil.PickElement();
        }

        private void CreateRebar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
