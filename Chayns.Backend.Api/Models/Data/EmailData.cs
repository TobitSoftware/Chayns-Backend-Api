using Chayns.Backend.Api.Models.Data.Base;
using Newtonsoft.Json;

namespace Chayns.Backend.Api.Models.Data
{
    public class EmailData : CommunicationBaseData
    {
        public EmailData(int locationId) : base(locationId) { }

        public EmailData(string siteId) : base(siteId) { }

        #region Subject

        private string _subject;
        [JsonProperty(PropertyName = "Subject")]
        public string Subject
        {
            get => _subject;
            set
            {
                _subject = value;
                OnPropertyChanged();
            }
        }

        #endregion Subject

        #region headline

        private string _headline;
        [JsonProperty(PropertyName = "Headline")]
        public string Headline
        {
            get => _headline;
            set
            {
                _headline = value;
                OnPropertyChanged();
            }
        }

        #endregion headline 

        #region text

        private string _text;
        [JsonProperty(PropertyName = "Text")]
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        #endregion text

        #region greeting

        private string _greeting;
        [JsonProperty(PropertyName = "Greeting")]
        public string Greeting
        {
            get => _greeting;
            set
            {
                _greeting = value;
                OnPropertyChanged();
            }
        }

        #endregion greeting
    }
}
