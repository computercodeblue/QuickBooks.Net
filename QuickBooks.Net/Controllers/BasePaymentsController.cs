using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OAuth;

using QuickBooks.Net.Exceptions;
using QuickBooks.Net.Data.Error_Responses;
using QuickBooks.Net.Utilities;

namespace QuickBooks.Net.Controllers
{
    public abstract class BasePaymentsController
    {

        #region Url Constants

        private const string QbPaymentsSandboxUrlV4 = "https://sandbox.api.intuit.com/quickbooks/v4";
        private const string QbPaymentsUrlV4 = "https://api.intuit.com/quickbooks/v4";

        #endregion

        private readonly string _oAuthVersion;

        private string Url
        {
            get
            {
                var baseUrl = Client.SandboxMode ? QbPaymentsSandboxUrlV4 : QbPaymentsUrlV4;
                return $"{baseUrl}/";
            }
        }

        protected QuickBooksClient Client;

        protected BasePaymentsController(QuickBooksClient client, string oAuthVersion)
        {
            Client = client;
            _oAuthVersion = oAuthVersion;
        }

        protected async Task<T> MakeRequest<T>(string objectName, string resourceUrl, HttpMethod requestMethod, object content = null, bool update = false, bool delete = false)
        {
            var url = Url + resourceUrl;

            var accept = "application/json";

            var client = url.WithHeaders(new
            {
                Accept = accept
            });

            client.Headers.Add("Request-Id", System.Guid.NewGuid().ToString());

            if (objectName != "Token")
                client.Headers.Add("Authorization", GetAuthHeader(url, requestMethod));

            try
            {
                if (requestMethod == HttpMethod.Get)
                {

                    var objectResponse = await client.GetJsonAsync<JObject>();
                    return objectResponse[objectName].ToObject<T>();
                }

                if (requestMethod == HttpMethod.Post)
                {
                    var response = await client.PostJsonAsync(content);
                    
                    var responseContentString = await response.Content.ReadAsStringAsync();
                    var responseContent = JsonConvert.DeserializeObject<JObject>(responseContentString);
                    return responseContent[objectName].ToObject<T>();
                }

                return default(T);
            }
            catch (FlurlHttpTimeoutException ex)
            {
                throw new QuickBooksTimeoutException("The QuickBooks request timed out.", ex.Message);
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.Response == null)
                {
                    throw new Exception(
                        "The request failed to get a response. You may need to check your internet connection.");
                }

                //The XML parsing is because QuickBooks only returns XML on unauthorized exceptions
                if (ex.Call.Response.Content.Headers.ContentType.MediaType == "text/xml")
                {
                    var xml = XmlHelper.ParseXmlString(ex.Call.Response.Content.ReadAsStringAsync().Result);
                    var errorCode = xml["Message"].Split(';')[1].Split('=')[1];
                    throw new QuickbooksAuthorizationException($"QuickBooks application authentication failed. Message: {xml["Message"]}",
                        xml["Detail"], errorCode);
                }

                var response =
                    JsonConvert.DeserializeObject<QuickBooksErrorResponse>(
                        ex.Call.Response.Content.ReadAsStringAsync().Result);

                throw new QuickBooksException("A Quickbooks exception occurred.", response);
            }
        }


        private string GetAuthHeader(string url, HttpMethod method)
        {
            return new OAuthRequest
            {
                Version = _oAuthVersion,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ConsumerKey = Client.ConsumerKey,
                ConsumerSecret = Client.ConsumerSecret,
                Token = Client.AccessToken,
                TokenSecret = Client.AccessTokenSecret,
                Method = method.ToString(),
                RequestUrl = url
            }
            .GetAuthorizationHeader();
        }
    }
}