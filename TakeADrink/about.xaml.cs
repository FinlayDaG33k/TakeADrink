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
using System.Windows.Shapes;

using MahApps.Metro.Controls; // To create a non-ancient looking MetroUI

namespace TakeADrink {
    /// <summary>
    /// Interaction logic for about.xaml
    /// </summary>
    public partial class about : MetroWindow {
        public about() {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e) {
            this.label8.Content = System.Diagnostics.FileVersionInfo.GetVersionInfo(Application.ResourceAssembly.Location).ProductVersion;
        }
    }
}
