//<-- START CUSTOM CODE-->
var result = await _mediator.Send(new {{model.endpoint.action.name}}Request(id), cancellationToken);
//<-- END CUSTOM CODE-->
return Ok(result);
