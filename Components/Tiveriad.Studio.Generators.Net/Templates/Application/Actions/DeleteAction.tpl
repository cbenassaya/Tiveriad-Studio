//<-- START CUSTOM CODE-->
return Task.Run(() => _{{model.action.entity.name | ToCamelCase}}Repository.DeleteMany(x=>x.Id == request.Id) == 1, cancellationToken);
//<-- END CUSTOM CODE-->