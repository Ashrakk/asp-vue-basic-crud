using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CorsoASP.Core.Models;

[Table("Difficulty")]
public class Difficulty : Base<int>
{
    [Required]
    public string Name { get; set; }
}