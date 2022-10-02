//<-- START CUSTOM CODE-->
var entity = _mapper.Map<{{model.endpoint.action.entity.name}}WriterModel, {{model.endpoint.action.entity.name}}>(model);
var result = await _mediator.Send(new {{model.endpoint.action.name}}Request(entity), cancellationToken);
var data = _mapper.Map<{{model.endpoint.action.entity.name}}, {{model.endpoint.action.entity.name}}ReaderModel>(result);
//<-- END CUSTOM CODE-->
return Ok(data);
