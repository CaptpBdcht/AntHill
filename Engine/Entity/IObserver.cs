using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Entity
{
    public interface IObserver
    {
        void Notify(Observable observable);
    }
}
