namespace Xamarin.Data.Models;

[Obsolete]
public abstract class DbEntity_Old : ISqliteEntity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Ignore]
    public int DbId => Id;
}

public abstract class DbEntity : ISqliteEntity
{
    [PrimaryKey, AutoIncrement]
    public int DbId { get; set; }
}

public interface ISqliteEntity
{
    public int DbId { get; }
}