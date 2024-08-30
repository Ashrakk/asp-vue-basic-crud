using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CorsoASP.Core.Models;

[Table("Regions")]
public class Regions : Base<int>
{
    [Required]
    public string Code { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public string? Image { get; set; }
}