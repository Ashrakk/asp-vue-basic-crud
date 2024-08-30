using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CorsoASP.Core.Models;

[Table("Walks")]
public class Walks: Base<int>
{
    [Required] 
    public string Description { get; set; }
    
    [Required]
    public double LengthKm { get; set; }
    
    public string? Image { get; set; }

    [Required, ForeignKey("Regions")] 
    public int RegionFK { get; set; }
    
    [Required, ForeignKey("Difficulty")] 
    public int DifficultyFK { get; set; }
    
    public virtual Regions Regions { get; set; }
    public virtual Difficulty Difficulty { get; set; }

}