using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hustex_backend.Helpers
{
    public static class LatexWriter
    {
        public async static Task<bool> SaveToFile(HttpContext context, string location)
        {
            try
            {
                var reader = new StreamReader(context.Request.Body, Encoding.UTF8);
                var body = await reader.ReadToEndAsync();

                var content = JsonConvert.DeserializeObject<string>(body);
                
                await System.IO.File.WriteAllTextAsync(location, content);

                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }
    }
}