using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Viewer.RegistryViewer.View
{
    public class ModeSwitch
    {
        private IModeState _modeState;

        public IModeState ModeState
        {
            get { return _modeState; }
            set { _modeState = value; }
        }

        public ModeSwitch(Button set, Button start, Button stop, Button delete)
        {
            ModeState = new ModeStateStartToStop(set, start, stop, delete);
        }

        public void onSwitch()
        {
            ModeState.toggle(this);
        }
    }
}
