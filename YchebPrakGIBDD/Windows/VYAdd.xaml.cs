using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using YchebPrakGIBDD.Entities;

namespace YchebPrakGIBDD.Windows
{
    /// <summary>
    /// Логика взаимодействия для VYAdd.xaml
    /// </summary>
    public partial class VYAdd : Window
    {
        public VYAdd()
        {
            InitializeComponent();
            LoadComboBoxData();
        }
        private void LoadComboBoxData()
        {
            using (GIBDDEntities db = new GIBDDEntities())
            {
                var voditel = db.Voditel.ToList();
                Voditel_IDCB.ItemsSource = voditel;
            }
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы перешли на окно 'Добавление водительского удостоверения'");
        }
        private void ButtonGlavnaia(object sender, RoutedEventArgs e)
        {
            Glavnaia glavnaia = new Glavnaia();
            glavnaia.Show();
            this.Close();
        }
        private void ButtonNazad(object sender, RoutedEventArgs e)
        {
            VYWindow vyWindow = new VYWindow();
            vyWindow.Show();
            this.Close();
        }
        private void ButtonAdd(object sender, RoutedEventArgs e)
        {
            string data_polych = Data_polychTB.Text;
            string data_okonch = Data_okonchTB.Text;            
            var selectedVoditel_ID = Voditel_IDCB.SelectedItem as Voditel;
            int seria = Convert.ToInt32(SeriaTB.Text);
            int nomer = Convert.ToInt32(NomerTB.Text);

            if (Data_polychTB.Text == null || Data_okonchTB.Text == null 
                || Voditel_IDCB.SelectedItem == null || SeriaTB.Text == null || NomerTB.Text == null)
            {
                MessageBox.Show("Заполните поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(data_polych, @"^\d{2}\.\d{2}\.\d{4}$") // Формат ДД.ММ.ГГГГ
                || !int.TryParse(data_polych.Split('.')[0], out int day) // День
                || !int.TryParse(data_polych.Split('.')[1], out int month) // Месяц
                || !int.TryParse(data_polych.Split('.')[2], out int year) // Год
                || year < 1920 || year > 2024 // Проверка года
                || month < 1 || month > 12 // Проверка месяца
                || day < 1 || day > DateTime.DaysInMonth(year, month)) // Проверка дня
            {
                MessageBox.Show("Дата получения введена некорректно. Убедитесь, что она в формате ДД.ММ.ГГГГ и соответствует календарю (год от 1920 до 2024).",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(data_okonch, @"^\d{2}\.\d{2}\.\d{4}$") // Формат ДД.ММ.ГГГГ
                || !int.TryParse(data_okonch.Split('.')[0], out int day1) // День
                || !int.TryParse(data_okonch.Split('.')[1], out int month1) // Месяц
                || !int.TryParse(data_okonch.Split('.')[2], out int year1) // Год
                || year1 < 1920 || year1 > 2040 // Проверка года
                || month1 < 1 || month1 > 12 // Проверка месяца
                || day1 < 1 || day1 > DateTime.DaysInMonth(year1, month1)) // Проверка дня
            {
                MessageBox.Show("Дата окончания введена некорректно. Убедитесь, что она в формате ДД.ММ.ГГГГ и соответствует календарю (год от 1920 до 2024).",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!(Convert.ToString(seria).Length == 4 && Convert.ToString(seria).All(char.IsDigit)))
            {
                MessageBox.Show("Серия состоит только из 4 цифр",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!(Convert.ToString(nomer).Length == 6 && Convert.ToString(nomer).All(char.IsDigit)))
            {
                MessageBox.Show("Номер состоит только из 6 цифр",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                using (GIBDDEntities db = new GIBDDEntities())
                {
                    int maxId = db.Voditelskoe_ydostoverenie.Any() ? db.Voditelskoe_ydostoverenie.Max(v 
                                => v.ID) : 0;

                    // Создаем новый объект Voditel
                    Voditelskoe_ydostoverenie vy = new Voditelskoe_ydostoverenie
                    {
                        ID = maxId + 1,
                        Data_polych = data_polych,
                        Data_okonch = data_okonch,
                        Voditel_ID = selectedVoditel_ID.ID,
                        Seria = seria,
                        Nomer = nomer
                    };

                    db.Voditelskoe_ydostoverenie.Add(vy);
                    db.SaveChanges();
                    MessageBox.Show("Водительское удостоверение успешно добавлено", "Успех",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        sb.AppendLine($"Property: {validationError.PropertyName}, Error: {validationError.ErrorMessage}");
                    }
                }
                MessageBox.Show("Произошла ошибка при сохранении изменений: " + sb.ToString(),
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
            VYWindow vyWindow = new VYWindow();
            vyWindow.Show();
            this.Close();
        }
    }
}