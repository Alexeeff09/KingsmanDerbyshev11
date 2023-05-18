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
    /// Логика взаимодействия для EditServiceWindow.xaml
    /// </summary>
    public partial class EditService : Window
    {
        DB.Service editService = null;

        private bool isEdit = false;
        public EditService()
        {
            InitializeComponent();
            isEdit = false;
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
        public EditService(DB.Service service)
        {
            InitializeComponent();

            isEdit = true;

            editService = service;

            // Заполнение типа услуги

            CmbStaff.ItemsSource = ClassHelper.EF.Context.Staff.ToList();
            CmbStaff.DisplayMemberPath = "FirstName";

            // выгрузка изображения 
            ImgImageService.Source = new BitmapImage(new Uri(service.Photo));

            // заполнение полей
            TbNameService.Text = service.Title;
            TbDiscService.Text = service.Description;
            TbPriceService.Text = Convert.ToString(service.Cost);
            TbDurationService.Text = Convert.ToString(service.DurationInSeconds);

            // заполнение типа услуги
            CmbStaff.SelectedItem = ClassHelper.EF.Context.Staff.Where(i => i.ID == service.StaffID).FirstOrDefault();

        }
        private void BtnEditService_Click(object sender, RoutedEventArgs e)
        {


            // валидация

            editService.Title = TbNameService.Text;
            editService.Description = TbDiscService.Text;
            editService.Cost = Convert.ToDecimal(TbPriceService.Text);
            editService.StaffID = (CmbStaff.SelectedItem as DB.Staff).ID;
            editService.DurationInSeconds = Convert.ToInt32(TbDurationService.Text);

            ClassHelper.EF.Context.SaveChanges();

            MessageBox.Show("Данные успешно сохранны");

            this.Close();
        }
    }
}

