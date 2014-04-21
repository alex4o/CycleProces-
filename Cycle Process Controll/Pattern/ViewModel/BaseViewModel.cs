using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CycleProcessControl.Pattern.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region PropertyChanged

        private event PropertyChangedEventHandler propertyChangedImpl = delegate { };

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { this.propertyChangedImpl += value; }
            remove { this.propertyChangedImpl -= value; }

        }
        public void NotifyPropertyChanged(string str)
        {
            if (propertyChangedImpl != null)
            {
                propertyChangedImpl(this, new PropertyChangedEventArgs(str));
            }
        }
        #endregion
    }
}
