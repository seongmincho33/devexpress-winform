using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace delegateSample003
{
    public partial class MyButton : System.Windows.Forms.Button
    {
        public event Action<object, Data> Caption2Changed;

        private string caption2 = string.Empty;

        [Category("모양"),
            Browsable(true),
            EditorBrowsable(),
            DefaultValue(""),
            Description("두번째 Caption을 가져오거나 설정합니다.")]
        public string Caption2
        {
            get {
                return caption2;
            }
            set {
                string oldValue = caption2;
                string result = value == null ? string.Empty : value;
                caption2 = result;

                if (Caption2Changed != null)
                {
                    
                    Data data = new Data() { OldValue = oldValue, NewValue = result };

                    Caption2Changed(this, data);
                }
            }
        }

        public MyButton()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }

    public class Data
    {
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
