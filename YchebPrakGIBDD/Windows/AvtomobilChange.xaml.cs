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
    /// Логика взаимодействия для AvtomobilChange.xaml
    /// </summary>
    public partial class AvtomobilChange : Window
    {
        private Avtomobil selectedAvtomobil;
        public AvtomobilChange(Avtomobil selectedAvtomobil)
        {
            InitializeComponent();
            this.selectedAvtomobil = selectedAvtomobil;
            LoadComboBox();
            LoadTextBox();
        }
        private void LoadComboBox()
        {
            using (GIBDDEntities db = new GIBDDEntities())
            {
                var сvet_Mashini = db.Cvet_Mashini.ToList();
                Cvet_IDCB.ItemsSource = сvet_Mashini;
                Cvet_IDCB.DisplayMemberPath = "Nazvanie_Cveta_RU";
                Cvet_IDCB.SelectedValuePath = "Nomer_Cveta";

                var tip_Dvigatelia = db.Tip_Dvigatelia.ToList();
                Tip_Dvigatelia_IDCB.ItemsSource = tip_Dvigatelia;
                Tip_Dvigatelia_IDCB.DisplayMemberPath = "Tip_RU";
                Tip_Dvigatelia_IDCB.SelectedValuePath = "ID";

                var privod = db.Privod.ToList();
                Privod_IDCB.ItemsSource = privod;
                Privod_IDCB.DisplayMemberPath = "Nazvanie";
                Privod_IDCB.SelectedValuePath = "ID";

                var code_Regiona = db.Code_Regiona.ToList();
                Cod_Regiona_IDCB.ItemsSource = code_Regiona;
                Cod_Regiona_IDCB.DisplayMemberPath = "Nazvanie_RU";
                Cod_Regiona_IDCB.SelectedValuePath = "ID";
            }
        }

        private void LoadTextBox()
        {
            if (selectedAvtomobil != null)
            {
                VIN_nomerTB.Text = selectedAvtomobil.VIN_nomer;
                MarkaTB.Text = selectedAvtomobil.Marka;
                ModelTB.Text = selectedAvtomobil.Model;
                GodTB.Text = Convert.ToString(selectedAvtomobil.God);
                VesTB.Text = Convert.ToString(selectedAvtomobil.Ves);
                Cvet_IDCB.SelectedValue = selectedAvtomobil.Cvet_ID;
                Tip_Dvigatelia_IDCB.SelectedValue = selectedAvtomobil.Tip_Dvigatelia_ID;
                Privod_IDCB.SelectedValue = selectedAvtomobil.Privod_ID;
                Cod_Regiona_IDCB.SelectedValue = selectedAvtomobil.Cod_Regiona_ID;
            }
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы перешли на окно 'Изменение автомобиля'");
        }
        private void ButtonGlavnaia(object sender, RoutedEventArgs e)
        {
            Glavnaia glavnaia = new Glavnaia();
            glavnaia.Show();
            this.Close();
        }
        private void ButtonNazad(object sender, RoutedEventArgs e)
        {
            AvtomobilWindow avtomobilWindow = new AvtomobilWindow();
            avtomobilWindow.Show();
            this.Close();
        }

        private void ButtonChange(object sender, RoutedEventArgs e)
        {
            string vin_nomer = VIN_nomerTB.Text;
            string marka = MarkaTB.Text;
            string model = ModelTB.Text;
            string god = GodTB.Text;
            string ves = VesTB.Text;
            var selectedCvet_ID = Cvet_IDCB.SelectedItem as Cvet_Mashini;
            var selectedTip_Dvigatelia_ID = Tip_Dvigatelia_IDCB.SelectedItem as Tip_Dvigatelia;
            var selectedPrivod_ID = Privod_IDCB.SelectedItem as Privod;
            var selectedCod_Regiona_ID = Cod_Regiona_IDCB.SelectedItem as Code_Regiona;

            if (VIN_nomerTB.Text == null || MarkaTB.Text == null || ModelTB.Text == null
                || GodTB.Text == null || VesTB.Text == null || Cvet_IDCB.SelectedItem == null
                || Tip_Dvigatelia_IDCB.SelectedItem == null || Privod_IDCB.SelectedItem == null)
            {
                MessageBox.Show("Заполните поля! Все поля, кроме 'Код региона', обязательны",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(vin_nomer, @"^[A-Z0-9]+$"))
            {
                MessageBox.Show("ВИН-номер должен содержать только цифры и заглавные латинские буквы",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (vin_nomer.Length != 17)
            {
                MessageBox.Show("ВИН-номер должен содержать 17 символов",
                               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(marka, @"^[A-Z][a-z]*$"))
            {
                MessageBox.Show("Марка автомобиля должна начинаться с заглавной латинской буквы и содержать только латинские буквы",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (marka.Length < 4 || marka.Length > 25)
            {
                MessageBox.Show("Марка автомобиля должна содержать от 4 до 25 символов",
                               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(model, @"^[A-Za-z0-9\W_]+$"))
            {
                MessageBox.Show("Модель автомобиля должна содержать только цифры, латинские буквы и любые символы",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (model.Length < 2 || model.Length > 40)
            {
                MessageBox.Show("Модель автомобиля должна содержать от 2 до 40 символов",
                               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(god, @"^[0-9]+$"))
            {
                MessageBox.Show("Год должен содержать только цифры",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (god.Length != 4)
            {
                MessageBox.Show("Год должен содержать 4 символа",
                               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(ves, @"^[0-9]+$"))
            {
                MessageBox.Show("Вес автомобиля должен содержать только цифры. Укажите вес в кг",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ves.Length < 3 || ves.Length > 4)
            {
                MessageBox.Show("Вес автомобиля должен содержать от 3 до 4 символов",
                               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (GIBDDEntities db = new GIBDDEntities())
                {
                    var existingAvtomobil = db.Avtomobil.FirstOrDefault(u => u.ID == selectedAvtomobil.ID);
                    if (existingAvtomobil != null)
                    {
                        if (existingAvtomobil.VIN_nomer != vin_nomer ||
                            existingAvtomobil.Marka != marka ||
                            existingAvtomobil.Model != model ||
                            existingAvtomobil.God != int.Parse(god) ||
                            existingAvtomobil.Ves != int.Parse(ves) ||
                            existingAvtomobil.Cvet_ID != selectedCvet_ID.Nomer_Cveta ||
                            existingAvtomobil.Tip_Dvigatelia_ID != selectedTip_Dvigatelia_ID.ID ||
                            existingAvtomobil.Privod_ID != selectedPrivod_ID.ID ||
                            existingAvtomobil.Cod_Regiona_ID != selectedCod_Regiona_ID.ID)
                        {
                            existingAvtomobil.VIN_nomer = vin_nomer;
                            existingAvtomobil.Marka = marka;
                            existingAvtomobil.Model = model;
                            existingAvtomobil.God = int.Parse(god);
                            existingAvtomobil.Ves = int.Parse(ves);
                            existingAvtomobil.Cvet_ID = selectedCvet_ID.Nomer_Cveta;
                            existingAvtomobil.Tip_Dvigatelia_ID = selectedTip_Dvigatelia_ID.ID;
                            existingAvtomobil.Privod_ID = selectedPrivod_ID.ID;
                            existingAvtomobil.Cod_Regiona_ID = selectedCod_Regiona_ID.ID;

                            db.SaveChanges();
                            MessageBox.Show("Данные автомобиля успешно изменены", "Успех", 
                                            MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Нет изменений для сохранения", "Информация", 
                                            MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Не удалось найти выбранного водителя", "Ошибка", 
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
            AvtomobilWindow avtomobilWindow = new AvtomobilWindow();
            avtomobilWindow.Show();
            this.Close();
        }
    }
}
