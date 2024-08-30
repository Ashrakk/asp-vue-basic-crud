using System.ComponentModel.DataAnnotations;

namespace CorsoASP.Core.Models;

public class Base<T>
{
    [Key]
    public T ID { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}