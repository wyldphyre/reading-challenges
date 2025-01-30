﻿namespace Challenges.Api.Sdk;

public static class ApiEndpoints
{
    private const string ApiBase = "/api";

    public static class Challenges
    {
        private const string Base = $"{ApiBase}/challenges";

        public const string Create = Base;

        public const string Get = $"{Base}/{{id}}";
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
    }
}