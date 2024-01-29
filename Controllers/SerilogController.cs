using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using Serilog.Events;

namespace SerilogProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SerilogController : ControllerBase
    {
        private static readonly string[] Summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

        [HttpGet(Name = "GetSummaries")]
        public IEnumerable<Serilog> Get()
        {
            //The LogContext class in Serilog is used to add a specific context to a log entry.
            //This allows for adding extra information to log entries and creating a context that is valid for each log entry.
            using (LogContext.PushProperty("SerilogProperty", "hasan"))
            {
                Log.Information($"Serilog first log. Summaries are {string.Join(',', Summaries)}");
            }

            return Enumerable.Empty<Serilog>();

        }
        /// <summary>
        /// Get log txt from path argument.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>log.txt</returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpGet("ReadLogs", Name = "ReadLogFile")]
        public async Task<IActionResult> ReadLogFileAsync([FromQuery] string path)
        {
            try
            {
                if (String.IsNullOrEmpty(path))
                {
                    Log.Error("Path cannot be null.", path);
                    throw new ArgumentNullException("Path cannot be null.");
                }
                String filePath = Directory.GetCurrentDirectory();
                filePath += path;
                using (StreamReader reader = new StreamReader(filePath))
                {
                    var result = await reader.ReadToEndAsync();
                    Log.Information("The file readed.");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex,"Path cannot be null.");
                throw new Exception(ex.Message);
            }
        }
    }
}