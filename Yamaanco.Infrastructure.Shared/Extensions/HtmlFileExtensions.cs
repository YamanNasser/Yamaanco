using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yamaanco.Infrastructure.Shared.Extensions
{
    public static class HtmlFileExtensions
    {
        //it can be used with   http://www.github.com/ekito/bootstrap-linkpreview
        //to get the url html.
        public static async Task<string> GetHtmlCodeForUrl(this string fullUrl)
        { 
            System.Net.WebResponse Result = null;
            string htmlContent;
            try
            {
                System.Net.WebRequest req = System.Net.WebRequest.Create(fullUrl);
                Result = req.GetResponse();
                System.IO.Stream RStream = Result.GetResponseStream();
                System.IO.StreamReader sr = new System.IO.StreamReader(RStream);
                new System.IO.StreamReader(RStream);
                htmlContent = await sr.ReadToEndAsync();
                sr.Dispose();
            }
            catch (Exception)
            {
                htmlContent = $"<br><a href='{fullUrl}'>{fullUrl}</a>";
            }
            finally
            {
                if (Result != null)
                {
                    Result.Close();
                }
            }
            return htmlContent;
        }
    }
}
