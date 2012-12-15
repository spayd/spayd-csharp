/**
 *  Copyright (c) 2012, SmartPayment (www.SmartPayment.com).
 */

using System.Collections.Generic;

namespace SmartPaymentFormat.String
{
    public class SmartPaymentMap : Dictionary<string, string>
    {
        public SmartPaymentMap(Dictionary<string, string> map)
        {
            foreach (string _key in map.Keys)
            {
                string key = _key.ToUpper();
                string value;
                if (!map.TryGetValue(key, out value)) continue;
                value = value.ToUpper();
                if (!key.StartsWith("X-"))
                {
                    key = "X-" + key;
                }
                Add(key, value);
            }
        }

        public string ToExtendedParams()
        {
            string returnValue = "";
            foreach (string _key in Keys)
            {
                string key = _key.ToUpper();
                string value = this[key];
                if (value == null) continue;
                if (!key.StartsWith("X-"))
                {
                    key = "X-" + key;
                }
                returnValue += key + ":" + SmartPayment.EscapeDisallowedCharacters(value) + "*";
            }
            return returnValue;
        }
    }
}