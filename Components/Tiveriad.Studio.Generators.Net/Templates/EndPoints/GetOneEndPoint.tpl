//<-- START CUSTOM CODE-->
var result = await _mediator.Send(new {{model.endpoint.action.name}}Request(id), cancellationToken);
var data = _mapper.Map<{{model.endpoint.action.entity.name}}, {{model.endpoint.action.entity.name}}ReaderModel>(result);
//<-- END CUSTOM CODE-->
return Ok(data);
