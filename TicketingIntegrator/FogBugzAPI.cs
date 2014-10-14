using System.IO;
using System.Net;

namespace TicketingIntegrator
{
	class FogBugzApi
	{
		public string MakeRequest(string url, string request)
		{
            string output = "";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + request);
            httpWebRequest.Method = WebRequestMethods.Http.Post;
            httpWebRequest.Accept = "application/xml";
            httpWebRequest.ContentLength = 0;

            var response = (HttpWebResponse)httpWebRequest.GetResponse();
            var streamReader = new StreamReader(response.GetResponseStream());

            output = streamReader.ReadToEnd();

            return output;
		}
	}
}
