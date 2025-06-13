using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using WpfAulaAtecA.Models;
using WpfAulaAtecA.Utils;

namespace WpfAulaAtecA.ViewModel
{
    public partial class DiasNoLectivosViewModel : ViewModelBase
    {
        [ObservableProperty]
        private DateTime? fecha;

        [ObservableProperty]
        private string motivo;

        [ObservableProperty]
        private ObservableCollection<DiaNoLectivoDto> diasNoLectivos = new();

        public override async Task LoadAsync()
        {
            try
            {
                var lista = await HttpJsonClient<List<DiaNoLectivoDto>>.Get("https://localhost:7777/api/DiaNoLectivo");
                diasNoLectivos = new ObservableCollection<DiaNoLectivoDto>(lista ?? new());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando días no lectivos: {ex.Message}");
            }
        }

        [RelayCommand]
        public async Task Add()
        {
            if (fecha == null || string.IsNullOrWhiteSpace(motivo))
            {
                MessageBox.Show("Debe seleccionar una fecha y escribir un motivo.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var nuevoDia = new DiaNoLectivoDto
            {
                Fecha = fecha.Value,
                Motivo = motivo
            };

            try
            {
                var response = await HttpJsonClient<DiaNoLectivoDto>.Post("https://localhost:7777/api/DiaNoLectivo", nuevoDia);
                if (response != null)
                {
                    diasNoLectivos.Add(response);
                    motivo = string.Empty;
                    fecha = null;
                    MessageBox.Show("Día no lectivo añadido con éxito.");
                    await this.LoadAsync();
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el día no lectivo.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}");
            }
            await this.LoadAsync();
        }
    }
}
