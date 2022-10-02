//<-- START CUSTOM CODE-->
var query = _{{model.action.entity.name | ToCamelCase}}Repository.Queryable
{{for manytoone in model.action.entity | GetManyToOneRelationShips}} 
.Include(x => x.{{manytoone.name}})
{{end}};
return Task.Run(() => query.ToList().AsEnumerable(), cancellationToken);
//<-- END CUSTOM CODE-->