using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.Imaging;
using YchebPrakGIBDD.Entities;
using System.Data.Entity.Validation;

namespace YchebPrakGIBDD.Windows
{
    /// <summary>
    /// Логика взаимодействия для VoditelAdd.xaml
    /// </summary>
    public partial class VoditelAdd : Window
    {
        private string ImagePath;  // Теперь это строка, а не byte[]


        public VoditelAdd()
        {
            InitializeComponent();
            LoadComboBoxData();
        }
        private void LoadComboBoxData()
        {
            using (GIBDDEntities db = new GIBDDEntities())
            {
                var adres_prop = db.Adres.ToList();
                Adres_prop_IDCB.ItemsSource = adres_prop;

                var adres_proziv = db.Adres.ToList();
                Adres_proziv_IDCB.ItemsSource = adres_proziv;

                var mesto_raboti = db.Mesto_raboti.ToList();
                Mesto_raboti_IDCB.ItemsSource = mesto_raboti;
            }
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы перешли на окно 'Добавление водителя'");
        }
        private void ButtonGlavnaia(object sender, RoutedEventArgs e)
        {
            Glavnaia glavnaia = new Glavnaia();
            glavnaia.Show();
            this.Close();
        }
        private void ButtonNazad(object sender, RoutedEventArgs e)
        {
            VoditelWindow voditelWindow = new VoditelWindow();
            voditelWindow.Show();
            this.Close();
        }
        private void ButtonAddVoditel(object sender, RoutedEventArgs e)
        {
            string imia = ImiaTB.Text;
            string familia = FamiliaTB.Text;
            string vtoroe_Imia = Vtoroe_ImiaTB.Text;
            string seria_pasporta = Seria_pasportaTB.Text;
            string nomer_Pasporta = Nomer_PasportaTB.Text;
            var selectedAdres_prop = Adres_prop_IDCB.SelectedItem as Adres;
            var selectedAdres_proziv = Adres_proziv_IDCB.SelectedItem as Adres;
            var selectedMesto_rabotie = Mesto_raboti_IDCB.SelectedItem as Mesto_raboti;
            string telefon = TelefonTB.Text;
            string pochta = PochtaTB.Text;

            // Сохраняем путь к изображению
            string fotoPath = ImagePath;  // Путь к файлу изображения
            string opisanie = OpisanieTB.Text;

            if (ImiaTB.Text == null || FamiliaTB.Text == null || Vtoroe_ImiaTB.Text == null
                || Seria_pasportaTB.Text == null || Nomer_PasportaTB.Text == null
                || Adres_prop_IDCB.SelectedItem == null || Adres_proziv_IDCB.SelectedItem == null
                || Mesto_raboti_IDCB.SelectedItem == null || TelefonTB.Text == null
                || PochtaTB.Text == null || ImagePath == null)
            {
                MessageBox.Show("Заполните поля! Все поля, кроме 'Описание', обязательны",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(imia, @"^([A-Z][a-z]*|[А-ЯЁ][а-яё]*)$"))
            {
                MessageBox.Show("Проверьте корректность имени",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (imia.Length < 3 || imia.Length > 10)
            {
                MessageBox.Show("Имя может содержать от 3 до 10 символов",
                               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(familia, @"^([A-Z][a-z]*|[А-ЯЁ][а-яё]*)$"))
            {
                MessageBox.Show("Проверьте корректность фамилии",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (familia.Length < 3 || familia.Length > 10)
            {
                MessageBox.Show("Фамилия может содержать от 3 до 10 символов",
                               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(vtoroe_Imia, @"^([A-Z][a-z]*|[А-ЯЁ][а-яё]*)$"))
            {
                MessageBox.Show("Проверьте корректность второго имени",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (vtoroe_Imia.Length < 3 || vtoroe_Imia.Length > 10)
            {
                MessageBox.Show("Второе имя может содержать от 3 до 10 символов",
                               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!(seria_pasporta.Length == 4 && seria_pasporta.All(char.IsDigit)))
            {
                MessageBox.Show("Серия паспорта состоит только из 4 цифр",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!(nomer_Pasporta.Length == 6 && nomer_Pasporta.All(char.IsDigit)))
            {
                MessageBox.Show("Номер паспорта состоит только из 6 цифр",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!(telefon.Length == 11 && telefon.All(char.IsDigit)))
            {
                MessageBox.Show("Номер телефона состоит только из 11 цифр",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!(IsValidEmail(pochta)))
            {
                MessageBox.Show("Проверьте корректность адреса электронной почты",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (pochta.Length > 40)
            {
                MessageBox.Show("Электронной почты имя может содержать до 10 символов",
                               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            try
            {
                using (GIBDDEntities db = new GIBDDEntities())
                {
                    int maxId = db.Voditel.Any() ? db.Voditel.Max(v => v.ID) : 0;
                   
                    // Создаем новый объект Voditel
                    Voditel voditel = new Voditel
                    {
                        ID = maxId + 1,
                        Imia = imia,
                        Familia = familia,
                        Vtoroe_Imia = vtoroe_Imia,
                        Seria_pasporta = Convert.ToInt32(seria_pasporta),
                        Nomer_Pasporta = Convert.ToInt32(nomer_Pasporta),
                        Adres_prop_ID = selectedAdres_prop.ID,
                        Adres_proziv_ID = selectedAdres_proziv.ID,
                        Mesto_raboti_ID = selectedMesto_rabotie.ID,
                        Telefon = telefon,
                        Pochta = pochta,
                        Foto = "D:\\4 курс\\УЧ ПРАК\\3 ЗАДАНИЕ\\photo\\003-happy-17.png", // Сохраняем путь к изображению
                        Opisanie = opisanie
                    };

                    db.Voditel.Add(voditel);
                    db.SaveChanges();
                    MessageBox.Show("Водитель успешно добавлен", "Успех",
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
                MessageBox.Show("Произошла ошибка при сохранении изменений: " + sb.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            VoditelWindow voditelWindow = new VoditelWindow();
            voditelWindow.Show();
            this.Close();            
        }


        // Метод для получения расширения изображения
        static string GetImageExtension(byte[] imageData)
        {
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                try
                {
                    // Считываем первые 3 байта, чтобы определить формат
                    byte[] bytes = new byte[3];
                    ms.Read(bytes, 0, 3);
                    string hexString = BitConverter.ToString(bytes).Replace("-", string.Empty).ToLower();

                    // Проверяем по сигнатурам JPEG, PNG
                    if (hexString.StartsWith("ffd8")) // JPEG
                        return ".jpg";
                    else if (hexString.StartsWith("89504e47")) // PNG
                        return ".png";
                }
                catch
                {
                    // Ошибка при чтении или определении расширения
                    return "";
                }
            }

            return "";
        }

        static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }
        private void Button_AddFoto(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                // Отображаем выбранное изображение в Image элементе
                AddFoto.Source = new BitmapImage(new Uri(openFileDialog.FileName));

                // Сохраняем путь к изображению в переменную
                ImagePath = openFileDialog.FileName;
            }
        }
        private byte[] ReadImageFile(string filePath)
        {
            return File.ReadAllBytes(filePath);  // Чтение файла в байты
        }
    }
}
