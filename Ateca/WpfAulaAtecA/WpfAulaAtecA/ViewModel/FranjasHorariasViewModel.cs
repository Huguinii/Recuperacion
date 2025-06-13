using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfAulaAtecA.Models;
using WpfAulaAtecA.Utils;

namespace WpfAulaAtecA.ViewModel
{
    public partial class FranjasHorariasViewModel : ViewModelBase
    {
        [ObservableProperty] private string horaInicio;
        [ObservableProperty] private string horaFin;
        [ObservableProperty] private bool esRecreo;
        [ObservableProperty] private bool disponible = true;

        [ObservableProperty] private FranjaDTO franjaSeleccionada;
        [ObservableProperty] private ObservableCollection<FranjaDTO> franjas = new();

        public override async Task LoadAsync()
        {
            try
            {
                var lista = await HttpJsonClient<List<FranjaDTO>>.Get("https://localhost:7777/api/FranjaHoraria");
                Franjas = new ObservableCollection<FranjaDTO>(lista ?? new());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar franjas: {ex.Message}");
            }
        }

        partial void OnFranjaSeleccionadaChanged(FranjaDTO value)
        {
            if (value == null) return;

            HoraInicio = value.HoraInicio.ToString("HH:mm");
            HoraFin = value.HoraFin.ToString("HH:mm");
            EsRecreo = value.EsRecreo;
            Disponible = value.Disponible;
        }

        public ObservableCollection<string> HorasDisponibles { get; } =
            new ObservableCollection<string>(
                Enumerable.Range(0, (345 / 5 + 1) * 2)
                    .Select(i =>
                    {
                        if (i <= 345 / 5)
                            return TimeOnly.FromTimeSpan(TimeSpan.FromMinutes(8 * 60 + i * 5)).ToString("HH:mm");
                        else
                            return TimeOnly.FromTimeSpan(TimeSpan.FromMinutes(16 * 60 + (i - 345 / 5 - 1) * 5)).ToString("HH:mm");
                    }));



        [RelayCommand]
        public async Task AddOrUpdate()
        {
            if (!TimeOnly.TryParse(HoraInicio, out var inicio) ||
                !TimeOnly.TryParse(HoraFin, out var fin))
            {
                MessageBox.Show("Formato de hora inválido. Use HH:mm");
                return;
            }

            if (inicio >= fin)
            {
                MessageBox.Show("La hora de inicio debe ser anterior a la hora de fin.");
                return;
            }

            // Validación: no solaparse + 5 minutos de separación
            foreach (var f in Franjas)
            {
                if (FranjaSeleccionada != null && f.Id == FranjaSeleccionada.Id)
                    continue;

                bool seSolapan = !(inicio >= f.HoraFin.AddMinutes(5) || fin <= f.HoraInicio.AddMinutes(-5));
                if (seSolapan)
                {
                    MessageBox.Show($"La franja se solapa con {f.HoraInicio}-{f.HoraFin}");
                    return;
                }
            }

            var nueva = new FranjaDTO
            {
                
                HoraInicio = inicio,
                HoraFin = fin,
                EsRecreo = EsRecreo,
                Disponible = Disponible
            };

            try
            {
                if (FranjaSeleccionada == null)
                {
                    var response = await HttpJsonClient<FranjaDTO>.Post("https://localhost:7777/api/FranjaHoraria", nueva);
                    if (response != null)
                    {
                        Franjas.Add(response);
                        MessageBox.Show("Franja añadida.");
                        await this.LoadAsync();
                    }
                }
                else
                {
                    nueva.Id = FranjaSeleccionada.Id;
                    var response = await HttpJsonClient<FranjaDTO>.Put($"https://localhost:7777/api/FranjaHoraria/{nueva.Id}", nueva);
                    if (response != null)
                    {
                        int index = Franjas.IndexOf(FranjaSeleccionada);
                        Franjas[index] = response;
                        MessageBox.Show("Franja actualizada.");
                        await this.LoadAsync();
                    }
                }

                // Reset formulario
                HoraInicio = string.Empty;
                HoraFin = string.Empty;
                EsRecreo = false;
                Disponible = true;
                FranjaSeleccionada = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar franja: {ex.Message}");
            }
        }
    }
}
