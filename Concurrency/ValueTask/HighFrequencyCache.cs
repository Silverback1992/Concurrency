using System.Diagnostics;

namespace Concurrency.ValueTask;

public class HighFrequencyCache
{
    private readonly Dictionary<int, string> _memoryCache = [];

    // We use ValueTask because we expect to ALMOST NEVER hit the 'await'
    public async ValueTask<string> GetUserNameAsync(int userId)
    {
        // THE FAST PATH
        // If the user is in the cache, we return immediately.
        // Because this is a ValueTask, the result is packed into a struct on the Stack.
        // Result: 0 bytes of Garbage Collection memory allocated.
        if (_memoryCache.TryGetValue(userId, out string? cachedName) && cachedName is not null)
        {
            Debug.Print("Cache Hit! Executed synchronously.");
            return cachedName;
        }

        // THE SLOW PATH
        // We only generate a Task on the Heap if we actually miss the cache.
        Debug.Print("Cache Miss! Going to the database...");
        string dbName = await FetchFromDatabaseAsync(userId);

        _memoryCache[userId] = dbName;
        return dbName;
    }

    private static async Task<string> FetchFromDatabaseAsync(int userId)
    {
        // Simulating physical I/O to a SQL database
        await Task.Delay(500).ConfigureAwait(false);
        return $"User_{userId}";
    }
}
