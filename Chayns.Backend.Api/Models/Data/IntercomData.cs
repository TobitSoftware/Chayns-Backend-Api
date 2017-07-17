using Chayns.Backend.Api.Models.Data.Base;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Chayns.Backend.Api.Models.Data
{
    public class IntercomData : CommunicationBaseData
    {
        public IntercomData(int locationId) : base(locationId)
        {
        }

        public IntercomData(string siteId) : base(siteId)
        {
        }

        #region ThreadName

        private string _threadName;

        [JsonProperty(PropertyName = "ThreadName")]
        public string ThreadName
        {
            get => _threadName;
            set
            {
                _threadName = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region SendAsSystemMessage

        private bool _sendAsSystemMessage;

        [JsonProperty(PropertyName = "SendAsSystemMessage")]
        public bool SendAsSystemMessage
        {
            get => _sendAsSystemMessage;
            set
            {
                _sendAsSystemMessage = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region UseGroupChat

        private bool _useGroupChat;
        [JsonProperty(PropertyName = "UseGroupChat")]
        public bool UseGroupChat
        {
            get => _useGroupChat;
            set
            {
                _useGroupChat = value;
                OnPropertyChanged();
            }
        }

        #endregion UseGroupChat

        #region ReceiverLocationIds

        private List<int> _receiverLocationIds;
        [JsonProperty(PropertyName = "ReceiverLocationIds")]
        public List<int> ReceiverLocationIds
        {
            get => _receiverLocationIds;
            set
            {
                _receiverLocationIds = value;
                OnPropertyChanged();
            }
        }

        #endregion ReceiverLocationIds

        #region Message

        private string _message;

        [JsonProperty(PropertyName = "Message")]
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        #endregion Message

        #region UserAccessToken

        private string _userAccessToken;

        [JsonProperty(PropertyName = "UserAccessToken")]
        public string UserAccessToken
        {
            get => _userAccessToken;
            set
            {
                _userAccessToken = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
