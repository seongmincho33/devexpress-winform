using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using XmlViewer.PDSMRegistry.MVC_Model;

namespace XmlViewer.PDSMRegistry.MVC_Controller
{
    public class PDMSRegistryController
    {
        private List<Timer> TimerList = new List<Timer>();

        IPDMSRegistry _view;

        IList _registrys;

        RegistryKeyAndValue _registryKeyAndValue;

        List<string> _lastRegistryValueList = new List<string>();

        public PDMSRegistryController(IPDMSRegistry view, IList registrys)
        {
            this._view = view;
            this._registrys = registrys;
            view.SetController(this);
        }

        public void LoadView()
        {
            _view.ClearRichTextBox();
            foreach (RegistryKeyAndValue registry in _registrys)
            {
                _view.UpdateRichTrackListWithRegistryKeyAndValue(registry);
            }
        }

        public void StopTimer()
        {
            foreach (Timer timer in TimerList)
            {
                if (timer != null)
                {
                    timer.Stop();
                    timer.Tick -= Timer_Tick;
                }
            }
        }

        public void DisposeTimer()
        {
            for (int i = 0; i < TimerList.Count; i++)
            {
                if (TimerList[i] != null)
                {
                    TimerList[i].Dispose();
                    TimerList[i] = null;
                }
            }
        }

        public void AddTrack()
        {
            _registryKeyAndValue = new RegistryKeyAndValue(_view.KeyName, _view.KeyValue);
            _registrys.Add(_registryKeyAndValue);
            _view.UpdateRichTrackListWithRegistryKeyAndValue(_registryKeyAndValue);
        }

        public void StopTrack()
        {
            foreach (Timer timer in TimerList)
            {
                if (timer != null)
                {
                    timer.Tick -= Timer_Tick;
                    timer.Stop();
                }
            }
            _view.ClearRichTextBox();
            _registrys.Clear();
            _lastRegistryValueList.Clear();
        }

        public void StartTrack()
        {
            if (_registrys != null && _registrys.Count > 0)
                this.CreateTimer();
        }

        private void CreateTimer()
        {
            int LastRegistryValueList_Index = 0;
            foreach (RegistryKeyAndValue regis in _registrys)
            {
                Timer timer = new Timer();
                string lastValue = null;
                _lastRegistryValueList.Add(lastValue);
                timer.Tag = new Tuple<RegistryKeyAndValue, int>(regis, LastRegistryValueList_Index++);
                timer.Interval = 10;
                timer.Tick += Timer_Tick;
                timer.Start();
                TimerList.Add(timer);
            }
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                Timer timer = sender as Timer;
                Tuple<RegistryKeyAndValue, int> r = timer.Tag as Tuple<RegistryKeyAndValue, int>;
                var registry_value = this.GetRegistryValue(r.Item1);
                if (_lastRegistryValueList[r.Item2] != registry_value && !string.IsNullOrEmpty(registry_value))
                {
                    _view.UpdateRichResultWithRegistryValue(registry_value);
                    _lastRegistryValueList[r.Item2] = registry_value;
                }
            }
            catch (Exception ex)
            {
                foreach (Timer timer in TimerList)
                {
                    timer.Tick -= Timer_Tick;
                    timer.Stop();
                }
                MessageBox.Show(ex.Message);
            }
        }

        private string GetRegistryValue(RegistryKeyAndValue r)
        {
            try
            {
                object obj;
                obj = Registry.GetValue(r.KeyName, r.KeyValue, "");

                if (obj == null)
                    return "";
                else
                    return obj.ToString();
            }
            catch (Exception ex)
            {
                foreach (Timer timer in TimerList)
                {
                    timer.Tick -= Timer_Tick;
                    timer.Stop();
                }
                MessageBox.Show(ex.ToString());
            }
            return "";
        }
    }
}
