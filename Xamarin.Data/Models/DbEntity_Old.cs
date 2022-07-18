namespace Xamarin.Data.Models;

[Obsolete]
public abstract class DbEntity_Old
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
}
    
public abstract class DbEntity
{
    [PrimaryKey, AutoIncrement]
    public int DbId { get; set; }
}