using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Viewer.RegistryViewer.Model;

namespace Viewer.RegistryViewer.View
{
    public class RegistrVieweryController
    {
        private List<Timer> TimerList = new List<Timer>();

        IRegistryViewer _view;

        IList _registryList;        

        List<string> _lastValueDataList = new List<string>();        

        public RegistrVieweryController(IRegistryViewer view, IList registrys)
        {
            this._view = view;
            this._registryList = registrys;
            view.SetController(this);
        }

        public void LoadView()
        {
            _view.ClearRichTextBox();
            _view.ClearDataGridView();         
            _view.UpdateDataGridViewTrackListWithRegistryKeyAndValueName(_registryList);
        }

        public void ClearRichResult()
        {
            _view.ClearRichTextBox();
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
            _registryList.Add(new SeonRegistry(_view.Key, _view.ValueName));
            _view.UpdateDataGridViewTrackListWithRegistryKeyAndValueName(_registryList);
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
        }

        public void DeleteTrack()
        {
            foreach(var delete in _view.SelectedTrackInfoToDelete)
            {
                string delete_key = ((SeonRegistry)((DataGridViewRow)delete).DataBoundItem).Key;
                string delete_valueName = ((SeonRegistry)((DataGridViewRow)delete).DataBoundItem).ValueName;              
                for (int i = _registryList.Count - 1; i >= 0; i--)
                {
                    if (((SeonRegistry)_registryList[i]).Key == delete_key && ((SeonRegistry)_registryList[i]).ValueName == delete_valueName)
                    {
                        _registryList.Remove(((SeonRegistry)_registryList[i]));
                    }
                }
            }
            _view.UpdateDataGridViewTrackListWithRegistryKeyAndValueName(_registryList);
        }

        public void StartTrack()
        {
            if (_registryList != null && _registryList.Count > 0)
                this.CreateTimer();
        }

        private void CreateTimer()
        {
            int LastValueDataList_Index = 0;
            foreach (SeonRegistry regis in _registryList)
            {
                Timer timer = new Timer();
                string lastValueData = null;
                _lastValueDataList.Add(lastValueData);
                timer.Tag = new Tuple<SeonRegistry, int>(regis, LastValueDataList_Index++);
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
                Tuple<SeonRegistry, int> r = timer.Tag as Tuple<SeonRegistry, int>;
                var valueData = this.GetValueData(r.Item1);
                if (_lastValueDataList[r.Item2] != valueData && !string.IsNullOrEmpty(valueData))
                {
                    _view.UpdateRichResultWithValueData(valueData);
                    _lastValueDataList[r.Item2] = valueData;
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

        private string GetValueData(SeonRegistry r)
        {
            try
            {
                object obj;
                obj = Registry.GetValue(r.Key, r.ValueName, "");

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
