using Chayns.Backend.Api.Models.Data.Base;

namespace Chayns.Backend.Api.Models.Data
{
    public class UacGroupDataGet : DefaultData
    {
        public UacGroupDataGet(int locationId) : base(locationId)
        {
        }

        public UacGroupDataGet(string siteId) : base(siteId)
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

        #region Name

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        #endregion Name

        #region ShowName

        private string _showName;
        public string ShowName
        {
            get => _showName;
            set
            {
                _showName = value;
                OnPropertyChanged();
            }
        }

        #endregion ShowName

        #region Description

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        #endregion Description

        #region TappId

        private int _tappId;
        public int TappId
        {
            get => _tappId;
            set
            {
                _tappId = value;
                OnPropertyChanged();
            }
        }

        #endregion TappId
    }
}
