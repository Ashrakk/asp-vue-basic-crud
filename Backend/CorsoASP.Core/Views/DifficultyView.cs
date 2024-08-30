using System.ComponentModel.DataAnnotations;

namespace CorsoASP.Core.Views;

public class DifficultyView
{
    [Required]
    public int ID { get; set; }
    [Required]
    public string Name { get; set; }
}