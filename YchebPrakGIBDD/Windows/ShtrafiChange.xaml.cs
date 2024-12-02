using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using YchebPrakGIBDD.Entities;

namespace YchebPrakGIBDD.Windows
{
    /// <summary>
    /// Логика взаимодействия для ShtrafiChange.xaml
    /// </summary>
    public partial class ShtrafiChange : Window
    {
        private Shtrafi selectedShtrafi;
        public ShtrafiChange(Shtrafi selectedShtrafi)
        {
            InitializeComponent(); 
            this.selectedShtrafi = selectedShtrafi;
            LoadComboBox();
            LoadTextBox();
        }
        public class VYDisplayItem
        {
            public int ID { get; set; }
            public string DisplayValue { get; set; }
        }
        private void LoadComboBox()
        {
            using (GIBDDEntities db = new GIBDDEntities())
            {
                var VY = db.Voditelskoe_ydostoverenie
                            .Select(s => new VYDisplayItem
                            {
                                ID = s.ID,
                                DisplayValue = s.Seria + " " + s.Nomer
                            })
                            .ToList();
                VY_IDCB.ItemsSource = VY;

                var avtomobil = db.Avtomobil
                    .Select(s => new VYDisplayItem
                    {
                        ID = s.ID,
                        DisplayValue = s.VIN_nomer + " " + s.Marka + " " + s.Model
                    })
                    .ToList();
                Avtomobil_IDCB.ItemsSource = avtomobil;

                var statys_Shtrafa = db.Statys_Shtrafa.ToList();
                StatysCB.ItemsSource = statys_Shtrafa;
                StatysCB.DisplayMemberPath = "Nazvanie";
                StatysCB.SelectedValuePath = "ID";
            }
        }
        private void VY_IDCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedVY_ID = VY_IDCB.SelectedItem as VYDisplayItem;
            if (selectedVY_ID != null)
            {
                MessageBox.Show("Selected VY_ID: " + selectedVY_ID.ID);
            }
            else
            {
                MessageBox.Show("No selection made VY_ID.");
            }
            var selectedAvtomobil_ID = Avtomobil_IDCB.SelectedItem as VYDisplayItem;
            if (selectedAvtomobil_ID != null)
            {
                MessageBox.Show("Selected Avtomobil_ID : " + selectedAvtomobil_ID.ID);
            }
            else
            {
                MessageBox.Show("No selection made Avtomobil_ID.");
            }
        }

        private void LoadTextBox()
        {
            if (selectedShtrafi != null)
            {
                VY_IDCB.SelectedValue = selectedShtrafi.VY_ID;
                Avtomobil_IDCB.SelectedValue = selectedShtrafi.Avtomobil_ID;
                StatysCB.SelectedValue = selectedShtrafi.Statys_ID;
                SymmaTB.Text = selectedShtrafi.Symma;
            }
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы перешли на окно 'Изменение штрафа'");
        }
        private void ButtonNazad(object sender, RoutedEventArgs e)
        {
            ShtrafiWindow shtrafiWindow = new ShtrafiWindow();
            shtrafiWindow.Show();
            this.Close();
        }
        private void ButtonChange(object sender, RoutedEventArgs e)
        {
            var selectedVY_ID = VY_IDCB.SelectedItem as VYDisplayItem;
            var selectedAvtomobil_ID = Avtomobil_IDCB.SelectedItem as VYDisplayItem;
            var selectedStatys_ID = StatysCB.SelectedItem as Statys_Shtrafa;
            string symma = SymmaTB.Text;

            if (selectedVY_ID == null || selectedAvtomobil_ID == null
                || selectedStatys_ID == null || SymmaTB.Text == null)
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
            Shtrafi existingShtrafi = null;
            try
            {
                using (GIBDDEntities db = new GIBDDEntities())
                {
                    existingShtrafi = db.Shtrafi.FirstOrDefault(u => u.ID == selectedShtrafi.ID);
                    if (existingShtrafi != null)
                    {
                        if (existingShtrafi.VY_ID != selectedVY_ID.ID ||
                            existingShtrafi.Avtomobil_ID != selectedAvtomobil_ID.ID ||
                            existingShtrafi.Statys_ID != selectedStatys_ID.ID ||
                            existingShtrafi.Symma != symma)
                        {
                            existingShtrafi.VY_ID = selectedVY_ID.ID;
                            existingShtrafi.Avtomobil_ID = selectedAvtomobil_ID.ID;
                            existingShtrafi.Statys_ID = selectedStatys_ID.ID;
                            existingShtrafi.Symma = symma;

                            db.SaveChanges();
                            MessageBox.Show("Данные о штрафе успешно изменены",
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
                        MessageBox.Show("Не удалось найти данные о штрафе", "Ошибка",
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
            ShtrafiWindow shtrafiWindow = new ShtrafiWindow();
            shtrafiWindow.Show();
            this.Close();
        }
    }
}
