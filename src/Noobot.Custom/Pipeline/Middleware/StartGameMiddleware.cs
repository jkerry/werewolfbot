using System.Collections.Generic;
using Noobot.Domain.MessagingPipeline.Middleware;
using Noobot.Domain.MessagingPipeline.Request;
using Noobot.Domain.MessagingPipeline.Response;

namespace Noobot.Custom.Pipeline.Middleware
{
    public class StartGameMiddleware : MiddlewareBase
    {
        private Domain.Slack.ISlackWrapper _wrapper;

        public StartGameMiddleware(Domain.Slack.ISlackWrapper slackWrapper, IMiddleware next) : base(next)
        {
            _wrapper = slackWrapper;
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
            yield return message.ReplyToChannel("There be lycanthropes about eh? Let's create a place to wrangle the villagers in.");
            _wrapper.CreateGameChannel().Wait();
            yield return message.ReplyToChannel("We've boarded the chapel up. Let's get'em in there...");
            yield return message.ReplyToChannel("John's not smart enough to know how to invite people...  yet...");
            yield return message.ReplyToChannel("Those unholy mutts need somewhere to talk too, I suppose.");
            _wrapper.CreateWerewolfGroup().Wait();
            yield return message.ReplyToChannel("Building the pack...");
            yield return message.ReplyToChannel("John's not smart enough to know how to invite people...  yet...");
            yield return message.ReplyToChannel("Let the witch hunt begin!");
        }
    }
}