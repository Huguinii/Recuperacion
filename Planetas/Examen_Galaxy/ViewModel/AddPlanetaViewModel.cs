using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Examen_Galaxy.DTO;
using Examen_Galaxy.Interface;
using LoginRegister.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Examen_Galaxy.ViewModel
{
    public partial class AddPlanetaViewModel : ViewModelBase
    {

        [ObservableProperty]
        private string _nombre;

        [ObservableProperty]
        private int _distancia;

        [ObservableProperty]
        private string _tipo;

        [ObservableProperty]
        private string _temperatura;

        [ObservableProperty]
        private string _atmosfera;

        [ObservableProperty]
        private string _imageName;

        private readonly IHttpJsonProvider<PlanetaDTO> _planetaServiceToApi;


        public AddPlanetaViewModel(IHttpJsonProvider<PlanetaDTO> planetaServiceToApi)
        {
            _planetaServiceToApi = planetaServiceToApi;
        }

        [RelayCommand]
        public async Task AnadirPlaneta()
        {
            if (string.IsNullOrEmpty(Nombre) ||
                (Distancia == 0)||
                string.IsNullOrEmpty(Tipo) ||
                string.IsNullOrEmpty(Temperatura)||
                string.IsNullOrEmpty(Atmosfera) ||
                string.IsNullOrEmpty(ImageName))
             
            {
                MessageBox.Show("Por favor, rellene todos los campos.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            PlanetaDTO planetaDTO = new()
            {
                Id = IdRandom(),
                Nombre = Nombre,
                Distancia = Distancia,
                Tipo = Tipo,
                Temperatura = Temperatura,
                Atmosfera = Atmosfera,
                ImageName = ImageName
            };

            if (string.IsNullOrEmpty(planetaDTO.ImageName))
            {
                planetaDTO.ImageName = "";
            }

            try
            {
                await _planetaServiceToApi.PostAsync(planetaDTO,Constants.Constants.PLANET_RESOURCE);

                MessageBox.Show("Planeta añadido con exito");
                App.Current.Services.GetService<MainViewModel>().LoadAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error durante el registro: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private int IdRandom()
        {
            Random Id = new Random();
            int idNumero= Id.Next(0, 10000);
            return idNumero;
        }
    }
}