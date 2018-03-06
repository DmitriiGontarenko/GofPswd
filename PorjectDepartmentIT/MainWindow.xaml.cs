using PorjectDepartmentIT.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace PorjectDepartmentIT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public IDbConnection Connection { get; set; }

        private void AppWindow_Loaded(object sender, RoutedEventArgs e) // Выполняется при запуске окна
        {
            IDbConnection _connection = new SqlConnection(Settings.Default.DbConnect);
            Connection = _connection;

            // Скрывает все label и textBox пока не открыта ни одна таблица
            lbl_attention.Visibility = Visibility.Hidden;
            lbl_attention_user.Visibility = Visibility.Hidden;
            lbl_name_user.Visibility = Visibility.Hidden; // Скрывает лейбел с Логином
            lbl_Welcome.Visibility = Visibility.Hidden; // Скрывает лейбел Велком

            lbl_Colum1.Visibility = Visibility.Hidden;
            lbl_Colum2.Visibility = Visibility.Hidden;
            lbl_Colum3.Visibility = Visibility.Hidden;
            lbl_Colum4.Visibility = Visibility.Hidden;
            lbl_Colum5.Visibility = Visibility.Hidden;

            tb_Colum1.Visibility = Visibility.Hidden;
            tb_Colum2.Visibility = Visibility.Hidden;
            tb_Colum3.Visibility = Visibility.Hidden;
            tb_Colum4.Visibility = Visibility.Hidden;
            tb_Colum5.Visibility = Visibility.Hidden;


            lbl_connection.Content = "Состояние соеденения: " + Connection.State; // Показывает статус соеденения


            #region Взаимодействие с окном Login

            // Открытие окна Login
            Login auth = new Login();
            auth.ShowDialog();

            lbl_name_user.Visibility = Visibility.Visible; // Показывает лейбел с Логином пользователя
            lbl_Welcome.Visibility = Visibility.Visible; // Показывает лейбел Велком
            lbl_Welcome.Content = "Вход выполнен: ";
            this.lbl_name_user.Content = auth.comboBox.Text; // Передает значения comboBox формы Login в label главной форме

            
            switch (Convert.ToString(lbl_name_user.Content)) // В зависимости от выбранного Логина отображает панели на главной форме
            {
                //case "Admin": user_Panel.Visibility = Visibility.Visible; // Если в программу зашел Администратор, то панель все панели открываются
                //    break;

                case "Master": // Если в программу зашел Мастер, то панель Пользователя скрывается
                    user_Panel.Visibility = Visibility.Hidden;
                    break;

                //case "Ucer": // Если в программу зашел Юзер, то панель Администратора скрывается
                //    break;

                //default: // Открыть комментарий после окончания работы с кодом
                //    MessageBox.Show("Передача username");
                //    break;
            }

            #endregion

        }

        private void AppWindow_Closed(object sender, EventArgs e) // Выполняется при закрытии окна программы
        {

            if (Connection != null && Connection.State != ConnectionState.Closed) // Если подключение не закрыто
                Connection.Close();                                                // Подключение закрывается
        }

        private void Open_btn_Click(object sender, RoutedEventArgs e) // При нажатии кнопки ADD выполняется функция открытия таблицы, если она выбрана в textBox
        {
            //IDbConnection _connection = Connection; // Создается переменная типа IDConnection _connection, которая получает ссылку из свойства Connection
            //int index = comboBox.SelectedIndex; // Index получает состояние ComboBox(значения 0,1,2..)
            //myDataGrid.ItemsSource = dbconnection(_connection, index); // Передает результат метода(читает таблицу) в свойства(отображает) DataGrid и выводит

            //label.Content = "Состояние соеденения: " + Connection.State; // Передает текующее состояния подключния
        }

        private void add_btn_Click(object sender, RoutedEventArgs e) // Кнопка добавления записи администратором 
        {
            // Создаем переменные для столбцов
            string var_Colum1 = "";
            string var_Colum2 = "";
            string var_Colum3 = "";
            string var_Colum4 = "";
            string var_Colum5 = "";



            // Первый проверяет, является ли строка нулевой (null) или пустой (Length == 0), а второй проверяет, является ли строка нулевой или состоит из одних пробельных символов.
            if (!string.IsNullOrEmpty(tb_Colum1.Text) && !string.IsNullOrWhiteSpace(tb_Colum1.Text) &&
               !string.IsNullOrEmpty(tb_Colum2.Text) && !string.IsNullOrWhiteSpace(tb_Colum2.Text))
            {
                // Присвоение переменным значений из textBox`ов
                var_Colum1 = tb_Colum1.Text;
                var_Colum2 = tb_Colum2.Text;
                var_Colum3 = tb_Colum3.Text;
                var_Colum4 = tb_Colum4.Text;
                var_Colum5 = tb_Colum5.Text;



                IDbConnection _connection = Connection;

                int index = comboBox.SelectedIndex; // Index получает состояние ComboBox(значения 0,1,2..)
                myDataGrid.ItemsSource = adddb(_connection, index, var_Colum1, var_Colum2, var_Colum3, var_Colum4, var_Colum5); // Передает результат метода(читает таблицу) в свойства(отображает) DataGrid и выводит


                // Отчищает textBox`ы после добавления записи в таблицу
                tb_id.Clear();
                tb_Colum1.Clear();
                tb_Colum2.Clear();
                tb_Colum3.Clear();
                tb_Colum4.Clear();
                tb_Colum5.Clear();

                lbl_attention.Visibility = Visibility.Hidden; // Скрывает надпись "Все поля должны быть заполнены" после добавления новой записи
            }

            else
            {
                lbl_attention.Visibility = Visibility.Visible; // Открывает надпись "Все поля должны быть заполнены" если поля не заполнены
                lbl_attention.Content = "Все поля должны быть заполнены";
            }



            lbl_connection.Content = "Состояние соеденения: " + Connection.State; // Передает текующее состояния подключния

        }

        private void us_add_btn_Click(object sender, RoutedEventArgs e) // Кнопка добавления записи пользователем 
        {

            // Создаем переменные для столбцов пользователя
            string var_us_Colum1 = "";
            string var_us_Colum2 = "";
            string var_us_Colum3 = "";

            if (!string.IsNullOrEmpty(u_tb_Colum1.Text) && !string.IsNullOrWhiteSpace(u_tb_Colum1.Text) &&
                !string.IsNullOrEmpty(u_tb_Colum2.Text) && !string.IsNullOrWhiteSpace(u_tb_Colum2.Text) &&
                !string.IsNullOrEmpty(u_tb_Colum3.Text) && !string.IsNullOrWhiteSpace(u_tb_Colum3.Text))
            {
                // ... Пользовательские поля
                var_us_Colum1 = u_tb_Colum1.Text;
                var_us_Colum2 = u_tb_Colum2.Text;
                var_us_Colum3 = u_tb_Colum3.Text;


                IDbConnection _connection = Connection;

                int index = comboBox.SelectedIndex; // Index получает состояние ComboBox(значения 0,1,2..)
                myDataGrid.ItemsSource = us_adddb(_connection, index, var_us_Colum1, var_us_Colum2, var_us_Colum3); // Передает результат метода(читает таблицу) в свойства(отображает) DataGrid и выводит

                // Отчищает textBox`ы после добавления записи в таблицу
                u_tb_Colum1.Clear();
                u_tb_Colum2.Clear();
                u_tb_Colum3.Clear();

                lbl_attention_user.Visibility = Visibility.Hidden; // Скрывает надпись "Все поля должны быть заполнены" после добавления новой записи
            }

            else
            {
                lbl_attention_user.Visibility = Visibility.Visible; // Открывает надпись "Все поля должны быть заполнены" если поля не заполнены
                lbl_attention_user.Content = "Все поля должны быть заполнены";
            }
        }

        private void update_btn_Click(object sender, RoutedEventArgs e) // Кнопка обновления записи 
        {
            // Создаем переменные для столбцов
            string var_Colum0 = "";
            string var_Colum1 = "";
            string var_Colum2 = "";
            string var_Colum3 = "";
            string var_Colum4 = "";
            string var_Colum5 = "";


            // Присвоение переменным значений из textBox`ов
            var_Colum0 = tb_id.Text;
            var_Colum1 = tb_Colum1.Text;
            var_Colum2 = tb_Colum2.Text;
            var_Colum3 = tb_Colum3.Text;
            var_Colum4 = tb_Colum4.Text;
            var_Colum5 = tb_Colum5.Text;


            IDbConnection _connection = Connection;

            int index = comboBox.SelectedIndex; // Index получает состояние ComboBox(значения 0,1,2..)
            myDataGrid.ItemsSource = updatedb(_connection, index, var_Colum0, var_Colum1, var_Colum2, var_Colum3, var_Colum4, var_Colum5); // Передает результат метода(читает таблицу) в свойства(отображает) DataGrid и выводит

            // Отчищает textBox`ы после обновления
            tb_id.Clear();
            tb_Colum1.Clear();
            tb_Colum2.Clear();
            tb_Colum3.Clear();
            tb_Colum4.Clear();
            tb_Colum5.Clear();
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e) // Кнопка удаления записи 
        {
            // Создаем переменные для столбцов
            string var_Colum0 = "";

            // Присвоение переменным значений из textBox`ов
            var_Colum0 = tb_id.Text;

            IDbConnection _connection = Connection;

            int index = comboBox.SelectedIndex; // Index получает состояние ComboBox(значения 0,1,2..)
            myDataGrid.ItemsSource = deletedb(_connection, index, var_Colum0); // Передает результат метода(читает таблицу) в свойства(отображает) DataGrid и выводит
        }

        private void Reset_btn_Click(object sender, RoutedEventArgs e) // Кнопка отчистки значений текст-боксов на панели Администратора 
        {
            tb_id.Clear();
            tb_Colum1.Clear();
            tb_Colum2.Clear();
            tb_Colum3.Clear();
            tb_Colum4.Clear();
            tb_Colum5.Clear();
        }

        private void us_Reset_btn_Click(object sender, RoutedEventArgs e) // Кнопка отчистки значений текст-боксов на панели Юзера 
        {
            u_tb_Colum1.Clear();
            u_tb_Colum2.Clear();
            u_tb_Colum3.Clear();

        }

        private void myDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) // Функция выполняющая передачу значений из таблицы в textBox`ы 
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                switch (comboBox.SelectedIndex) // Имена колонок заданы в зависимости от выбранной таблицы в comboBox
                {
                    case 0: // TestTb
                        tb_Colum1.Text = dr["Name"].ToString();
                        tb_Colum2.Text = dr["Surname"].ToString();
                        tb_Colum3.Text = dr["RegistrationDate"].ToString();
                        break;
                    case 1: // Employees
                        tb_id.Text = dr["IdEmp"].ToString();
                        tb_Colum1.Text = dr["Name"].ToString();
                        tb_Colum2.Text = dr["Surname"].ToString();
                        tb_Colum3.Text = dr["Patronymic"].ToString();
                        tb_Colum4.Text = dr["Position"].ToString();
                        break;
                    case 2: // PPC
                        tb_id.Text = dr["PPCId"].ToString();
                        tb_Colum1.Text = dr["RegNumber"].ToString();
                        tb_Colum2.Text = dr["PcInformation"].ToString();
                        tb_Colum3.Text = dr["Model"].ToString();
                        tb_Colum4.Text = dr["PcParameters"].ToString();
                        tb_Colum5.Text = dr["Location"].ToString();
                        break;
                    case 3: // SubDivision
                        tb_id.Text = dr["SDId"].ToString();
                        tb_Colum1.Text = dr["NameSD"].ToString();
                        tb_Colum2.Text = dr["Housing"].ToString();
                        tb_Colum3.Text = dr["Phone"].ToString();
                        break;
                    case 4: // Order
                        tb_id.Text = dr["OrderID"].ToString();
                        tb_Colum1.Text = dr["Status"].ToString();
                        tb_Colum2.Text = dr["Staff"].ToString();
                        break;
                    case 5: // Staff
                        tb_id.Text = dr["SfId"].ToString();
                        tb_Colum1.Text = dr["Name"].ToString();
                        tb_Colum2.Text = dr["Surname"].ToString();
                        tb_Colum3.Text = dr["Patronymic"].ToString();
                        tb_Colum4.Text = dr["Position"].ToString();
                        break;
                    default:
                        break;
                }

            }
        }

        private void comboBox_DropDownClosed(object sender, EventArgs e) // Функция выполняется при выборе любого элемента из выпадающего списка comboBox
        {

            IDbConnection _connection = Connection; // Создается переменная типа IDConnection _connection, которая получает ссылку из свойства Connection
            int index = comboBox.SelectedIndex; // Index получает состояние ComboBox(значения 0,1,2..)
            myDataGrid.ItemsSource = dbconnection(_connection, index); // Передает результат метода(читает таблицу) в свойства(отображает) DataGrid и выводит

            lbl_connection.Content = "Состояние соеденения: " + Connection.State; // Передает текующее состояния подключния

            lbl_attention.Visibility = Visibility.Hidden; // Скрывает надпись "Все поля должны быть заполнены" при открытии новой таблицы

            // Отчищает textBox при открытии новой таблицы
            tb_Colum1.Clear();
            tb_Colum2.Clear();
            tb_Colum3.Clear();
            tb_Colum4.Clear();
            tb_Colum5.Clear();

            #region Скрытие/открытие label и textBox в зависимости от выбранной таблицы

            switch (comboBox.SelectedIndex)
            {
                case 0: // TestTb
                    lbl_Colum3.Visibility = Visibility.Hidden;
                    lbl_Colum4.Visibility = Visibility.Hidden;
                    lbl_Colum5.Visibility = Visibility.Hidden;

                    tb_Colum3.Visibility = Visibility.Hidden;
                    tb_Colum4.Visibility = Visibility.Hidden;
                    tb_Colum5.Visibility = Visibility.Hidden;

                    lbl_Colum1.Visibility = Visibility.Visible;
                    lbl_Colum2.Visibility = Visibility.Visible;

                    tb_Colum1.Visibility = Visibility.Visible;
                    tb_Colum2.Visibility = Visibility.Visible;

                    lbl_Colum1.Content = "Name";
                    lbl_Colum2.Content = "Surname";
                    break;

                case 1: // Employees
                    lbl_Colum5.Visibility = Visibility.Hidden;

                    tb_Colum5.Visibility = Visibility.Hidden;

                    lbl_Colum1.Visibility = Visibility.Visible;
                    lbl_Colum2.Visibility = Visibility.Visible;
                    lbl_Colum3.Visibility = Visibility.Visible;
                    lbl_Colum4.Visibility = Visibility.Visible;

                    tb_Colum1.Visibility = Visibility.Visible;
                    tb_Colum2.Visibility = Visibility.Visible;
                    tb_Colum3.Visibility = Visibility.Visible;
                    tb_Colum4.Visibility = Visibility.Visible;

                    lbl_Colum1.Content = "Name";
                    lbl_Colum2.Content = "Surname";
                    lbl_Colum3.Content = "Patronymic";
                    lbl_Colum4.Content = "Position";
                    break;

                case 2: // PPC
                    lbl_Colum1.Visibility = Visibility.Visible;
                    lbl_Colum2.Visibility = Visibility.Visible;
                    lbl_Colum3.Visibility = Visibility.Visible;
                    lbl_Colum4.Visibility = Visibility.Visible;
                    lbl_Colum5.Visibility = Visibility.Visible;

                    tb_Colum1.Visibility = Visibility.Visible;
                    tb_Colum2.Visibility = Visibility.Visible;
                    tb_Colum3.Visibility = Visibility.Visible;
                    tb_Colum4.Visibility = Visibility.Visible;
                    tb_Colum5.Visibility = Visibility.Visible;

                    lbl_Colum1.Content = "RegNumber";
                    lbl_Colum2.Content = "PcInformation";
                    lbl_Colum3.Content = "Model";
                    lbl_Colum4.Content = "PcParameters";
                    lbl_Colum5.Content = "Location";
                    break;

                case 3: // SubDivision
                    lbl_Colum4.Visibility = Visibility.Hidden;
                    lbl_Colum5.Visibility = Visibility.Hidden;

                    tb_Colum4.Visibility = Visibility.Hidden;
                    tb_Colum5.Visibility = Visibility.Hidden;

                    lbl_Colum1.Visibility = Visibility.Visible;
                    lbl_Colum2.Visibility = Visibility.Visible;
                    lbl_Colum3.Visibility = Visibility.Visible;

                    tb_Colum1.Visibility = Visibility.Visible;
                    tb_Colum2.Visibility = Visibility.Visible;
                    tb_Colum3.Visibility = Visibility.Visible;

                    lbl_Colum1.Content = "NameSD";
                    lbl_Colum2.Content = "Housing";
                    lbl_Colum3.Content = "Phone";
                    break;

                case 4: // Order
                    lbl_Colum1.Visibility = Visibility.Visible;
                    lbl_Colum2.Visibility = Visibility.Visible;

                    lbl_Colum3.Visibility = Visibility.Hidden;
                    lbl_Colum4.Visibility = Visibility.Hidden;
                    lbl_Colum5.Visibility = Visibility.Hidden;

                    tb_Colum1.Visibility = Visibility.Visible;
                    tb_Colum2.Visibility = Visibility.Visible;

                    tb_Colum3.Visibility = Visibility.Hidden;
                    tb_Colum4.Visibility = Visibility.Hidden;
                    tb_Colum5.Visibility = Visibility.Hidden;

                    lbl_Colum1.Content = "Status";
                    lbl_Colum2.Content = "Staff";
                    break;

                case 5: // Staff
                    lbl_Colum5.Visibility = Visibility.Hidden;

                    tb_Colum5.Visibility = Visibility.Hidden;

                    lbl_Colum1.Visibility = Visibility.Visible;
                    lbl_Colum2.Visibility = Visibility.Visible;
                    lbl_Colum3.Visibility = Visibility.Visible;
                    lbl_Colum4.Visibility = Visibility.Visible;

                    tb_Colum1.Visibility = Visibility.Visible;
                    tb_Colum2.Visibility = Visibility.Visible;
                    tb_Colum3.Visibility = Visibility.Visible;
                    tb_Colum4.Visibility = Visibility.Visible;

                    lbl_Colum1.Content = "Name";
                    lbl_Colum2.Content = "Surname";
                    lbl_Colum3.Content = "Patronymic";
                    lbl_Colum4.Content = "Position";
                    break;

                default:
                    MessageBox.Show("Цикл switch, появление tb & lbl");
                    break;
            }
            #endregion

        }

        /* Список задач:

        отоброжение доступных txtbox-ов для ввода данных в зависимости от выбран. таблицы
        написать label для txtbox-ов каждый таблицы которые будут изменяться в зависимости от выбр.табл.

        Написать метод который будет изменять цвет(индикация в таблице при выводе)/оповещать польз.о просроченных заявках(по истечении 4 дней). 
        Сделать новую таблицу о невыполненых сроках заявках

        Добавить фильтр строк в DataGrid

        Добавить проверку на пустные значения textBoxпри добавлении в таблицу

        Убрать тестовую таблицу во всем коде

        Сделать в окне Login так, что бы при нажатии кнопки закрытия закрывалась вся программа

         */


    }
}
