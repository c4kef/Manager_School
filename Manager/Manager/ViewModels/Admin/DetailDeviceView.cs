using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Manager.Views.Admin;
using Xamarin.Forms;

namespace Manager.ViewModels.Admin
{
    public class DetailDeviceView : ViewModelBase
    {
        public DetailDeviceView(Device device, DetailDevice detailDevice)
        {
            _view = detailDevice;
            DeviceInfo = device;
            
            switch (DeviceInfo.Status)
            {
                case "В работе":
                    InWork = true;
                    break;
                case "Списан":
                    Decommissioned = true;
                    break;
                case "На ремонте":
                    OnRepair = true;
                    break;
            }
            
            AcceptChangesCommand = new Command(async () => await ExecuteAcceptChangesCommand());
        }

        private DetailDevice _view;
        
        public Device DeviceInfo { get; set; }
        
        private bool _inWork;
        public bool InWork
        {
            get { return _inWork; }
            set
            {
                _inWork = value;
                OnPropertyChanged("InWork");
            }
        }

        private bool _decommissioned;
        public bool Decommissioned
        {
            get { return _decommissioned; }
            set
            {
                _decommissioned = value;
                OnPropertyChanged("Decommissioned");
            }
        }

        private bool _onRepair;
        public bool OnRepair
        {
            get { return _onRepair; }
            set
            {
                _onRepair = value;
                OnPropertyChanged("OnRepair");
            }
        }

        public Command AcceptChangesCommand { get; }

        private async Task ExecuteAcceptChangesCommand()
        {
            if (!OnRepair && !Decommissioned && !InWork)
                return;

            await (new SqlCommand(
                $"UPDATE Equipmentwork SET id_status = (Select Equipmentstatus.id from Equipmentstatus WHERE Equipmentstatus.statusname = N'{GetStatusText()}') WHERE id = {DeviceInfo.WId}",
                Globals.connection)).ExecuteNonQueryAsync();

            await _view.DisplayAlert("Успешно", "Вы обновили статус устройства", "OK");

            string GetStatusText()
            {
                if (InWork)
                    return "В работе";
                else if (Decommissioned)
                    return "Списан";
                else
                    return "На ремонте";
            }
        }
    }
}