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
    public partial class VoditeliAddPage : Page
    {
        private Voditeli _currentVoditel = new Voditeli();
        public VoditeliAddPage(Voditeli selectVoditel)
        {
            InitializeComponent();
            if (selectVoditel != null)
                _currentVoditel = selectVoditel;
            DataContext = _currentVoditel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите сохранить?", "Внимание!!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                StringBuilder errors = new StringBuilder();

                if (string.IsNullOrWhiteSpace(_currentVoditel.F))
                    errors.AppendLine("Укажите Фамилию");

                if (string.IsNullOrWhiteSpace(_currentVoditel.I))
                    errors.AppendLine("Укажите Имя");

                if (string.IsNullOrWhiteSpace(_currentVoditel.AutoPrava))
                    errors.AppendLine("Укажите Номер водительский удостовирение");
                foreach(var item in Autoprava.Text)

                if (!char.IsDigit(item))
                    errors.AppendLine("Водительского удостоверение не может содержать символы");
                    
                if(Autoprava.Text.Length!=10)
                    errors.AppendLine("Длина водительского удостоверения должа быть 10 чисел");

                if (_currentVoditel.IdVoditeli == 0)
                    DaliAftotransEntities.GetContext().Voditelis.Add(_currentVoditel);

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }

                try
                {
                    DaliAftotransEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
