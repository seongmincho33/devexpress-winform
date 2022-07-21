using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XmlViewer.PDSMRegistry.MVC_Controller
{
    public class ModeStateStopToStart : IModeState
    {
        Button Set { get; set; }
        Button Start { get; set; }
        Button Stop { get; set; }
        Button Delete { get; set; }

        public ModeStateStopToStart(Button set, Button start, Button stop, Button delete)
        {
            Set = set;
            Start = start;
            Stop = stop;
            Delete = delete;

            Set.Enabled = false;
            Start.Enabled = false;
            Stop.Enabled = true;
            Delete.Enabled = false;
        }

        public void toggle(ModeSwitch modeSwitch)
        {
            modeSwitch.ModeState = new ModeStateStartToStop(Set, Start, Stop, Delete);
        }
    }
}
