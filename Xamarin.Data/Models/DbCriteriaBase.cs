namespace Xamarin.Data.Models;

public record DbCriteriaBase<T>
    where T : DbEntity
{
    public int Page { get; set; }
    public int ItemsPerPage { get; set; }
    
    
}