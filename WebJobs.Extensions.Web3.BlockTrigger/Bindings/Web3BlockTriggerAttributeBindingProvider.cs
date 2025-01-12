﻿using Microsoft.Azure.WebJobs.Host.Triggers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nethereum.RPC.Eth.DTOs;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebJobs.Extensions.Web3.BlockTrigger.Bindings
{
    public class Web3BlockTriggerAttributeBindingProvider : ITriggerBindingProvider
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;

        public Web3BlockTriggerAttributeBindingProvider(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        public Task<ITriggerBinding> TryCreateAsync(TriggerBindingProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var attribute = context.Parameter.GetCustomAttribute<Web3BlockTriggerAttribute>(false);
            if (attribute == null)
                return Task.FromResult<ITriggerBinding>(null);

            if(context.Parameter.ParameterType != typeof(BlockWithTransactions))
                throw new InvalidOperationException(string.Format("Can't bind {0} to type '{1}'",
                    nameof(Web3BlockTriggerAttribute), context.Parameter.ParameterType));

            var binding = new Web3BlockTriggerBinding(context.Parameter, _configuration, _loggerFactory);
            return Task.FromResult<ITriggerBinding>(binding);
        }
    }
}
