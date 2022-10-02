//<-- START CUSTOM CODE-->
var query = _{{model.action.entity.name | ToCamelCase}}Repository.Queryable.Include(x => x.Laboratory);

if (string.IsNullOrEmpty(request.Query))
    return Task.Run(() => query.ToList().AsEnumerable());

IParser parser = new QueryParser();
//<-- END CUSTOM CODE-->
return Task.Run(() => query.Where(request.Query, parser).ToList().AsEnumerable());

