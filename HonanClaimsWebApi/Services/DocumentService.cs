using HonanClaimsWebApi.Models.Claim;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace HonanClaimsWebApi.Services
{
    public class DocumentService
    {
        private const string createClaimAttachApiGet1 = "api/claim/CreateClaimAttachmentCustomerDoc?ClaimId=";
        private const string createClaimAttachApiGet2 = "&AttachmentType=";
        private const string createClaimAttachApiGet3 = "&AttachmentName=";
        private const string createClaimAttachApiGet4 = "&AttachmentDescription=";
        private const string createClaimAttachApiGet5 = "&Size=";
        private const string createClaimAttachApiGet6 = "&UserId=";
        private const string createClaimAttachApiGet7 = "&IsCustomerDoc=";
        private const string createClaimAttachApiGet8 = "api/claim/TeamGetDocs?ClaimId=";
        private const string createClaimAttachApiGet9 = "&isCustomerDocs=";

        public bool CreateClaimAttachmentCustomerDoc(ClaimAttachmentSimple attachment, out string fileCreateId)
        {
            string _fileCreateId = "";

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + createClaimAttachApiGet1 + attachment.ClaimId + createClaimAttachApiGet2 + attachment.AttachmentType
                    + createClaimAttachApiGet3 + attachment.AttachmentName + createClaimAttachApiGet4 + attachment.AttachmentDescription
                    + createClaimAttachApiGet5 + attachment.Size + createClaimAttachApiGet6 + attachment.UserId + createClaimAttachApiGet7 + attachment.IsCustomerDoc);

                request.Method = "GET";
                request.ContentType = "application/json";

                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            string response = responseReader.ReadToEnd();
                            _fileCreateId = new JavaScriptSerializer().Deserialize<string>(response);
                        }
                    }
                }

                fileCreateId = _fileCreateId;

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public List<ClaimAttachmentSimple> TeamGetDocs(string claimId, string isCustomerDocs)
        {

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    ConfigurationManager.AppSettings["apiurl"] + createClaimAttachApiGet8 + claimId + createClaimAttachApiGet9 + isCustomerDocs);

                request.Method = "GET";
                request.ContentType = "application/json";

                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            string response = responseReader.ReadToEnd();
                            return new JavaScriptSerializer().Deserialize<List<ClaimAttachmentSimple>>(response);
                        }
                    }
                }


                return null;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

    }
}
