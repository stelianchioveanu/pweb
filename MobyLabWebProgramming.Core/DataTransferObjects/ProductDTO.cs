﻿using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.DataTransferObjects;


public class ProductDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Price { get; set; } = default!;
    public ICollection<string> Tags { get; set; } = default!;
    public UserDTO User { get; set; } = default!;
}
