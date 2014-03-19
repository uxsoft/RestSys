using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using RestSys.Models.Magic;
namespace RestSys.Models.Exports
{
    [Magic]
    public abstract class IRSEntity : INotifyPropertyChanged
    {
        [Key]
        public int Id { get; set; }

        private bool SuppressPropertyChanged = false;

        public void Quietly(Action<IRSEntity> action)
        {
            SuppressPropertyChanged = true;
            action(this);
            SuppressPropertyChanged = false;
        }

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
