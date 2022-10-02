
var query = _{{model.action.entity.name | ToCamelCase}}Repository.Queryable.Include(x => x.Laboratory).Where(x => x.Id == request.Brand.Id);
return Task.Run(async () =>
{
    //<-- START CUSTOM CODE-->
    var result = query.ToList().FirstOrDefault();
    if (result == null)
    {
        await  _{{model.action.entity.name | ToCamelCase}}Repository.AddOneAsync(request.{{model.action.entity.name}}, cancellationToken);
        return request.{{model.action.entity.name}};
    }
        
    else
    {
        {{for property in model.action.entity | GetProperties}} 
        result.{{property.name}}= request.{{model.action.entity.name}}.{{property.name}};
        {{end}}
        {{for manytoone in model.action.entity | GetManyToOneRelationShips}} 
        result.{{manytoone.name}} =  (request.{{model.action.entity.name}}.{{manytoone.name}} != null) ? await _{{manytoone.type.name | ToCamelCase}}Repository.GetByIdAsync(request.{{model.action.entity.name}}.{{manytoone.name}}.Id, cancellationToken) : null;
        {{end}}
        return result;
    }
    //<-- END CUSTOM CODE-->
}, cancellationToken);
