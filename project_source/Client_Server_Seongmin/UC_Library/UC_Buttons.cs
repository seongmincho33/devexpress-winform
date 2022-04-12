using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;

namespace UC_Library
{
    public partial class UC_Buttons : DevExpress.XtraEditors.XtraUserControl, IEnumerable, IEnumerator, ICollection
    {
        [Description("Test text displayed in the textbox"), Category("Data")]
        public int MyProperty
        {
            get
            {
                // Insert code here.
                return 0;
            }
            set
            {
                // Insert code here.
            }
        }

        public Button[] buttons;
        private int position = -1;

        public UC_Buttons()
        {
            InitializeComponent();
            buttons = new Button[4];
        }

        [Description("Test text displayed in the textbox"), Category("Data")]
        public Button this[int index]
        {
            get
            {
                return buttons[index];
            }

            set
            {
                if(index >= buttons.Length)
                {
                    Array.Resize<Button>(ref buttons, index + 1);
                    buttons[index] = value;
                }
            }
        }

        public object Current
        {
            get
            {
                return buttons[position];
            }
        }

        public int Count => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        public bool IsSynchronized => throw new NotImplementedException();

        public bool MoveNext()
        {
            if(position == buttons.Length - 1)
            {
                Reset();
                return false;
            }

            position++;
            return (position < buttons.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }
    }
}
