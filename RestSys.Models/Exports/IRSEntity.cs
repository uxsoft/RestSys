using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSys.Models.Exports
{
    abstract class IRSEntity : INotifyPropertyChanged
    {
        int Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
