using BookApp.Entities;
using System.Text.Json;

namespace BookApp.Repositories.Extensions
{
    public static class EntityExtensions
    {
        public static T? SerializeJson<T>(this T itemToCopy) where T : IEntity
        {
            var json = JsonSerializer.Serialize<T>(itemToCopy);
            return JsonSerializer.Deserialize<T>(json);
        }

        public static T? DeserializeJson<T>(this T itemToCopy) where T : IEntity
        {
            var json = JsonSerializer.Serialize<T>(itemToCopy);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
