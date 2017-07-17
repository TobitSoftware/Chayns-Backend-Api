using System.Collections.Generic;
using Chayns.Backend.Api.Models.Data.Base;
using Newtonsoft.Json;

namespace Chayns.Backend.Api.Models.Data
{
    public class PushData : DefaultData
    {
        public PushData(int locationId) : base(locationId)
        {
        }

        public PushData(string siteId) : base(siteId)
        {
        }

        #region UserId

        private int _userId;
        [JsonProperty(PropertyName = "UserId")]
        internal int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged();
            }
        }

        #endregion UserId

        #region UacGroupId

        private int _uacGroupId;
        [JsonProperty(PropertyName = "UacGroupId")]
        internal int UacGroupId
        {
            get => _uacGroupId;
            set
            {
                _uacGroupId = value;
                OnPropertyChanged();
            }
        }

        #endregion UacGroupId

        #region Alert

        private string _alert;
        public string Alert
        {
            get => _alert;
            set
            {
                _alert = value;
                OnPropertyChanged();
            }
        }

        #endregion Alert

        #region Actions

        private int _categoryId;
        private List<PushAction> _actions;

        public int CategoryId
        {
            get => _categoryId;
            set
            {
                _categoryId = value;
                OnPropertyChanged();
            }
        }

        public List<PushAction> Actions
        {
            get => _actions;
            set
            {
                _actions = value;
                OnPropertyChanged();
            }
        }

        #endregion Actions

        #region TappId

        private int? _tappId;

        public int? TappId
        {
            get => _tappId;
            set
            {
                _tappId = value;
                OnPropertyChanged();
            }
        }

        #endregion TappId

        #region Payload

        private Dictionary<string, object> _payload;

        public Dictionary<string, object> Payload
        {
            get => _payload;
            set
            {
                _payload = value;
                OnPropertyChanged();
            }
        }

        #endregion Payload
    }
}
