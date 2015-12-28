using Noobot.Custom.Pipeline.Middleware;
using Noobot.Domain.MessagingPipeline;

namespace Noobot.Custom
{
    public class PipelineManager : PipelineManagerBase
    {
        protected override void Initialise()
        {
            //Use<AutoResponderMiddleware>();
            Use<WelcomeMiddleware>();
            Use<JokeMiddleware>();
            Use<StartGameMiddleware>();
        }
    }
}