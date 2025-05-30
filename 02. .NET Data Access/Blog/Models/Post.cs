﻿using Dapper.Contrib.Extensions;

namespace Blog.Models;

[Table("Post")]
public class Post
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

