using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace TicketingIntegrator
{
    class FogBugz
    {
	    readonly FogBugzApi _fogBugzApi = new FogBugzApi();
	    readonly JsonHelper _jsonHelper = new JsonHelper();
	    readonly XmlHelper _xmlHelper = new XmlHelper();

        public string InsertCase(string apiUrl, string apiRequest)
        {
            string output = "";

            // Inserting case to FogBugz through the API and gathering result (XML)
            var xResult = XDocument.Parse(_fogBugzApi.MakeRequest(apiUrl, apiRequest));

            // Checking XML to check for existence of an error element indicating a failure
            if (!_xmlHelper.CheckNodeExistence(xResult, "error"))
            {
                //output = jsonHelper.CreateSuccessResponse(xmlHelper.GetAttribute(xResult, "case", "ixBug"));
                output = _jsonHelper.CreateResponse(_xmlHelper.GetAttribute(xResult, "case", "ixBug"));
            } 
            else
            {
               output = _jsonHelper.CreateFailureResponse(_xmlHelper.GetElement(xResult, "error")); // ** Need to do something different here maybe - To return error to ServiceDesk **
            }

            return output;
        }


        public string CreateRequestStringFromJson(JObject myJsonData, string apiToken)
        {
            string myRequestString = "";
            
            myRequestString = AddParameter(myRequestString, "token", apiToken);
            myRequestString = AddParameter(myRequestString, "cmd", "new");
            myRequestString = AddParameter(myRequestString, "sTitle", _jsonHelper.GetParamater(myJsonData, "SUBJECT"));
            
            // **In future versions we could put logic here depending on what is set in ServiceDesk
            myRequestString = AddParameter(myRequestString, "sProject", "inbox"); // Mapping to 'inbox' project within FogBugz 
            myRequestString = AddParameter(myRequestString, "sPriority", "3 - Dev-Top Priority");
            myRequestString = AddParameter(myRequestString, "ixCategory", "1");


            //Building up events string from ServiceDesk data -  https://forums.manageengine.com/topic/external-action-plugin-feature-in-sdp
            var eventString = string.Format("ServiceDesk Case ID: {1}{0}Requester: {2}{0}Created By: {3}{0}Technican: {4}{0}Status: {5}{0}Impact: {6}{0}Impact Details: {7}{0}{0}Description: {8}",
                                            "\r\n",
                                            _jsonHelper.GetParamater(myJsonData, "WORKORDERID"),
                                            _jsonHelper.GetParamater(myJsonData, "REQUESTER"),
                                            _jsonHelper.GetParamater(myJsonData, "CREATEDBY"),
                                            _jsonHelper.GetParamater(myJsonData, "TECHNICIAN"),
                                            _jsonHelper.GetParamater(myJsonData, "STATUS"),
                                            _jsonHelper.GetParamater(myJsonData, "IMPACT"),
                                            _jsonHelper.GetParamater(myJsonData, "IMPACTDETAILS"),
                                            _jsonHelper.GetParamater(myJsonData, "SHORTDESCRIPTION")
                                            );


            myRequestString = AddParameter(myRequestString, "sEvent", eventString);
            
            return myRequestString;
        }


        static string AddParameter(string currentParameter, string parameterName, string parameterValue)
        {
            if (currentParameter.Length == 0)
            {
                currentParameter = string.Format("?{1}={2}", currentParameter, parameterName, parameterValue);
            }
            else
            {
                currentParameter = string.Format("{0}&{1}={2}", currentParameter, parameterName, parameterValue);
            }

            return currentParameter;
        }
    }
}
