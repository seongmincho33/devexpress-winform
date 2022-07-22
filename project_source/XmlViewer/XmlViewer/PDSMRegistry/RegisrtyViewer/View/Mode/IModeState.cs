using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viewer.RegistryViewer.View
{
    public interface IModeState
    {
        void toggle(ModeSwitch modeSwitch);
    }
}
