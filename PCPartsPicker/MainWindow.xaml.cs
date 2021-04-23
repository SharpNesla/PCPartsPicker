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
                            if (!pickResult.Rule.IsSuggestion)
                            {
                                PickResult = pickResult;
                                this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(this.PickResult)));
                                this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(this.Variant)));
                            }
                            else
                            {
                                var selectedGPUName = pickResult.SelectedGPU != null ? pickResult.SelectedGPU.Name : "нет";
                                MessageBox.Show("Для выбранных входных данных не найдено решение!\n\n" +
                                    $"Ближайший возможный вариант стоимостью: {pickResult.SumCost}:₽\n" + 
                                    $"Процессор: {pickResult.SelectedCPU.Name};\n" +
                                    $"Видеокарта: {selectedGPUName}.\n",
                                    "Ошибка: не найдено решение!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Для выбранных входных данных не найдено решение!",
                                "Ошибка: не найдено решение!");
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
