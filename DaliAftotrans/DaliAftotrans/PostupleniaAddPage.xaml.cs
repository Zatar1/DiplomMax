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
    /// Логика взаимодействия для PostupleniaAddPage.xaml
    /// </summary>
    public partial class PostupleniaAddPage : Page
    {
        private Sredstva _currentPostuplenia = new Sredstva();
        public PostupleniaAddPage(User user, Sredstva selectedPostuplenia)
        {
            InitializeComponent();
            if (selectedPostuplenia != null)
            {
                _currentPostuplenia = selectedPostuplenia;
            }
            DataContext = _currentPostuplenia;
            VoditeliCB.ItemsSource = DaliAftotransEntities.GetContext().Voditelis.Where(p=>p.Active).ToList();
            _currentPostuplenia.IdUser = user.IdUser;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите сохранить?", "Внимание!!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                StringBuilder errors = new StringBuilder();
                if (MoneyText.Text.Any(Char.IsLetter))
                    errors.AppendLine("В зароботке не должно быть букв");
                if (DateText.Text.Any(Char.IsLetter))
                    errors.AppendLine("В дате не должно быть букв");
                if (_currentPostuplenia.Voditeli == null)
                    errors.AppendLine("Укажите водителя");

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }
                if (_currentPostuplenia.IdZapisi == 0)
                {
                    DaliAftotransEntities.GetContext().Sredstvas.Add(_currentPostuplenia);
                }
                try
                {
                    DaliAftotransEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные сохранены!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

        }
    }
}
