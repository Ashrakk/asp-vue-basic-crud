using System.ComponentModel.DataAnnotations;

namespace CorsoASP.Core.Views;

public class RegionsView
{
    [Required]
    public int ID { get; set; }
    [Required]
    public string Code { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Image { get; set; }
}