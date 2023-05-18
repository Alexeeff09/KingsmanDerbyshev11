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
using KingsmanDerbyshev.ClassHelper;


namespace KingsmanDerbyshev.Windows
{
    /// <summary>
    /// Логика взаимодействия для Service.xaml
    /// </summary>
    public partial class Service : Window
    {
            public Service()
            {
                InitializeComponent();
                GetListService();
            }
            private void GetListService()
            {
                LvService.ItemsSource = ClassHelper.EF.Context.Service.ToList();
            }
            private void BtnAddService_Click(object sender, RoutedEventArgs e)
            {
                AddService addServiceWindow = new AddService();
                addServiceWindow.ShowDialog();

                // Обновляем лист
                GetListService();
            }
            private void BtnEdit_Click(object sender, RoutedEventArgs e)
            {
                var button = sender as Button;
                if (button == null)
                {
                    return;
                }
                var service = button.DataContext as DB.Service;

                EditService editServiceWindow = new EditService(service);
                editServiceWindow.ShowDialog();

                GetListService();
            }
            private void BtnAddToCart_Click(object sender, RoutedEventArgs e)
            {
                var button = sender as Button;
                if (button == null)
                {
                    return;
                }
                var service = button.DataContext as DB.Service; // получаем выбранную запись


                CartServiceClass.ServiceCart.Add(service);

                MessageBox.Show($"Услуга {service.Title} добавлена в корзину!");
            }
            private void BtnGoToCart_Click(object sender, RoutedEventArgs e)
            {
                Cart cartWindow = new Cart();
                cartWindow.Show();
                this.Close();
            }
        }
    }

