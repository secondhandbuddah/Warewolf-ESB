﻿using System;
using System.Text;
using Dev2.Common.Interfaces.Core.Convertors.Base;

namespace Dev2.Converters
{
    internal class Dev2HexConverter : IBaseConverter
    {

        public string ConvertToBase(byte[] payload)
        {
            StringBuilder result = new StringBuilder("0x");

            foreach(byte t in payload)
            {
                result.Append(Convert.ToString(t, 16));
            }

            return result.ToString();
        }

        public byte[] NeutralizeToCommon(string payload)
        {
            // strip 0x from string if present
            if(payload.StartsWith("0x") || payload.StartsWith("0X"))
            {
                payload = payload.Substring(2);
            }


            byte[] result = new byte[(payload.Length / 2)];

            int pos = 0;
            for(int i = 0; i < payload.Length; i += 2)
            {
                result[pos] = Convert.ToByte((payload.Substring(i, 2)), 16);
                pos++;
            }

            return result;
        }

        public bool IsType(string payload)
        {
            bool result = (System.Text.RegularExpressions.Regex.IsMatch(payload, @"\A\b[0-9a-fA-F]+\b\Z") || System.Text.RegularExpressions.Regex.IsMatch(payload, @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"));

            if((payload.Length % 2) != 0)
            {
                result = false;
            }
            return result;
        }

        public Enum HandlesType()
        {
            return enDev2BaseConvertType.Hex;
        }
    }
}
