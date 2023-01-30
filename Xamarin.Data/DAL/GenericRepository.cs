using Xamarin.Data.Models;

namespace Xamarin.Data.DAL;

public sealed class GenericRepositoryOld<T> : DbRepositoryBaseOld<T>
    where T : DbEntity_Old, new()
{
    public GenericRepositoryOld(BaseDbContext db)
        : base(db)
    { }
}

public sealed class GenericRepository<T> : DbRepositoryBase<T>
    where T : DbEntity, new()
{
    public GenericRepository(BaseDbContext db)
        : base(db)
    { }
}