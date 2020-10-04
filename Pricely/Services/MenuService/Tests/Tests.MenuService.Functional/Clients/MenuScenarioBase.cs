using System;
using Tests.MenuService.Functional.Infrastructure;

namespace Tests.MenuService.Functional.Clients
{
    public class MenuScenarioBase : ScenarioBase
    {
        public static string BaseUrl => "api/Menu";

        public static class Get
        {
            public static string GetById(Guid id) => $"{BaseUrl}/{id}";
            public static string GetAll => $"{BaseUrl}";
        }

        public static class Post
        {
            public static string CreateMenu = $"{BaseUrl}";
        }

        public static class Put
        {
            public static string UpdateMenu => $"{BaseUrl}";
        }

        public static class Delete
        {
            public static string DeleteMenu(Guid id) => $"{BaseUrl}/{id}";
        }
    }
}
