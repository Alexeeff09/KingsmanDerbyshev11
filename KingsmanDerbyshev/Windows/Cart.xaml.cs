using KingsmanDerbyshev.ClassHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для Window4.xaml
    /// </summary>
    public partial class Cart : Window
    {
        public Cart()
        {
            InitializeComponent();
            GetListServise();
        }
        private void GetListServise()
        {
            ObservableCollection<DB.Service> listCart = new ObservableCollection<DB.Service>(ClassHelper.CartServiceClass.ServiceCart);
            LvCartService.ItemsSource = listCart;
        }
        private void BtnRomoveToCart_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }
            var service = button.DataContext as DB.Service; // получаем выбранную запись

            ClassHelper.CartServiceClass.ServiceCart.Remove(service);

            GetListServise();
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Service serviceWindow = new Service();
            serviceWindow.Show();
            this.Close();
        }
        private void BtnPay_Click(object sender, RoutedEventArgs e)
        {
            // покупка
            EF.Context.Order.Add(new DB.Order
            {
                ClientID = 1
            }
            );

            foreach (var item in ClassHelper.CartServiceClass.ServiceCart)
            {
                DB.OrderService orderService = new DB.OrderService();
                orderService.OrderID = 1;
                orderService.ServiceID = item.ID;

                EF.Context.OrderService.Add(orderService);
            }


            EF.Context.SaveChanges();
            // переход на главную

            this.Close();
        }
    }
}
