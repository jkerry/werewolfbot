using System.Collections.Generic;
using Noobot.Domain.MessagingPipeline.Middleware;
using Noobot.Domain.MessagingPipeline.Request;
using Noobot.Domain.MessagingPipeline.Response;

namespace Noobot.Custom.Pipeline.Middleware
{
    public class StartGameMiddleware : MiddlewareBase
    {
        public StartGameMiddleware(IMiddleware next) : base(next)
        {
            HandlerMappings = new []
            {
                new HandlerMapping
                {
                    ValidHandles = new [] { "start game"},
                    Description = "Starts a werewolf game if one isn't already running",
                    EvaluatorFunc = StartGameHandler,
                    MessageShouldTargetBot = true,
                    ShouldContinueProcessing = true
                }
            };
        }

        private IEnumerable<ResponseMessage> StartGameHandler(IncomingMessage message, string matchedHandle)
        {
            yield return message.ReplyToChannel("I'd totally start a game if @richard or @john would get off their rumps and get the game defined.");
        }
    }
}