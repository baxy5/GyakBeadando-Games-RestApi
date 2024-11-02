using System;
using System.Collections.Generic;

namespace GyakBeadando.Models;

public partial class Game
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Developer { get; set; }

    public int? ReleaseDate { get; set; }

    public string? Description { get; set; }

    public bool? Isplayed { get; set; }
}
