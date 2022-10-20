//<-- START CUSTOM CODE-->
var result = await _mediator.Send(new {{model.endpoint.action.name}}Request((string)query), cancellationToken);
var data = _mapper.Map<IEnumerable<Brand>, IEnumerable<BrandReaderModel>>(result);
return Ok(data.AsQueryable().Paginate(query.CurrentPage, query.PageSize));
//<-- END CUSTOM CODE-->