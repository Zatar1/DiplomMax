using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        private void BtnLogIn_Click(object sender, RoutedEventArgs e)
        {
            foreach(var user in DaliAftotransEntities.GetContext().Users.ToList())
            {
                if(UserLogin.Text==user.Login && UserPassword.Text == user.Password)
                {
                    Manager.MainFrame.Navigate(new PostupleniaPage(user));
                    break;
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");      
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UserLogin.Text = null;
            UserPassword.Text = null;
        }
    }
}
