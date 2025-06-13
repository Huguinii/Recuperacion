using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Examen_Galaxy.DTO;
using Examen_Galaxy.Interface;
using Examen_Galaxy.Service;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_Galaxy.ViewModel
{
    public partial class ExportViewModel : ViewModelBase
    {
        private readonly IFileService<PlanetaDTO> _fileService;
        private readonly IHttpJsonProvider<PlanetaDTO> _httpJsonProvider;
        public static IEnumerable<PlanetaDTO> planetas;

        public ExportViewModel(IFileService<PlanetaDTO> fileService, IHttpJsonProvider<PlanetaDTO> httpJsonProvider)
        {
            _fileService = fileService;
            _httpJsonProvider = httpJsonProvider;
       
        }

        public override async Task LoadAsync() {
            planetas = await _httpJsonProvider.GetAsync(Constants.Constants.BASE_URL);
        }


        [RelayCommand]
        public async void Export()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = Constants.Constants.JSON_FILTER
            };

            if (saveFileDialog.ShowDialog() == true)
            {

                _fileService.Save(saveFileDialog.FileName, planetas);
            }
        }


    }
}
