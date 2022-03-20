namespace What2Pack.Api.Services
{
    public interface IWeatherstackService
    {

    }

    public class WeatherstackService : IWeatherstackService
    {
        private Serilog.ILogger Log { get; }

        public WeatherstackService(Serilog.ILogger log)
        {
            Log = log;
        }
    }
}
