﻿namespace UserService.Resources;

public class AuthTokenResource
{
    public string Token { get; set; }
    
    public string RefreshToken { get; set; }
    
    public DateTime ExpiredAt { get; set; }
}