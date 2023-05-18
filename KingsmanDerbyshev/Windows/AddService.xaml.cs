using Microsoft.Win32;
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

namespace KingsmanDerbyshev.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddServiceWindow.xaml
    /// </summary>
    public partial class AddService : Window
    {
        public AddService()
        {
            InitializeComponent();
            CmbStaff.ItemsSource = ClassHelper.EF.Context.Staff.ToList();
            CmbStaff.DisplayMemberPath = "FirstName";
            CmbStaff.SelectedIndex = 0;
        }
        private string Photo = null;
        private void BtnChooseImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                ImgImageService.Source = new BitmapImage(new Uri(openFileDialog.FileName));

                Photo = openFileDialog.FileName;
            }
        }

        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {
            DB.Service newService = new DB.Service();

            newService.Cost = Convert.ToDecimal(TbPriceService.Text);
            newService.Title = TbNameService.Text;
            newService.Description = TbDiscService.Text;
            newService.DurationInSeconds = Convert.ToInt32(TbDurationService.Text);
            if (Photo != null)
            {
                newService.Photo = Photo;
            }
            newService.StaffID = (CmbStaff.SelectedItem as DB.Staff).ID; ;

            ClassHelper.EF.Context.Service.Add(newService);
            ClassHelper.EF.Context.SaveChanges();

            MessageBox.Show("Услуга добавлена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
        }
    }
}
