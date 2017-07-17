using System.Linq;
using Chayns.Backend.Api.Models.Data.Base;

namespace Chayns.Backend.Api.Models.Data
{
    public class UserAccessTokenDataGet : DefaultData
    {
        public UserAccessTokenDataGet(int locationId, params int[] requiredUacGroups) : base(locationId)
        {
            if (requiredUacGroups != null && requiredUacGroups.Length > 0)
            {
                RequiredUacGroups = requiredUacGroups.ToArray();
            }
        }

        public UserAccessTokenDataGet(string siteId, params int[] requiredUacGroups) : base(siteId)
        {
            if (requiredUacGroups != null && requiredUacGroups.Length > 0)
            {
                RequiredUacGroups = requiredUacGroups.ToArray();
            }
        }

        #region RequiredUacGroups

        private int[] _requiredUacGroups;

        public int[] RequiredUacGroups
        {
            get => _requiredUacGroups;
            set
            {
                _requiredUacGroups = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
