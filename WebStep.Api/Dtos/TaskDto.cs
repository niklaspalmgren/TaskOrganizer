using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class TaskDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}