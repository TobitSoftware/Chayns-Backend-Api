using Chayns.Backend.Api.Models.Data.Base;

namespace Chayns.Backend.Api.Models.Data
{
    public class DeviceDataGet : DefaultData
    {
        public DeviceDataGet(int locationId) : base(locationId)
        {
        }

        public DeviceDataGet(string siteId) : base(siteId)
        {
        }

        #region UserId

        private int _userId;
        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged();
            }
        }

        #endregion UserId

        #region UdId

        private string _udId;
        public string UdId
        {
            get => _udId;
            set
            {
                _udId = value;
                OnPropertyChanged();
            }
        }

        #endregion UDID

        #region SysName

        private string _sysName;
        public string SysName
        {
            get => _sysName;
            set
            {
                _sysName = value;
                OnPropertyChanged();
            }
        }
        #endregion SysName

        #region SysVersion

        private string _sysVersion;
        public string SysVersion
        {
            get => _sysVersion;
            set
            {
                _sysVersion = value;
                OnPropertyChanged();
            }
        }
        #endregion SysVersion

        #region AppVersion

        private int _appVersion;
        public int AppVersion
        {
            get => _appVersion;
            set
            {
                _appVersion = value;
                OnPropertyChanged();
            }
        }

        #endregion AppVersion
    }
}
