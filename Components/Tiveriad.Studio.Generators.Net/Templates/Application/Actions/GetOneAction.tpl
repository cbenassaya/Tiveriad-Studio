//<-- START CUSTOM CODE-->
var query = _{{model.action.entity.name | ToCamelCase}}Repository.Queryable
{{for manytoone in model.action.entity | GetManyToOneRelationShips}} 
.Include(x => x.{{manytoone.name}})
{{end}}
.Where(x => x.Id == request.Id);
//<-- END CUSTOM CODE-->
return Task.Run(() => query.ToList().FirstOrDefault(), cancellationToken);
