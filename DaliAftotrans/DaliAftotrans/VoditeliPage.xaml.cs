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

namespace DaliAftotrans
{
    /// <summary>
    /// Логика взаимодействия для VoditeliPage.xaml
    /// </summary>
    public partial class VoditeliPage : Page
    {
        public VoditeliPage()
        {
            InitializeComponent();
            DBGridVoditel.ItemsSource = DaliAftotransEntities.GetContext().Voditelis.ToList();
        }

        private void Redact_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new VoditeliAddPage((sender as Button).DataContext as Voditeli));
        }

        private void Btn_add_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new VoditeliAddPage(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DaliAftotransEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DBGridVoditel.ItemsSource = DaliAftotransEntities.GetContext().Voditelis.ToList();
            }
        }
    }
}
