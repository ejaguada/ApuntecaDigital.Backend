// GetCareersByNameRequest.cs
using FastEndpoints; // Needed if you put Request in a separate file and use RequestBodyType

public class GetCareersByNameRequest
{
    // The base route is now just /Careers
    public const string Route = "/Careers";

    // This method is for building the client-side URL
    // All parameters are now query parameters
    public static string BuildRoute(string? name = null)
    {
        var queryStringParams = new List<string>();

        // 'name' is now a mandatory query parameter for this endpoint
        // Always URL-encode values for query parameters!
        if (!string.IsNullOrEmpty(name))
        {
            queryStringParams.Add($"name={Uri.EscapeDataString(name)}");
        }

        // Combine the base route with the query string
        return Route + (queryStringParams.Any() ? "?" + string.Join("&", queryStringParams) : "");
    }

    // This will now be bound from the query string: ?name=somevalue
    // It's not nullable here because we will make it required via validation.
    public string Name { get; set; } = string.Empty;
}
