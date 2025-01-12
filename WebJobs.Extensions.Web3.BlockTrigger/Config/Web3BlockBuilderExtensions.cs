﻿using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebJobs.Extensions.Web3.BlockTrigger.Config
{
    public static class Web3BlockBuilderExtensions
    {
        public static IWebJobsBuilder AddWeb3BlockTrigger(this IWebJobsBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.AddExtension<Web3BlockExtensionConfigProvider>();
            builder.Services
                .TryAddSingleton<StorageAccountProvider>();
            return builder;
        }
    }
}
