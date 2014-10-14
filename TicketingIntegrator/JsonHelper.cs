using System;
using Newtonsoft.Json.Linq;


namespace TicketingIntegrator
{
    class JsonHelper
    {
        public JObject ParseJsonString(string rawJsonData)
        {
            JObject jsonDataObject;

            try
            {
                jsonDataObject = JObject.Parse(rawJsonData);
            }
            catch (Exception ex)
            {
                // ** DO something here to return error ** Write to log file too! **
                jsonDataObject = JObject.Parse(""); // ** This errors. Fix it.
            }

            return jsonDataObject;
        }


        public string CreateSuccessResponse(string input)
        {
            var output =
                new JObject(
                    new JProperty("message", string.Format("Success! FogBugz ID {0}", input)),
                    new JProperty("operation",
                        new JArray(
                             new JObject(
                                new JProperty("INPUT_DATA",
                                    new JArray(
                                        new JObject(
                                            new JProperty("FogBugzID", input)
                                        )
                                    )
                                ),
                            new JProperty("OPERATIONNAME", "UPDATE_REQUEST")
                            ),
                            new JObject(
                                new JProperty("INPUT_DATA",
                                    new JArray(
                                        new JObject(
                                            new JProperty("notes",
                                                new JObject(
                                                    new JProperty("notesText", string.Format("Case has been referred to FogBugz with case ID {0}. Please see FogBugz case for future updates.", input))
                                                )
                                            )
                                        )
                                    )
                                ),
                            new JProperty("OPERATIONNAME", "ADD_NOTE")
                            )
                        )
                    )
                );

            return output.ToString();
        }


        public string CreateFailureResponse(string input)
        {
            var output =
                new JObject(
                    new JProperty("message", string.Format("ERROR! ({0})", input)),
                    new JProperty("operation",
                        new JArray(
                             new JObject(
                                new JProperty("INPUT_DATA",
                                    new JArray(
                                        new JObject(
                                            new JProperty("notesText", string.Format("Error creating case in FogBugz, Please seak assistance for TicketingIntegrator. Error - {0}.", input))
                                        )
                                    )
                                )
                            )
                        )
                    )
                );

            return output.ToString();
        }


        public string GetParamater(JObject myJsonData, string paramater)
        {
            string output;

            try
            {
                output = myJsonData[paramater].ToString();
            }
            catch
            {
                output = "NULL";
            }

            // Setting to 'NULL' if length of parsed data
            if (output.Length == 0)
            {
                output = "NULL";
            }

            return output;
        }

        
        public string CreateResponse(string input)
        {
            var output = new JObject(new JProperty("fogbugzid", input));

            return output.ToString();
        }
    }
}
