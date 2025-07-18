﻿using System;
using System.Net.Http;

namespace MyApp.Core.Utilities
{
    public class AppConstants
    {
        public const string ApiBaseUrl = "https://api.myapp.com/";
        public const string DEV_CONNECTION_NAME = "DefaultConnection";
        public const string DefaultDateFormat = "yyyy-MM-dd";
        public const int MaxRetryAttempts = 3;
        public const int RetryDelayMilliseconds = 2000;

        public static readonly HttpClient HttpClient = new HttpClient
        {
            BaseAddress = new Uri(ApiBaseUrl),
            Timeout = TimeSpan.FromSeconds(30)
        };

        // Add other constants as needed

    }
}