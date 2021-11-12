using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyButtonClass
{
    public class Button
    {
        public event EventHandler Clicked;

        public delegate void DeleCall(int v1, int v2);
        public event Action<object, EventArgs> Clicked2;
        public void OnClick()
        {
            if (Clicked != null)
                Clicked(this, new EventArgs());

            if (Clicked2 != null)
                Clicked2(this, new EventArgs());
        }

        public void CallBackTest00(DeleCall delCall)
        {
            delCall(1, 2);
        }

        public void CallBackTest01(Action<int, int> delCall)
        {
            delCall(1, 2);
        }

        public void CallBackTest02(Action<int, int> delCall, int v1, int v2)
        {
            delCall(v1, v2);
        }
    }
}
