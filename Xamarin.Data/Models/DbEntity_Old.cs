namespace Xamarin.Data.Models;

[Obsolete]
public abstract class DbEntity_Old
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Ignore]
    public int DbId => Id;
}

public abstract class DbEntity
{
    [PrimaryKey, AutoIncrement]
    public int DbId { get; set; }
}