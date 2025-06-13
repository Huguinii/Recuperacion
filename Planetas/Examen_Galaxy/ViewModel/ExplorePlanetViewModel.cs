using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Examen_Galaxy.Constants;
using Examen_Galaxy.DTO;
using Examen_Galaxy.Interface;
using Examen_Galaxy.Model;
using Examen_Galaxy.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Examen_Galaxy.ViewModel
{
    public partial class ExplorePlanetViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<PlanetModel> _items;

        private int _planetId;

        private GalaxyOverviewViewModel _galaxyOverviewViewModel;

        private readonly IHttpJsonProvider<PlanetaDTO> _httpJsonProvider;

        [ObservableProperty]
        private ExplorePlanetModel _Planet;

        [ObservableProperty]
        public ExplorePlanetModel _PlanetaCercano;

        [ObservableProperty]
        public ExplorePlanetModel _PlanetaLejano;

        [ObservableProperty]
        private ObservableCollection<PlanetModel> _items2;



        public ExplorePlanetViewModel(IHttpJsonProvider<PlanetaDTO> httpJsonProvider)
        {
            _httpJsonProvider = httpJsonProvider;
            _items = new ObservableCollection<PlanetModel>();
        }

        public void SetIdPlanet(int id)
        {
            _planetId = id;
        }

        public override async Task LoadAsync()
        {
            IEnumerable<PlanetaDTO> planetas = await _httpJsonProvider.GetAsync(Constants.Constants.PLANET_RESOURCE);
            Items = new ObservableCollection<PlanetModel>();
            foreach (var planeta in planetas)
            {
                Items.Add(PlanetModel.CreateModelFromDTO(planeta));
            }
            Planet = ExplorePlanetModel.CreateModelFromDTO(planetas.FirstOrDefault(x => x.Id == _planetId) ?? new PlanetaDTO());
            await CalcularDistancia();
                
        }

        internal void SetParentViewModel(ViewModelBase galaxyOverviewViewModel)
        {
            if (galaxyOverviewViewModel is GalaxyOverviewViewModel galaxyOverview)
            {
                _galaxyOverviewViewModel = galaxyOverview;
            }
        }

        [RelayCommand]
        private async Task Close(object? parameter)
        {
            if (_galaxyOverviewViewModel != null)
            {
                _galaxyOverviewViewModel.SelectedViewModel = null;
            }
        }

        [RelayCommand]
        private void SaveData() {
                if (Planet == null) return;

                var sb = new StringBuilder();
                sb.AppendLine("Name,Distance,Type,Temperature,ImagePath");
                sb.AppendLine($"{Planet.Name},{Planet.Distance},{Planet.Type},{Planet.Temperature},{Planet.ImagePath}");

                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{Planet.Name}.csv");
                File.WriteAllText(path, sb.ToString());
            if (File.Exists(path))
            {
                MessageBox.Show($"Planeta exportado correctamente en: {path}", "Exportación exitosa",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Hubo un error al exportar el planeta.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task CalcularDistancia()
        {
            IEnumerable<PlanetaDTO> planetas = await _httpJsonProvider.GetAsync(Constants.Constants.PLANET_RESOURCE);
            List<ExplorePlanetModel> items2= new List<ExplorePlanetModel>();
            foreach (var planeta in planetas)
            {
                items2.Add(ExplorePlanetModel.CreateModelFromDTO(planeta));
            }
            if (Planet == null)
            {
                return;
            }
            //Convertir string a double
            double distancia = Convert.ToDouble(Planet.Distance);

            PlanetaCercano = items2.Where(p => p != Planet)
                .OrderBy(p => Math.Abs(Convert.ToDouble(p.Distance) - distancia)).First();

            PlanetaLejano = items2.Where(p => p != Planet).OrderByDescending(p => Math.Abs(Convert.ToDouble(p.Distance) - distancia)).First();
        }
    }
    }
