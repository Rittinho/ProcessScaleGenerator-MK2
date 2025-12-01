using CommunityToolkit.Mvvm.ComponentModel;
using ProcessScaleGenerator.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessScaleGenerator.ViewModel.Wrappers
{
    public partial class ToyotaEmployeeWrapper : ObservableObject
    {
        public ToyotaEmployee Model { get; }

        public ToyotaEmployeeWrapper(ToyotaEmployee model)
        {
            Model = model;
        }
        public string Name => Model.Name;
        public string Position => Model.Position;
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
