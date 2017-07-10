using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anthill.Zones
{
    public interface IFridge
    {
        Miam GetMiams();
        void DepositMiams(Miam miam);
    }
}
