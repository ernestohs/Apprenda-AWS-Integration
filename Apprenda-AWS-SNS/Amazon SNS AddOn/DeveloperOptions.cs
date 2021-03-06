﻿using System;
using System.Collections.Generic;

namespace Apprenda.SaaSGrid.Addons.AWS.SNS
{
    public class DeveloperOptions
    {
        // Amazon Credentials. Required for IAM. 
        public string AccessKey { get; set; }
        public string SecretAccessKey { get; set; }
        public string TopicName { get; set; }

        public string PlatformApplicationName { get; set; }

        // we'll handle this in 1.1. just topic creation for now.
        public Dictionary<string, string> PlatformAttributes { get; set; }

        public string MessagingPlatform { get; set; }

        public string PlatformApplicationArn { get; set; }

        public string CustomUserData { get; set; }

        public string Token { get; set; }

        public Dictionary<string, string> EndpointAttributes { get; set; }

        // Method takes in a string and parses it into a DeveloperOptions class.
        public static DeveloperOptions Parse(string developerOptions)
        {
            DeveloperOptions options = new DeveloperOptions();

            if (!string.IsNullOrWhiteSpace(developerOptions))
            {
                var optionPairs = developerOptions.Split(new []{'&'}, StringSplitOptions.RemoveEmptyEntries);
                foreach (var optionPair in optionPairs)
                {
                    var optionPairParts = optionPair.Split(new[]{'='}, StringSplitOptions.RemoveEmptyEntries);
                    if (optionPairParts.Length == 2)
                    {
                        MapToOption(options, optionPairParts[0].Trim().ToLowerInvariant(), optionPairParts[1].Trim());
                    }
                    else
                    {
                        throw new ArgumentException(
                            string.Format(
                                "Unable to parse developer options which should be in the form of 'option=value&nextOption=nextValue'. The option '{0}' was not properly constructed",
                                optionPair));
                    }
                }
            }

            return options;
        }

        // Interior method takes in instance of DeveloperOptions (aptly named existingOptions) and maps them to the proper value. In essence, a setter method.
        private static void MapToOption(DeveloperOptions existingOptions, string key, string value)
        {
            if ("accesskey".Equals(key))
            {
                existingOptions.AccessKey = value;
                return;
            }

            if ("secretkey".Equals(key))
            {
                existingOptions.SecretAccessKey = value;
                return;
            }

            if("topicname".Equals(key))
            {
                existingOptions.TopicName = value;
                return;
            }

            if ("platformapplicationname".Equals(key))
            {
                existingOptions.PlatformApplicationName = value;
                return;
            }
            if ("platformapplicationarn".Equals(key))
            {
                existingOptions.PlatformApplicationArn = value;
                return;
            }
            if ("existingoptions".Equals(key))
            {
                existingOptions.Token = value;
                return;
            }
            if ("customuserdata".Equals(key))
            {
                existingOptions.CustomUserData = value;
                return;
            }
            throw new ArgumentException(string.Format("The developer option '{0}' was not expected and is not understood.", key));
        }

        
    }
}