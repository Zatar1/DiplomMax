using Excel = Microsoft.Office.Interop.Excel;
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
using Microsoft.Win32;

namespace DaliAftotrans
{
    /// <summary>
    /// Логика взаимодействия для PostupleniaPage.xaml
    /// </summary>
    public partial class PostupleniaPage : Page
    {
        private User _currentUser = null;
        public PostupleniaPage(User user)
        {
            InitializeComponent();
            UpdatePostuplenia();
            TextData.DisplayDateEnd = DateTime.Now;
            _currentUser = user;

            if (user.IdUser != 1)
            {
                BtnDeletePostuplenia.Visibility = Visibility.Hidden;
                RedactCell.Visibility = Visibility.Hidden;
            }
            else
            {
                BtnDeletePostuplenia.Visibility = Visibility.Visible;
                RedactCell.Visibility = Visibility.Visible;
            }

        }

        private void ButtonAddPostuplenia_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PostupleniaAddPage(_currentUser, null));
        }

        private void BtnDeletePostuplenia_Click(object sender, RoutedEventArgs e)
        {
            var PostupleniaDel = DBGridPostuplenia.SelectedItems.Cast<Sredstva>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {PostupleniaDel.Count()} Элементов ? ", "Внимание",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    DaliAftotransEntities.GetContext().Sredstvas.RemoveRange(PostupleniaDel);
                    DaliAftotransEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    UpdatePostuplenia();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                DaliAftotransEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            }
        }

        private void TBoxSearching_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePostuplenia();
        }

        public void UpdatePostuplenia()
        {
            DaliAftotransEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            var currentPostuplenia = DaliAftotransEntities.GetContext().Sredstvas.ToList();
            if (TBoxSearching.Text != "")
                currentPostuplenia = currentPostuplenia.Where(p => p.Voditeli.FIO.ToLower().Contains(TBoxSearching.Text.ToLower())).ToList();
            if (TextData.Text != "")
                currentPostuplenia = currentPostuplenia.Where(p => p.Data.Equals(TextData.SelectedDate)).ToList();
            DBGridPostuplenia.ItemsSource = currentPostuplenia;
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdatePostuplenia();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new VoditeliPage());
        }

        private void TextData_DataContextChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePostuplenia();
        }

        private void Redact_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PostupleniaAddPage(_currentUser, (sender as Button).DataContext as Sredstva));
        }


        private void BtnOtch_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Excel.Worksheet sheet1 = (Excel.Worksheet)workbook.Sheets[1];

            for (int j = 0; j < DBGridPostuplenia.Columns.Count; j++) 
            {
                Excel.Range myRange = (Excel.Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true; 
                sheet1.Columns[j + 1].ColumnWidth = 15; 
                myRange.Value2 = DBGridPostuplenia.Columns[j].Header;
            }
            for (int i = 0; i < DBGridPostuplenia.Columns.Count-1; i++)
            { 
                for (int j = 0; j < DBGridPostuplenia.Items.Count; j++)
                {
                    TextBlock b = DBGridPostuplenia.Columns[i].GetCellContent(DBGridPostuplenia.Items[j]) as TextBlock;
                    Excel.Range myRange = (Excel.Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;
                }
            }
        }
    }
}
