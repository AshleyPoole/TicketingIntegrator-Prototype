using System;
using System.Linq;
using System.Xml.Linq;

namespace TicketingIntegrator
{
    class XmlHelper
    {
        public Boolean CheckNodeExistence(XDocument doc, string element)
        {
            return doc.Descendants(element).Any();
        }

        public string GetAttribute(XDocument doc, string node, string attribute)
        {
            try
            {
                return doc.Root.Element(node).Attribute(attribute).Value;
            }
            catch
            {
                return "ERROR - Unable to read atrributes value";
            }
        }

        public string GetElement(XDocument doc, string element)
        {
			try
			{
				return doc.Root.Element(element).Value;
			}
			catch
			{
				return "ERROR - Unable to get element";
			}
        }
    }
}
