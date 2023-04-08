namespace UserService.Extensions;

public static class RequestUser
{
    public static string GetUserId(this HttpContext context)
    {
        if (context.User == null)
            return string.Empty;

        return context.User.Claims.Single(cp => cp.Type == "id").Value;
    }
}