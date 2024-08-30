using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CorsoASP.Core.Views;

public class WalksView
{
    [Required]
    public int ID { get; set; }
    [Required] 
    public string Description { get; set; }

    [Required]
    public double LengthKm { get; set; }
    
    public string? Image { get; set; }

    [Required, ForeignKey("Regions")] 
    public int RegionFK { get; set; }
    
    [Required, ForeignKey("Difficulty")] 
    public int DifficultyFK { get; set; }
}