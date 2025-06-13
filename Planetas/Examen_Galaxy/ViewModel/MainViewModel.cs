using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_Galaxy.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        private ViewModelBase? _selectedViewModel;

        public GalaxyOverviewViewModel GalaxyOverviewViewModel { get; }

        public MainViewModel(GalaxyOverviewViewModel galaxyOverviewViewModel, GalaxyGridViewModel galaxyGridViewModel, ImportViewModel importViewModel, AddPlanetaViewModel addPlanetaViewModel, ExplorePlanetViewModel explorePlanetViewModel, ExportViewModel exportViewModel)
        {
            _selectedViewModel = galaxyOverviewViewModel;
            GalaxyOverviewViewModel = galaxyOverviewViewModel;
            GalaxyGridViewModel = galaxyGridViewModel;
            ImportViewModel = importViewModel;
            AddPlanetaViewModel = addPlanetaViewModel;
            ExplorePlanetViewModel = explorePlanetViewModel;
            ExportViewModel = exportViewModel;

        }

        public ViewModelBase? SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                SetProperty(ref _selectedViewModel, value);
            }
        }

        [RelayCommand]
        private async Task SelectViewModel(object? parameter)
        {
            SelectedViewModel = parameter as ViewModelBase;
            await LoadAsync();
        }
        
        public ImportViewModel ImportViewModel { get; }
        public GalaxyGridViewModel GalaxyGridViewModel { get; }

        public AddPlanetaViewModel AddPlanetaViewModel { get; }

        public ExplorePlanetViewModel ExplorePlanetViewModel { get; }

        public ExportViewModel ExportViewModel { get; } 

        public async override Task LoadAsync()
        {
            if (SelectedViewModel is not null)
            {
                await SelectedViewModel.LoadAsync();
            }
        }
    }
}
