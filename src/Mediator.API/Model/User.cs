﻿using System.Text.Json.Serialization;

namespace Mediator.API.Model;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    
    public string Password { get; set; }
    [JsonIgnore] public ICollection<Link> Links { get; set; }
}