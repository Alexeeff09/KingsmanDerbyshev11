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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
            CmbGender.ItemsSource = ClassHelper.EF.Context.Gender.ToList();
            CmbGender.DisplayMemberPath = "Title";
            CmbGender.SelectedIndex = 0;
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            // валидация
            if (string.IsNullOrWhiteSpace(TbLastName.Text))
            {
                MessageBox.Show("Поле Фамилия не заполнено");
                return;
            }

            if (string.IsNullOrWhiteSpace(TbFirstName.Text))
            {
                MessageBox.Show("Поле Имя не заполнено");
                return;
            }

            if (string.IsNullOrWhiteSpace(TbPhone.Text))
            {
                MessageBox.Show("Поле Телефон не заполнено");
                return;
            }

            if (string.IsNullOrWhiteSpace(PbPassword.Password))
            {
                MessageBox.Show("Поле Пароль не заполнено");
                return;
            }

            // добавление 
            DB.Client addClient = new DB.Client();
            addClient.Password = PbPassword.Password;
            addClient.Phone = TbPhone.Text;
            addClient.FirstName = TbFirstName.Text;
            addClient.LastName = TbLastName.Text;
            if (TbPatronymic.Text != string.Empty)
            {
                addClient.Patronymic = TbPatronymic.Text;
            }
            addClient.GenderID = (CmbGender.SelectedItem as DB.Gender).ID;

            ClassHelper.EF.Context.Client.Add(addClient);

            // сохранение
            ClassHelper.EF.Context.SaveChanges();

            MessageBox.Show("Пользователь успешно добавлен");
        }
        private void BtnAuth_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Authorization Authorization = new Authorization();
            Authorization.Show();
            this.Close();
        }
    }
}
