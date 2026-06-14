namespace Hornet.Domain.DTOs.SpaceX;
public class RocketDto
{
        public int Id {get;set;}
        public required string Name { get; set; }
        public string? Variant { get; set; }
}
