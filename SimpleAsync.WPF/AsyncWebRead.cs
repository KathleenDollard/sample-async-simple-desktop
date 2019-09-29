using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimpleAsync
{
    public class AsyncWebReadClass
    {
        private IDisplay _display;

        public AsyncWebReadClass(IDisplay display)
        {
            _display = display;
        }

        public async Task<string> Run()
        {
            WebClient client = new WebClient();

            client.BaseAddress = "https://www.msudenver.edu/math/";

            _display.DisplayText($"b. Attempting to get page {Utils.CurrentThread}");
            try
            {
                var result = await client.DownloadStringTaskAsync(
                    new Uri("/", UriKind.Relative));
                _display.DisplayText($"c. Page retrieved {Utils.CurrentThread}");
                return GetPageTitle(result);
            }
            finally
            {
                _display.DisplayText($"d. Done {Utils.CurrentThread}");
            }
        }

        private string GetPageTitle(string page)
        {
            var regex = new Regex(@"<title>(?<text>.*)</title>");
            var match = regex.Match(page);
            if (match == null) return null;
            var group = match.Groups["text"];
            if (group == null) return null;
            return group.Value;
        }
    }
}
