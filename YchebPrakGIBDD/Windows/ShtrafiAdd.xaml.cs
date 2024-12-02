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
    /// Логика взаимодействия для ShtrafiAdd.xaml
    /// </summary>
    public partial class ShtrafiAdd : Window
    {
        public ShtrafiAdd()
        {
            InitializeComponent();
            LoadComboBoxData();
        }
        private void LoadComboBoxData()
        {
            using (GIBDDEntities db = new GIBDDEntities())
            {
                var vy = db.Voditelskoe_ydostoverenie.ToList();
                VY_IDCB.ItemsSource = vy;
                var avtomobil_ID = db.Avtomobil.ToList();
                Avtomobil_IDCB.ItemsSource = avtomobil_ID;
                var statys_Shtrafa = db.Statys_Shtrafa.ToList();
                StatysCB.ItemsSource = statys_Shtrafa;
            }
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы перешли на окно 'Добавление штрафов'");
        }
        private void ButtonNazad(object sender, RoutedEventArgs e)
        {
            ShtrafiWindow shtrafiWindow = new ShtrafiWindow();
            shtrafiWindow.Show();
            this.Close();
        }
        private void ButtonAdd(object sender, RoutedEventArgs e)
        {
            var selectedVY_ID = VY_IDCB.SelectedItem as Voditelskoe_ydostoverenie;
            var selectedAvtomobil_ID = Avtomobil_IDCB.SelectedItem as Avtomobil;
            var statys_Shtrafa1 = StatysCB.SelectedItem as Statys_Shtrafa;
            string symma = SymmaTB.Text;

            if (VY_IDCB.SelectedItem == null || Avtomobil_IDCB.SelectedItem == null
                || statys_Shtrafa1 == null || SymmaTB.Text == null)
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, 
                                MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(symma, @"^\d+р\.$") // Формат числа с "р."
    || !int.TryParse(symma.TrimEnd('р', '.'), out int penalty) // Извлекаем число
    || penalty < 500 || penalty > 500000) // Проверка диапазона
            {
                MessageBox.Show("Штраф введен некорректно. Убедитесь, что сумма указана в формате 'Число'р., и находится в диапазоне от 500р. до 500000р.",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (GIBDDEntities db = new GIBDDEntities())
                {
                    int maxId = db.Shtrafi.Any() ? db.Shtrafi.Max(v
                                => v.ID) : 0;

                    // Создаем новый объект Voditel
                    Shtrafi shtrafi = new Shtrafi
                    {
                        ID = maxId + 1,
                        VY_ID = selectedVY_ID.ID,
                        Avtomobil_ID = selectedAvtomobil_ID.ID,
                        Statys_ID = statys_Shtrafa1.ID,
                        Symma = symma
                    };

                    db.Shtrafi.Add(shtrafi);
                    db.SaveChanges();
                    MessageBox.Show("Штраф успешно добавлен", "Успех",
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
            ShtrafiWindow shtrafiWindow = new ShtrafiWindow();
            shtrafiWindow.Show();
            this.Close();
        }
    }
}
