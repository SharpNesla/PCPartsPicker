using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace PCPartsPicker
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public string Variant { get; set; }
        public Model Model { get; set; }
        public decimal MaxCost { get; set; }
        public ICommand Evaulate { get; }
        public PartPickResult PickResult { get; set; }
        public bool IsPickResultNotNull { get; set; }
        public MainWindowViewModel()
        {
            this.Model = LoadModel();
            Evaulate = new RelayCommand(
                () =>
                {
                    if (!string.IsNullOrEmpty(this.Variant))
                    {
                        var pickResult = Model.PickByCase(Variant, MaxCost);
                        if (pickResult != null)
                        {
                            PickResult = pickResult;
                            this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(this.PickResult)));
                            this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(this.Variant)));
                        }
                        else
                        {
                            MessageBox.Show("Для выбранных входных данных не найдено решение!",
                           "Ошибка: найдено решение!");
                        }
                       
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста выберите вариант использования!",
                            "Ошибка: не выбран вариант использования!");
                    }
                }
            );            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Model LoadModel()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Model));
            using (FileStream fs = new FileStream("Data.xml", FileMode.OpenOrCreate))
            {
                return (Model)formatter.Deserialize(fs);
            }
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();
        }
    }
}
