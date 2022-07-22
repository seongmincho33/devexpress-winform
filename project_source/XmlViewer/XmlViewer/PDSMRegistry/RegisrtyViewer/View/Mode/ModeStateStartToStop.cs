using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Viewer.RegistryViewer.View
{
    public class ModeStateStartToStop : IModeState
    {
        Button Set { get; set; }
        Button Start { get; set; }
        Button Stop { get; set; }
        Button Delete { get; set; }
        public ModeStateStartToStop(Button set, Button start, Button stop, Button delete)
        {
            Set = set;
            Start = start;
            Stop = stop;
            Delete = delete;

            Set.Enabled = true;
            Start.Enabled = true;
            Stop.Enabled = false;
            Delete.Enabled = true;
        }
        public void toggle(ModeSwitch modeSwitch)
        {
            modeSwitch.ModeState = new ModeStateStopToStart(Set, Start, Stop, Delete);
        }
    }
}
