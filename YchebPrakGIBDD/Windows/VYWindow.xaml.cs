using System.Linq;
using System.Windows;
using YchebPrakGIBDD.Entities;

namespace YchebPrakGIBDD.Windows
{
    /// <summary>
    /// Логика взаимодействия для VoditelskoeYdostoverenieWindow.xaml
    /// </summary>
    public partial class VYWindow : Window
    {
        private string VY_Data_polych;
        private string VY_Data_okonch;
        private int VY_Voditel_ID;
        private int VY_Seria;
        private int VY_Nomer;
        public VYWindow()
        {
            InitializeComponent();
            PopulateVY();
        }
        public VYWindow(string Data_polych, string Data_okonch, 
                                              int Voditel_ID, int Seria, int Nomer)
        {
            InitializeComponent();
            VY_Data_polych = Data_polych;
            VY_Data_okonch = Data_okonch;
            VY_Voditel_ID = Voditel_ID;
            VY_Seria = Seria;
            VY_Nomer = Nomer;
            PopulateVY();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы перешли на окно 'Водительское удостоверение'");
        }
        public void PopulateVY()
        {
            using (GIBDDEntities db = new GIBDDEntities())
            {
                var VY = db.Voditelskoe_ydostoverenie.ToList();
                VYDG.ItemsSource = VY;
            }
        }
        private void ButtonGlavnaia(object sender, RoutedEventArgs e)
        {
            Glavnaia glavnaia = new Glavnaia();
            glavnaia.Show();
            this.Close();
        }
        private void AddButton(object sender, RoutedEventArgs e)
        {
            VYAdd vyAdd = new VYAdd();
            vyAdd.Show();
            PopulateVY();
            this.Close();
        }
        private void ChangeButton(object sender, RoutedEventArgs e)
        {
            var selectedVY= VYDG.SelectedItem as Voditelskoe_ydostoverenie;
            if (selectedVY != null)
            {
                VYChange vyChange = new VYChange(selectedVY);
                vyChange.Show();
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
            var selectedVY= VYDG.SelectedItem as Voditelskoe_ydostoverenie;

            if (selectedVY != null)
            {
                using (GIBDDEntities db = new GIBDDEntities())
                {
                    var hasShtrafi = db.Shtrafi.Any(d => d.VY_ID == selectedVY.ID);
                    var hasVY_Istoria_Statysa = db.VY_Istoria_Statysa.Any(d => d.VY_ID == selectedVY.ID);
                    if (hasShtrafi|| hasVY_Istoria_Statysa)
                    {
                        MessageBoxResult result1 = MessageBox.Show("Вы уверены, что хотите удалить водительское удостоверение, которое связано с другими таблицами? Строки из связанных таблиц будут также удалены",
                                                                   "Подтверждение удаления",
                                                                   MessageBoxButton.YesNo,
                                                                   MessageBoxImage.Question);
                        if (result1 == MessageBoxResult.Yes)
                        {
                            db.Voditelskoe_ydostoverenie.Attach(selectedVY);
                            db.Voditelskoe_ydostoverenie.Remove(selectedVY);
                            db.SaveChanges();
                            PopulateVY();
                            MessageBox.Show("Водительское удостоверение успешно удален", "Успех", 
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                        if (result1 == MessageBoxResult.No)
                        {
                            return;
                        }
                    }
                    MessageBoxResult result2 = MessageBox.Show("Вы уверены, что хотите удалить выбранное водительское удостоверение?",
                                                               "Подтверждение удаления",
                                                               MessageBoxButton.YesNo,
                                                               MessageBoxImage.Question);
                    if (result2 == MessageBoxResult.Yes)
                    {
                        db.Voditelskoe_ydostoverenie.Attach(selectedVY);
                        db.Voditelskoe_ydostoverenie.Remove(selectedVY);
                        db.SaveChanges();
                        PopulateVY();
                        MessageBox.Show("Водительское удостоверение успешно удалено", "Успех",
                                        MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите водительское удостоверение для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }



}