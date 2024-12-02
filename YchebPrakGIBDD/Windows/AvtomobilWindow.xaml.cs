using System.Linq;
using System.Windows;
using YchebPrakGIBDD.Entities;

namespace YchebPrakGIBDD.Windows
{
    /// <summary>
    /// Логика взаимодействия для AvtomobilWindow.xaml
    /// </summary>
    public partial class AvtomobilWindow : Window
    {
        private string avtomobilVIN_nomer;
        private string avtomobilMarka;
        private string avtomobilModel;
        private int avtomobilGod;
        private int avtomobilVes;
        private int avtomobilCvet_ID;
        private int avtomobilTip_Dvigatelia_ID;
        private int avtomobilPrivod_ID;
        private int avtomobilCod_Regiona_ID;
        public AvtomobilWindow()
        {
            InitializeComponent();
            PopulateAvtomobil();
        }
        public AvtomobilWindow (string VIN_nomer, string Marka, string Model, int God,
                                int Ves, int Cvet_ID, int Tip_Dvigatelia_ID,
                                int Privod_ID, int Cod_Regiona_ID)
        {
            InitializeComponent();
            avtomobilVIN_nomer = VIN_nomer;
            avtomobilMarka = Marka;
            avtomobilModel = Model;
            avtomobilGod = God;
            avtomobilVes = Ves;
            avtomobilCvet_ID = Cvet_ID;
            avtomobilTip_Dvigatelia_ID = Tip_Dvigatelia_ID;
            avtomobilPrivod_ID = Privod_ID;
            avtomobilCod_Regiona_ID = Cod_Regiona_ID;
            PopulateAvtomobil();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы перешли на окно 'Автомобиль'");
        }
        public void PopulateAvtomobil()
        {
            using (GIBDDEntities db = new GIBDDEntities())
            {
                var avtomobil = db.Avtomobil.ToList();
                AvtomobilDG.ItemsSource = avtomobil;
            }
        }
        private void ButtonGlavnaia(object sender, RoutedEventArgs e)
        {
            Glavnaia glavnaia = new Glavnaia();
            glavnaia.Show();
            this.Close();
        }
        private void CvetButton(object sender, RoutedEventArgs e)
        {
            CvetWindow cvetWindow = new CvetWindow();
            cvetWindow.Show();
            this.Close();
        }
        private void Tip_DvigateliaButton(object sender, RoutedEventArgs e)
        {
            TipDvigateliaWindow tipDvigateliaWindow = new TipDvigateliaWindow();
            tipDvigateliaWindow.Show();
            this.Close();
        }
        private void PrivodButton(object sender, RoutedEventArgs e)
        {
            PrivodWindow privodWindow = new PrivodWindow();
            privodWindow.Show();
            this.Close();
        }
        private void Cod_RegionaButton(object sender, RoutedEventArgs e)
        {
            CodRegionaWindow codRegionaWindow = new CodRegionaWindow();
            codRegionaWindow.Show();
            this.Close();
        }
        private void AddButton(object sender, RoutedEventArgs e)
        {
            AvtomobilAdd avtomobilAdd = new AvtomobilAdd();
            avtomobilAdd.Show();
            PopulateAvtomobil();
            this.Close();
        }
        private void ChangeButton(object sender, RoutedEventArgs e)
        {
            var selectedAvtomobil = AvtomobilDG.SelectedItem as Avtomobil;
            if (selectedAvtomobil != null)
            {
                AvtomobilChange avtomobilChange = new AvtomobilChange(selectedAvtomobil);
                avtomobilChange.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите автомобиль для изменения", "Предупреждение", 
                                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            var selectedAvtomobil = AvtomobilDG.SelectedItem as Avtomobil;

            if (selectedAvtomobil != null)
            {
                using (GIBDDEntities db = new GIBDDEntities())
                {
                    var hasShtrafi = db.Shtrafi.Any(d => d.Avtomobil_ID == selectedAvtomobil.ID);
                    if (hasShtrafi)
                    {
                        MessageBoxResult result1 = MessageBox.Show("Вы уверены, что хотите удалить автомобиль, который связан со штрафом? Штраф будет также удалено",
                                                                   "Подтверждение удаления", 
                                                                   MessageBoxButton.YesNo,
                                                                   MessageBoxImage.Question);
                        if (result1 == MessageBoxResult.Yes)
                        {
                            db.Avtomobil.Attach(selectedAvtomobil);
                            db.Avtomobil.Remove(selectedAvtomobil);
                            db.SaveChanges();
                            PopulateAvtomobil();
                            MessageBox.Show("Автомобиль успешно удален", "Успех", MessageBoxButton.OK, 
                                MessageBoxImage.Information);
                            return;
                        }
                        if (result1 == MessageBoxResult.No)
                        {
                            return;
                        }
                    }
                    MessageBoxResult result2 = MessageBox.Show("Вы уверены, что хотите удалить выбранный автомобиль?", 
                                                               "Подтверждение удаления", 
                                                               MessageBoxButton.YesNo, 
                                                               MessageBoxImage.Question);
                    if (result2 == MessageBoxResult.Yes)
                    {
                        db.Avtomobil.Attach(selectedAvtomobil);
                        db.Avtomobil.Remove(selectedAvtomobil);
                        db.SaveChanges();
                        PopulateAvtomobil();
                        MessageBox.Show("Автомобиль успешно удален", "Успех", MessageBoxButton.OK, 
                                        MessageBoxImage.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите автомобиль для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
