﻿namespace SkillProfiWeb.Data
{
    public abstract class DataApi
    {
        internal HttpClient client { get; set; } = new HttpClient();
        internal string url { get; set; } = "http://46.50.174.117:5000/api/";
        internal static string Token { get; set; }
    }
}
