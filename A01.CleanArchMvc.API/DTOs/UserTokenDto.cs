﻿namespace A01.CleanArchMvc.API.DTOs;

public class UserTokenDto
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}