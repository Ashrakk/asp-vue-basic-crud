using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CorsoASP.Core.Models;

namespace CorsoASP.Core.Views;

public class WalksDetailView
{
    [Required]
    public int ID { get; set; }
    [Required] 
    public string Description { get; set; }

    [Required]
    public double LengthKm { get; set; }
    
    public string? Image { get; set; }

    [Required] 
    public RegionsView Regions { get; set; }
    
    [Required] 
    public DifficultyView Difficulty { get; set; }
}