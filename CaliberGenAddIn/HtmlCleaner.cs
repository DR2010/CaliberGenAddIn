using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EAAddIn
{
    public static class HtmlCleaner
    {
        public static string StripHtmlAdvanced(string HTML)
        {
            string description = Regex.Replace(HTML,
                                               @"<html xmlns=\""http://www.w3.org/1999/xhtml\""><head /><body><p>",
                                               "");
            //description = Regex.Replace(description, "<p>", "\r\n\r\n");
            description = Regex.Replace(description, @"<p[A-Za-z0-9""=: \\]*>", "\r\n\r\n");
            description = ProcessLists(description);
            //description = Regex.Replace(description, "<li>", "\r\n\r\n");
            description = Regex.Replace(description, @"<li[A-Za-z0-9""=: \\]*>", "\r\n\r\n");
            description = Regex.Replace(description, "<[^>]*>", "");
            description = Regex.Replace(description, "&nbsp;", " ");
            description = Regex.Replace(description, "&ndash;", "-");
            description = Regex.Replace(description, "&ldquo;", "'");
            description = Regex.Replace(description, "&rdquo;", "'");

            return description;
        }

        private static string ProcessLists(string description)
        {
            return ProcessLists(description, 0, false);
        }

        private static string ProcessLists(string description, int depth, bool ordered)
        {
            int myDepth = depth + 1;
            symbol nextSymbol = GetNextSymbol(description);
            int listNumber = 1;

            while (nextSymbol != symbol.None)
            {
                #region start of list

                if (nextSymbol == symbol.OrderdList || nextSymbol == symbol.UnorderedList)
                {
                    int pos = 0;
                    //depth++;
                    if (nextSymbol == symbol.OrderdList)
                    {
                        pos = description.IndexOf(@"<ol");
                    }
                    else
                        pos = description.IndexOf(@"<ul");

                    int posEnd = description.Substring(pos).IndexOf(@">");

                    description =
                        description.Substring(0, pos) + "\r\n" +
                        ProcessLists(description.Substring(pos + posEnd + 1), myDepth, nextSymbol == symbol.OrderdList);
                }
                #endregion

                #region end of list

                else if (nextSymbol == symbol.EndOrderedList || nextSymbol == symbol.EndUnorderedList)
                {
                    int pos = 0;
                    depth--;

                    if (nextSymbol == symbol.EndOrderedList)
                    {
                        pos = description.IndexOf(@"</ol>");
                    }
                    else
                        pos = description.IndexOf(@"</ul>");

                    description =
                        description.Substring(0, pos) +
                        description.Substring(pos + 5) + "\r\n\r\n";
                    return description;
                }
                #endregion

                #region ListItem

                else if (nextSymbol == symbol.ListItem)
                {
                    int pos = description.IndexOf(@"<li");
                    int posEnd = description.Substring(pos).IndexOf(@">");

                    string prefix = string.Empty;
                    if (ordered)
                    {
                        prefix = listNumber + ") ";
                        listNumber++;
                    }
                    else
                    {
                        prefix = "* ";
                    }
                    description =
                        description.Substring(0, pos) +
                        "\r\n" + Indent(depth) + prefix + description.Substring(pos + posEnd + 1);
                }

                #endregion

                nextSymbol = GetNextSymbol(description);
            }
            return description;
        }

        private static symbol GetNextSymbol(string description)
        {
            int orderedListPos = int.MaxValue;
            int unorderedListPos = int.MaxValue;
            int listItemPos = int.MaxValue;
            int endOrderedListPos = int.MaxValue;
            int endUnorderedListPos = int.MaxValue;
            int lowestPos = int.MaxValue;

            if (description.Contains(@"<ol"))
                orderedListPos = description.IndexOf(@"<ol");
            if (description.Contains(@"<ul"))
                unorderedListPos = description.IndexOf(@"<ul");
            if (description.Contains(@"</ol>"))
                endOrderedListPos = description.IndexOf(@"</ol>");
            if (description.Contains(@"</ul>"))
                endUnorderedListPos = description.IndexOf(@"</ul>");
            if (description.Contains(@"<li"))
                listItemPos = description.IndexOf(@"<li");

            lowestPos = Math.Min(lowestPos, orderedListPos);
            lowestPos = Math.Min(lowestPos, unorderedListPos);
            lowestPos = Math.Min(lowestPos, endOrderedListPos);
            lowestPos = Math.Min(lowestPos, endUnorderedListPos);
            lowestPos = Math.Min(lowestPos, listItemPos);

            symbol nextSymbol = symbol.None;

            if (lowestPos == int.MaxValue)
                nextSymbol = symbol.None;
            else if (lowestPos == orderedListPos)
                nextSymbol = symbol.OrderdList;
            else if (lowestPos == unorderedListPos)
                nextSymbol = symbol.UnorderedList;
            else if (lowestPos == endOrderedListPos)
                nextSymbol = symbol.EndOrderedList;
            else if (lowestPos == endUnorderedListPos)
                nextSymbol = symbol.EndUnorderedList;
            else if (lowestPos == listItemPos)
                nextSymbol = symbol.ListItem;

            return nextSymbol;
        }

        private static string Indent(int depth)
        {
            string tabs = string.Empty;

            for (int i = 0; i < depth; i++)
            {
                tabs += "\t";
            }
            return tabs;
        }
        private enum symbol
        {
            OrderdList,
            UnorderedList,
            EndOrderedList,
            EndUnorderedList,
            ListItem,
            None
        } ;


        private static string StripHtmlSimple(string source)
        {
            try
            {
                string result;

                // Remove HTML Development formatting
                // Replace line breaks with space
                // because browsers inserts space
                result = source.Replace("\r", " ");
                // Replace line breaks with space
                // because browsers inserts space
                result = result.Replace("\n", " ");
                // Remove step-formatting
                result = result.Replace("\t", string.Empty);
                // Remove repeating spaces because browsers ignore them
                result = System.Text.RegularExpressions.Regex.Replace(result,
                                                                      @"( )+", " ");

                // Remove the header (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*head([^>])*>", "<head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*head( )*>)", "</head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(<head>).*(</head>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // remove all scripts (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*script([^>])*>", "<script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*script( )*>)", "</script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                //result = System.Text.RegularExpressions.Regex.Replace(result,
                //         @"(<script>)([^(<script>\.</script>)])*(</script>)",
                //         string.Empty,
                //         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<script>).*(</script>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // remove all styles (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*style([^>])*>", "<style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*style( )*>)", "</style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(<style>).*(</style>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert tabs in spaces of <td> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*td([^>])*>", "\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line breaks in places of <BR> and <LI> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*br( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*li( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line paragraphs (double line breaks) in place
                // if <P>, <DIV> and <TR> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*div([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*tr([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*p([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // Remove remaining tags like <a>, links, images,
                // comments etc - anything that's enclosed inside < >
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<[^>]*>", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // replace special characters:
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @" ", " ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&bull;", " * ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&lsaquo;", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&rsaquo;", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&trade;", "(tm)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&frasl;", "/",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&lt;", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&gt;", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&copy;", "(c)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&reg;", "(r)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove all others. More can be added, see
                // http://hotwired.lycos.com/webmonkey/reference/special_characters/
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&(.{2,6});", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // make line breaking consistent
                result = result.Replace("\n", "\r");

                // Remove extra line breaks and tabs:
                // replace over 2 breaks with 2 and over 4 tabs with 4.
                // Prepare first to remove any whitespaces in between
                // the escaped characters and remove redundant tabs in between line breaks
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)( )+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\t)( )+(\t)", "\t\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\t)( )+(\r)", "\t\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)( )+(\t)", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove redundant tabs
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)(\t)+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove multiple tabs following a line break with just one tab
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)(\t)+", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Initial replacement target string for line breaks
                string breaks = "\r\r\r";
                // Initial replacement target string for tabs
                string tabs = "\t\t\t\t\t";
                for (int index = 0; index < result.Length; index++)
                {
                    result = result.Replace(breaks, "\r\r");
                    result = result.Replace(tabs, "\t\t\t\t");
                    breaks = breaks + "\r";
                    tabs = tabs + "\t";
                }

                // That's it.
                return result;
            }
            catch
            {
                MessageBox.Show("Error");
                return source;
            }
        }
    }
}