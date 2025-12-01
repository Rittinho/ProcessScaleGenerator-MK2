using CommunityToolkit.Mvvm.ComponentModel;
using ProcessScaleGenerator.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessScaleGenerator.ViewModel.Wrappers
{
    public partial class ToyotaProcessWrapper : ObservableObject
    {
        public ToyotaProcess Model { get; }

        public ToyotaProcessWrapper(ToyotaProcess model)
        {
            Model = model;
        }
        public string Title => Model.Title;
        public string Description => Model.Description;
        public IconParameters Icon => Model.Icon;

        public bool Hidded
        {
            get => Model.Hidded;
            set
            {
                if (Model.Hidded != value)
                {
                    Model.Hidded = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
