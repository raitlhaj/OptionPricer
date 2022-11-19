using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OptionPricerWPF.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T field, T newvalue ,[CallerMemberName] string propertyName=null )
        {
             if (!EqualityComparer<T>.Default.Equals(field, newvalue))
              {
                field=newvalue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // set field
             }

            return false;
        }
    }
}
