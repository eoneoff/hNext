using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClientBlazor.ViewModels
{
    public class AppStateViewModel
    {
        private Dictionary<string, StateDataRecord> stateData = new Dictionary<string, StateDataRecord>();

        public T GetData<T>(string name = null)
        {
            name ??= typeof(T).Name;
            if (!stateData.ContainsKey(name)) stateData[name] = new StateDataRecord { Data = default(T) };
            return (T)stateData[name].Data;
        }

        public void SetData<T>(T data, string name = null)
        {
            name ??= typeof(T).Name;
            if (!stateData.ContainsKey(name)) stateData[name] = new StateDataRecord { Data = data };
            else stateData[name].Data = data;
        }

        public void Subscribe<T>(Action action, string name = null)
        {
            name ??= typeof(T).Name;
            if (!stateData.ContainsKey(name)) stateData[name] = new StateDataRecord { Data = default(T) };
            stateData[name].DataUpdated += (obj, args) => action();
        }
    }

    class StateDataRecord
    {
        private object data;

        public object Data
        {
            get => data;
            set
            {
                data = value;
                DataUpdated?.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler DataUpdated;
    }
}
