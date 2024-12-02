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
    /// Логика взаимодействия для VYChange.xaml
    /// </summary>
    public partial class VYChange : Window
    {
        private Voditelskoe_ydostoverenie selectedVY;
        public VYChange(Voditelskoe_ydostoverenie selectedVY)
        {
            InitializeComponent();
            this.selectedVY = selectedVY;
            LoadComboBox();
            LoadTextBox();
        }
        private void LoadComboBox()
        {
            using (GIBDDEntities db = new GIBDDEntities())
            {
                var voditel = db.Voditel.ToList();
                Voditel_IDCB.ItemsSource = voditel;
                Voditel_IDCB.DisplayMemberPath = "Familia";
                Voditel_IDCB.SelectedValuePath = "ID";
            }
        }

        private void LoadTextBox()
        {
            if (selectedVY != null)
            {
                Data_polychTB.Text = selectedVY.Data_polych;
                Data_okonchTB.Text = selectedVY.Data_okonch;
                Voditel_IDCB.SelectedValue = selectedVY.Voditel_ID;
                SeriaTB.Text = Convert.ToString(selectedVY.Seria);
                NomerTB.Text = Convert.ToString(selectedVY.Nomer);
            }
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы перешли на окно 'Изменение водительского удостоверения'");
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

        private void ButtonChange(object sender, RoutedEventArgs e)
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
                    var existingVY = db.Voditelskoe_ydostoverenie.FirstOrDefault(u => u.ID == selectedVY.ID);
                    if (existingVY != null)
                    {
                        if (existingVY.Data_polych != data_polych ||
                            existingVY.Data_okonch != data_okonch ||
                            existingVY.Voditel_ID != selectedVoditel_ID.ID ||
                            existingVY.Seria != seria ||
                            existingVY.Nomer != nomer)
                        {
                            existingVY.Data_polych = data_polych;
                            existingVY.Data_okonch = data_okonch;
                            existingVY.Voditel_ID = selectedVoditel_ID.ID;
                            existingVY.Seria = seria;
                            existingVY.Nomer = nomer;

                            db.SaveChanges();
                            MessageBox.Show("Данные водительского удостоверения успешно изменены", 
                                            "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Нет изменений для сохранения", "Информация",
                                            MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Не удалось найти выбранное водительское удостоверение", "Ошибка",
                                        MessageBoxButton.OK, MessageBoxImage.Error);
                    }
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
                MessageBox.Show("Произошла ошибка при сохранении изменений: " + sb.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            VYWindow vyWindow = new VYWindow();
            vyWindow.Show();
            this.Close();
        }
    }    
}
