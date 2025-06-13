using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Examen_Galaxy.DTO;
using Examen_Galaxy.Interface;
using Examen_Galaxy.Model;
using Examen_Galaxy.Service;
using Examen_Galaxy.View;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_Galaxy.ViewModel
{
    public partial class GalaxyGridViewModel : ViewModelBase
    {
        private readonly IHttpJsonProvider<PlanetaDTO> _httpJsonProvider;
        private AddPlanetaViewModel _addViewModel;

       [ObservableProperty]
        private ObservableCollection<PlanetaDTO> listaPlanetas;

        public GalaxyGridViewModel(IHttpJsonProvider<PlanetaDTO> httpJsonProvider,AddPlanetaViewModel addViewModel)
        {
            _httpJsonProvider = httpJsonProvider;
            _addViewModel = addViewModel;
            listaPlanetas = new ObservableCollection<PlanetaDTO>();
           
        }


        [RelayCommand]
        public async Task AddPlaneta()
        {
            var addPlanetaWindow = new AddPlaneta();

            var addPlanetaViewModel = App.Current.Services.GetService<AddPlanetaViewModel>();
            addPlanetaWindow.DataContext = addPlanetaViewModel;
            addPlanetaWindow.ShowDialog();
            await LoadAsync();
        }


        public override async Task LoadAsync()
        {
            listaPlanetas.Clear();
            IEnumerable<PlanetaDTO> planetas = await _httpJsonProvider.GetAsync(Constants.Constants.PLANET_RESOURCE);
            foreach (PlanetaDTO planeta in planetas)
            {
                ListaPlanetas.Add(planeta);
            }
        }
    }
}