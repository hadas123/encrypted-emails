using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EAGetMail;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Globalization;

namespace Mailbox
{
    class Sing
    {
        const string clientID = "your client id";
        const string clientSecret = "your client secret";
        const string scope = "openid%20profile%20email%20https://mail.google.com";
        const string authUri = "https://accounts.google.com/o/oauth2/v2/auth";
        const string tokenUri = "https://www.googleapis.com/oauth2/v4/token";
        string user;
        string accessToken;
        public async Task<bool> DoOauthAndRetrieveEmail()
        {
            Conect.conect();

            // Creates the OAuth 2.0 authorization request.
            string authorizationRequest = string.Format("{0}?response_type=code&scope={1}&redirect_uri={2}&client_id={3}",
                authUri,
                scope,
                Uri.EscapeDataString(Conect.redirectUri),
                clientID
            );

            // Opens request in the browser.
            System.Diagnostics.Process.Start(authorizationRequest);

            // Waits for the OAuth authorization response.
            var context = await Conect.http.GetContextAsync();



            // Sends an HTTP response to the browser.
            var response = context.Response;
            string responseString = string.Format("<html><head></head><body>Please return to the app and close current window.</body></html>");
            var buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            var responseOutput = response.OutputStream;
            Task responseTask = responseOutput.WriteAsync(buffer, 0, buffer.Length).ContinueWith((task) =>
            {
                responseOutput.Close();
                Conect.http.Stop();
                Console.WriteLine("HTTP server stopped.");
            });

            // Checks for errors.
            if (context.Request.QueryString.Get("error") != null)
            {
                Console.WriteLine(string.Format("OAuth authorization error: {0}.", context.Request.QueryString.Get("error")));
                return false;
            }

            if (context.Request.QueryString.Get("code") == null)
            {
                Console.WriteLine("Malformed authorization response. " + context.Request.QueryString);
                return false;
            }

            // extracts the code
            var code = context.Request.QueryString.Get("code");
           

            string responseText = await RequestAccessToken(code, Conect.redirectUri);
            

            OAuthResponseParser parser = new OAuthResponseParser();
            parser.Load(responseText);

            user = parser.EmailInIdToken;
            accessToken = parser.AccessToken;

           
            File.WriteAllText(@"userEmail", user);
            File.WriteAllText(@"acssesToken", accessToken);

            return true;
            
        }

        async Task<string> RequestAccessToken(string code, string redirectUri)
        {


            Console.WriteLine("Exchanging code for tokens...");

            // builds the  request
            string tokenRequestBody = string.Format("code={0}&redirect_uri={1}&client_id={2}&client_secret={3}&grant_type=authorization_code",
                code,
                Uri.EscapeDataString(redirectUri),
                clientID,
                clientSecret
                );

            // sends the request
            HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create(tokenUri);
            tokenRequest.Method = "POST";
            tokenRequest.ContentType = "application/x-www-form-urlencoded";
            tokenRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            byte[] _byteVersion = Encoding.ASCII.GetBytes(tokenRequestBody);
            tokenRequest.ContentLength = _byteVersion.Length;

            Stream stream = tokenRequest.GetRequestStream();
            await stream.WriteAsync(_byteVersion, 0, _byteVersion.Length);
            stream.Close();

            try
            {
                // gets the response
                WebResponse tokenResponse = await tokenRequest.GetResponseAsync();
                using (StreamReader reader = new StreamReader(tokenResponse.GetResponseStream()))
                {
                    // reads response body
                    return await reader.ReadToEndAsync();
                }

            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response != null)
                    {
                        Console.WriteLine("HTTP: " + response.StatusCode);
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            // reads response body
                            string responseText = await reader.ReadToEndAsync();
                            Console.WriteLine(responseText);
                        }
                    }
                }

                throw ex;
            }
        }

    }
}
