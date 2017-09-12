using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winsell.Hopi.API.HopiWS
{
    public static class HopiResponseClasses
    {
        #region DetailClass

        public class FaultErrorServiceError
        {
            public string code { get; set; }

            public string description { get; set; }
        }

        public class FaultErrorDetail
        {
            public FaultErrorServiceError ServiceError = new FaultErrorServiceError();
        }

        public class JoinedCampaign
        {
            public string code { get; set; }

            public string type { get; set; }

            public double multiplier { get; set; }

            public decimal limit { get; set; }
        }

        #endregion

        #region MainClass

        public class FaultError
        {
            public string faultCode { get; set; }

            public string faultString { get; set; }

            public FaultErrorDetail detail = new FaultErrorDetail();
        }

        public class UserInfo
        {
            public long birdId { get; set; }

            public string customerId { get; set; }

            public decimal coinBalance { get; set; }

            public string provisionTokens { get; set; }

            public decimal availableCoinBalance { get; set; }

            public List<JoinedCampaign> joinedCampaigns { get; set; }
        }

        public class HopiProvision
        {
            public ulong provisionId { get; set; }

            public bool otpNeeded { get; set; }
        }

        public class ReturnInfo
        {
            public decimal residual { get; set; }

            public ulong returnTrxId { get; set; }
        }

        #endregion

    }
}
