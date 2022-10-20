//<-- START CUSTOM CODE-->
var result = await _mediator.Send(new {{model.endpoint.action.name}}Request(), cancellationToken);
var data = _mapper.Map<IEnumerable<{{model.endpoint.action.entity.name}}>, IEnumerable<{{model.endpoint.action.entity.name}}ReaderModel>>(result);
//<-- END CUSTOM CODE-->
return Ok(data);
