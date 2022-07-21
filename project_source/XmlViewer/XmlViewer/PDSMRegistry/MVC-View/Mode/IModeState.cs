using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlViewer.PDSMRegistry.MVC_Controller
{
    public interface IModeState
    {
        void toggle(ModeSwitch modeSwitch);
    }
}
