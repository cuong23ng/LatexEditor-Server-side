using System.Text;
using Newtonsoft.Json;

namespace Hustex_backend.Helpers
{
    public static class LatexWriter
    {
        public async static Task<bool> SaveToFile(HttpContext context, string path)
        {
            try
            {
                var reader = new StreamReader(context.Request.Body, Encoding.UTF8);
                var body = await reader.ReadToEndAsync();

                var content = JsonConvert.DeserializeObject<string>(body);
                
                await File.WriteAllTextAsync(path, content);

                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }
    }
}