using System;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Microsoft.Phone.Tasks;

namespace WordPressStarterKit
{
    //credit to: http://www.ben.geek.nz for this WebBrowserHelper class to open WebBrowser Control links into full IE browser.
    public class WebBrowserHelper
    {
        public static string NotifyScript
        {
            get
            {
                return @"<script>
                    window.onload = function()
                    {
                        a = document.getElementsByTagName('a');
                        for(var i=0; i < a.length; i++){
                            var msg = a[i].href;
                            a[i].onclick = function() {notify(msg);};
                        }
                    }
                    function notify(msg)
                    {
	                    window.external.Notify(msg);
	                    event.returnValue=false;
	                    return false;
                    }
                    </script>";
            }
        }

        public static string WrapHtml(string htmlSubString, double viewportWidth)
        {
            var html = new StringBuilder();
            html.Append("<html>");
            html.Append(HtmlHeader(viewportWidth));
            html.Append("<body>");
            html.Append(htmlSubString);
            html.Append("</body>");
            html.Append("</html>");
            return html.ToString();
        }

        public static string HtmlHeader(double viewportWidth)
        {
            var head = new StringBuilder();
            head.Append("<head>");
            head.Append("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1, maximum-scale=1\">");
            head.Append("<style>");
            head.Append(string.Format(
                "body {{background-color:#188AA5;color:{1};font-family:'Segoe WP';margin:0;padding:0 }}",
                GetBrowserColor("PhoneBackgroundColor"),
                GetBrowserColor("PhoneForegroundColor"),
                (double)Application.Current.Resources["PhoneFontSizeNormal"]));
            head.Append(string.Format(
                "a {{color:{0}}} a:link {{color:white}} a:visited {{color:white}} a:active{{color:white}}",
                GetBrowserColor("PhoneAccentColor")));
            head.Append("</style>");
            head.Append(NotifyScript);
            head.Append("</head>");

            return head.ToString();
        }

        public static void OpenBrowser(string url)
        {
            WebBrowserTask webBrowserTask = new WebBrowserTask { URL = url };
            webBrowserTask.Show();
        }

        private static string GetBrowserColor(string sourceResource)
        {
            var color = (Color)Application.Current.Resources[sourceResource];

            return "#" + color.ToString().Substring(3, 6);
        }
    }
}