
namespace TicketingIntegrator
{
    class Processor
    {
        static readonly FogBugz FogBugz = new FogBugz();
        static readonly JsonHelper JsonHelper = new JsonHelper();

        public string CreateCase(string rawJson, string fogBugzUrl, string fogBugzToken)
        {
	        if (rawJson.Length == 0)
            {
                // If input json length is zero then returning error 
                return JsonHelper.CreateFailureResponse("NO INPUT PROVIDED");
            }

            if ((!rawJson.Contains("{")) && (!rawJson.Contains("}"))) // ** Test Parse instead of this dirty check
            {
                // Cannot be a valid JSON so returning error
                return JsonHelper.CreateFailureResponse("NO VALID JSON DETECTED");
            }


             // Parsing Json data into JObject object
             var jsonCaseData = JsonHelper.ParseJsonString(rawJson);

             // Creating FB case request
             var myRequest = FogBugz.CreateRequestStringFromJson(jsonCaseData, fogBugzToken);

             // Insert FB case and gather json output - Long hand writing to string first though I prefer this
             var output = FogBugz.InsertCase(fogBugzUrl, myRequest);

            return output;
        }
    }
}
