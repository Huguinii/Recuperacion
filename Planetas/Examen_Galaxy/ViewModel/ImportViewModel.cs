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
    public partial class ImportViewModel : ViewModelBase
    {
        private readonly IFileService<PlanetaDTO> _fileService;
        private readonly IHttpJsonProvider<PlanetaDTO> _httpJsonProvider;

        public ImportViewModel(IFileService<PlanetaDTO> fileService, IHttpJsonProvider<PlanetaDTO> httpJsonProvider)
        {
            _fileService = fileService;
            _httpJsonProvider = httpJsonProvider;
        }


        [RelayCommand]
        public async Task LoadFromFile()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = Constants.Constants.JSON_FILTER
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var datos = _fileService.Load(openFileDialog.FileName);
                await _httpJsonProvider.DeleteAsync(Constants.Constants.BASE_URL + "deleteAll");
                foreach (var planeta in datos)
                {
                    _httpJsonProvider.PostAsync(planeta , Constants.Constants.BASE_URL);
                }
            }
        }
    }
}