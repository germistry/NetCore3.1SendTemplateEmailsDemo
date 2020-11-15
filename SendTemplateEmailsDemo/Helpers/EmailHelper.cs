using System.IO;

namespace SendTemplateEmailsDemo.Helpers
{
    public class EmailHelper
    {
        public static string BuildTemplate(string path, string template)
        {
            string fullPath = Path.Combine(path, template);
            
            StreamReader str = new StreamReader(fullPath);
            string mailText = str.ReadToEnd();
            str.Close();
            
            return mailText;
        }
    }
}
