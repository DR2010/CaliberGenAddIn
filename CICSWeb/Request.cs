using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace CICSWeb.Net
{
    public static class Request
    {
        private const string HISECRET = "HISECRET";
        private const uint MAXEXPORTEXTRAS = 800;
        public static string Build(IWrapperConfig data)
        {
            var builder = new StringBuilder();
            string ticket = GetTicket(data);
            //tiket
            builder.AppendFormat("\tSTICKET={0}", ticket);
            //hash ticket
            builder.AppendFormat("\tSHKEY={0}", GetHash(ticket));

            builder.AppendFormat("\tMAXEXPORT={0}", 1292);

            builder.AppendFormat("\tDEBUG={0}", 0);

            builder.AppendFormat("\tRIGHTSIZE={0}", "A");

            builder.AppendFormat("\tMODINTEG={0}", new string(Encoding.ASCII.GetChars(data.Integrity)));

            string ip = string.Format("{0}", System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())[0].ToString());
            builder.AppendFormat("\tRETURNIP={0}", ip);

            builder.AppendFormat("\tTERMINALID={0}", string.Empty);

            builder.AppendFormat("\tUSERID={0}", Environment.UserName);

            builder.AppendFormat("\tTRAN={0}", Environment.UserName.Substring(0,4));

            builder.AppendFormat("\tAPPLICATIONID={0}", "ESC3");

            builder.AppendFormat("\tLEFTSIZE={0}", "A");

            builder.AppendFormat("\tRTRACTION={0}", "EXECUTE");

            builder.Append(BuildParameters(data.ChildrenImport));

            builder.Append("\x0000");
            

            return builder.ToString();
        }
        private static string BuildParameters(CICSParameterCollection collection)
        {
            var builder = new StringBuilder();
            foreach (var item in collection.Dictionary)
            {
                if (string.IsNullOrEmpty(item.Value.Value)) continue;
                builder.AppendFormat("\t{0}={1}", item.Value.ArrayKey, item.Value.Value);
                builder.Append(BuildParameters(item.Value.Children));
            }
            return builder.ToString();
        }
        private static string GetTicket(IWrapperConfig data)
        {
            var site = "USRS";
            return String.Format("/USER={0}/DATE={1}/TIME={2}/SITE={3}/SECGRP={4}", Environment.UserName
                , Helper.CicsDateTimeToString(DateTime.UtcNow)
                , Helper.CicsTimeToString(DateTime.UtcNow)
                , site
                , "DAD");
        }
        private static string GetHash(string ticket)
        {
            // Generate ticket with shared secret.
            string ticketWithSecret = ticket + HISECRET;

            var hash = new MD5CryptoServiceProvider();
            byte[] hashedBytes = hash.ComputeHash(Encoding.UTF8.GetBytes(ticketWithSecret));

            var hashHex = new StringBuilder();
            for (int iCount = 0; iCount < hashedBytes.Length; iCount++)
            {
                hashHex.AppendFormat("{0:x2}", hashedBytes[iCount]);
            }

            return hashHex.ToString();
        }

    }
}
