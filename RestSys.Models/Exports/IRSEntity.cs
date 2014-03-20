using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Diagnostics;
namespace RestSys.Models.Exports
{
    public interface IRSEntity
    {
        int Id { get; set; }
    }
}
