﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfAulaAtecA.ViewModel
{
    public class ViewModelBase : ObservableObject
        {
            public virtual Task LoadAsync() => Task.CompletedTask;
        }
}


