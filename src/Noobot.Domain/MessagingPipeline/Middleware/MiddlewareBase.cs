﻿using System;
using System.Collections.Generic;
using System.Linq;
using Noobot.Domain.MessagingPipeline.Middleware.StandardMiddleware;
using Noobot.Domain.MessagingPipeline.Request;
using Noobot.Domain.MessagingPipeline.Response;

namespace Noobot.Domain.MessagingPipeline.Middleware
{
    public abstract class MiddlewareBase : IMiddleware
    {
        protected HandlerMapping[] HandlerMappings;
        private readonly IMiddleware _next;

        protected MiddlewareBase(IMiddleware next)
        {
            _next = next;
            HandlerMappings = HandlerMappings ?? new HandlerMapping[0];
        }

        protected IEnumerable<ResponseMessage> Next(IncomingMessage message)
        {
            return _next.Invoke(message);
        }

        public virtual IEnumerable<ResponseMessage> Invoke(IncomingMessage message)
        {
            foreach (var handlerMapping in HandlerMappings)
            {
                foreach (string map in handlerMapping.ValidHandles)
                {
                    string text = message.FullText;
                    if (handlerMapping.MessageShouldTargetBot)
                    {
                        text = message.TargetedText;
                    }

                    if (text.StartsWith(map, StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.WriteLine($"Matched '{map}' on '{this.GetType().Name}'");

                        foreach (var responseMessage in handlerMapping.EvaluatorFunc(message, map))
                        {
                            yield return responseMessage;
                        }

                        if (!handlerMapping.ShouldContinueProcessing)
                        {
                            yield break;
                        }
                    }
                }
            }

            foreach (ResponseMessage responseMessage in Next(message))
            {
                yield return responseMessage;
            }
        }

        public IEnumerable<CommandDescription> GetSupportedCommands()
        {
            if (!(this is AdminMiddleware))
            {
                foreach (var handlerMapping in HandlerMappings)
                {
                    yield return new CommandDescription
                    {
                        Command = string.Join(" | ", handlerMapping.ValidHandles.Select(x => $"`{x}`").OrderBy(x => x)),
                        Description = handlerMapping.Description
                    };
                }
            }

            foreach (var commandDescription in _next.GetSupportedCommands())
            {
                yield return commandDescription;
            }
        }
    }
}