using Tiveriad.Commons.Extensions;
using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Extensions;
using Tiveriad.Studio.Core.Processors;
using Tiveriad.Studio.Core.Services;

namespace Tiveriad.Studio.Application.Middlewares;

public class EndpointMiddleware : Commons.Reflexions.AbstractProcessor<XElementBase, XNamedElement>,
    IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>, IProcessor
{
    private readonly IDictionary<XBehaviourType, Builder> _builders;
    private readonly IXTypeService _typeService;

    public EndpointMiddleware(IXTypeService typeService)
    {
        _typeService = typeService;
        _builders =
            new Dictionary<XBehaviourType, Builder>
            {
                { XBehaviourType.Delete, BuildDeleteEndPoint },
                { XBehaviourType.GetMany, BuildGetAllEndPoint },
                { XBehaviourType.GetOne, BuildGetByIdEndPoint },
                { XBehaviourType.SaveOrUpdate, BuildSaveOrUpdateEndPoint }
            };
    }

    public void Run(PipelineContext context, PipelineModel model)
    {
        Traverse(model.Project);
    }

    protected override bool ApplyIf(XElementBase value)
    {
        return value is XAction { Entity.IsBusinessEntity: true };
    }

    protected override void DoApply(XElementBase value)
    {
        var action = value as XAction;
        var project = action?.GetProject();
        if (project == null) return;
        var module = action?.GetModule();
        if (module == null) return;

        var endPointsPackage = module.Packages.FirstOrDefault(x => x.Name == "EndPoints");
        if (endPointsPackage == null)
        {
            endPointsPackage = new XPackage
            {
                Name = "EndPoints",
                Module = module
            };
            module.Packages.Add(endPointsPackage);
        }

        var endPointPackage = module.Packages.FirstOrDefault(x => x.Name == $"{action.Entity.Name}EndPoints");
        if (endPointPackage == null)
        {
            endPointPackage = new XPackage
            {
                Name = $"{action.Entity.Name}EndPoints",
                Parent = endPointsPackage
            };
            endPointsPackage.Add(endPointPackage);
        }

        var contractsEndPoint = module.Packages.FirstOrDefault(x => x.Name == "Contracts");
        if (contractsEndPoint == null)
        {
            contractsEndPoint = new XPackage
            {
                Name = "Contracts",
                Module = module
            };
            module.Packages.Add(contractsEndPoint);
        }


        var contracts = new List<XContract>();
        contracts.AddRange(CreateReaderContract(action.Entity));
        contracts.AddRange(CreateWriterContract(action.Entity));

        contracts.ForEach(x =>
        {
            x.Package = contractsEndPoint;
            x.Namespace = contractsEndPoint.GetNamespace();

            if (!_typeService.Exists(x))
            {
                contractsEndPoint.Add(x);
                _typeService.Add(x);
            }
        });


        var endPoint = _builders[action.BehaviourType](action);

        endPoint.Package = endPointPackage;
        endPoint.Namespace = endPointPackage.GetNamespace();
        endPointPackage.Add(endPoint);

        _typeService.Add(endPoint);
    }

    private XEndPoint BuildGetByIdEndPoint(XAction action)
    {
        var route = "/api";

        route += $"/{action.Entity.PluralName.ToCamelCase()}";

        action.Entity.Properties.Where(x => x is XId).ToList()
            .ForEach(x => { route += $"/{{{x.Name.ToCamelCase()}}}"; });

        var endPoint = new XEndPoint
        {
            Name = "GetByIdEndPoint",
            HttpMethod = XHttpMethod.HttpGet,
            Authorize = true,
            Route = route,
            ActionReference = action.ToString(),
            Action = action
        };

        endPoint.Parameters = action.Parameters.Where(x => x.Type is XDataType).Select(x => new XParameter
        {
            Name = x.Name,
            Type = x.Type,
            Classifier = endPoint,
            TypeReference = x.TypeReference,
            Source = "FromRoute"
        }).ToList();

        endPoint.Response = new XResponse
        {
            Classifier = endPoint,
            IsCollection = false,
            Type = _typeService.Get($"{action.Entity.Name}ReaderModel")
        };

        endPoint.Mappings.Add(new XMapping
        {
            From = action.Entity, To = endPoint.Response.Type
        });

        return endPoint;
    }

    private XEndPoint BuildGetAllEndPoint(XAction action)
    {
        var route = "/api";

        //var dependenciesFor = _context.GetDependencyGraph().DependenciesFor(action.Entity, 1);

        /*
        foreach (var id in dependenciesFor.SelectMany(x => x.Properties).Where(x => x is XId).ToList())
            route += $"/{id.Classifier.PluralName.ToCamelCase()}/{{{id.Classifier.Name.ToCamelCase()}{id.Name}}}";
            */

        route += $"/{action.Entity.PluralName.ToCamelCase()}";

        var endPoint = new XEndPoint
        {
            Name = "GetAllEndPoint",
            HttpMethod = XHttpMethod.HttpGet,
            Authorize = true,
            Route = route,
            ActionReference = action.ToString(),
            Action = action
        };
        endPoint.Parameters = action.Parameters.Where(x => x.Type is XDataType).Select(x => new XParameter
        {
            Name = x.Name,
            Type = x.Type,
            Classifier = endPoint,
            TypeReference = x.TypeReference,
            Source = "FromRoute"
        }).ToList();

        endPoint.Response = new XResponse
        {
            Classifier = endPoint,
            IsCollection = true,
            Type = _typeService.Get($"{action.Entity.Name}ReaderModel")
        };

        endPoint.Mappings.Add(new XMapping
        {
            From = action.Entity, To = endPoint.Response.Type
        });

        return endPoint;
    }

    private XEndPoint BuildSaveOrUpdateEndPoint(XAction action)
    {
        var route = "/api";
        route += $"/{action.Entity.PluralName.ToCamelCase()}";

        var endPoint = new XEndPoint
        {
            Name = "SaveOrUpdateEndPoint",
            Parameters = action.Parameters,
            HttpMethod = XHttpMethod.HttpPost,
            Authorize = true,
            Route = route,
            ActionReference = action.ToString(),
            Action = action
        };
        endPoint.Parameters = action.Parameters.Where(x => x.Type is XDataType).Select(x => new XParameter
        {
            Name = x.Name,
            Type = x.Type,
            Classifier = endPoint,
            TypeReference = x.TypeReference,
            Source = "FromRoute"
        }).ToList();

        var model = _typeService.Get($"{action.Entity.Name}WriterModel");
        endPoint.Parameters.Add(new XParameter
        {
            Name = "model",
            Type = model,
            Classifier = endPoint,
            TypeReference = model.ToString(),
            Source = "FromBody"
        });

        endPoint.Response = new XResponse
        {
            Classifier = endPoint,
            IsCollection = false,
            Type = _typeService.Get($"{action.Entity.Name}ReaderModel")
        };

        endPoint.Mappings.Add(new XMapping
        {
            From = model, To = action.Entity
        });

        endPoint.Mappings.Add(new XMapping
        {
            From = action.Entity, To = endPoint.Response.Type
        });


        return endPoint;
    }

    private XEndPoint BuildDeleteEndPoint(XAction action)
    {
        var route = "/api";


        route += $"/{action.Entity.PluralName.ToCamelCase()}";

        action.Entity.Properties.Where(x => x is XId).ToList()
            .ForEach(x => { route += $"/{{{x.Name.ToCamelCase()}}}"; });

        var endPoint = new XEndPoint
        {
            Name = "DeleteEndPoint",
            Response = action.Response,
            HttpMethod = XHttpMethod.HttpDelete,
            Authorize = true,
            Route = route,
            ActionReference = action.ToString(),
            Action = action
        };
        endPoint.Parameters = action.Parameters.Select(x => new XParameter
        {
            Name = x.Name,
            Type = x.Type,
            Classifier = endPoint,
            TypeReference = x.TypeReference,
            Source = "FromRoute"
        }).ToList();
        return endPoint;
    }

    private IList<XContract> CreateReaderContract(XEntity entity)
    {
        IList<XContract> contracts = new List<XContract>();
        var contract = new XContract
        {
            Name = $"{entity.Name}ReaderModel"
        };
        contracts.Add(contract);

        entity.GetIds().ToList().ForEach(x =>
        {
            contract.Properties.Add(new XProperty
            {
                Name = x.Name ?? "Id",
                Type = x.Type,
                Classifier = contract,
                TypeReference = x.TypeReference
            });
        });

        entity.GetProperties().ToList().ForEach(x =>
        {
            contract.Properties.Add(new XProperty
            {
                Name = x.Name,
                Type = x.Type,
                Classifier = contract,
                TypeReference = x.TypeReference
            });
        });

        entity.RelationShips.ForEach(x =>
        {
            if (x.Type is XEntity target)
            {
                var reduceContract = CreateReduceContract(target);
                contracts.Add(reduceContract);
                contract.Properties.Add(new XProperty
                {
                    Name = x.Name,
                    Type = reduceContract,
                    Classifier = contract,
                    IsCollection = x is XOneToMany or XManyToMany
                });
            }
        });

        return contracts;
    }

    private IList<XContract> CreateWriterContract(XEntity entity)
    {
        IList<XContract> contracts = new List<XContract>();
        var contract = new XContract
        {
            Name = $"{entity.Name}WriterModel"
        };
        contracts.Add(contract);

        entity.GetIds().ToList().ForEach(x =>
        {
            contract.Properties.Add(new XProperty
            {
                Name = x.Name ?? "Id",
                Type = x.Type,
                Classifier = contract,
                TypeReference = x.TypeReference
            });
        });

        entity.GetProperties().ToList().ForEach(x =>
        {
            contract.Properties.Add(new XProperty
            {
                Name = x.Name,
                Type = x.Type,
                Classifier = contract,
                TypeReference = x.TypeReference
            });
        });

        entity.RelationShips.ForEach(x =>
        {
            if (x.Type is XEntity target)
            {
                if (target.GetIds().Count == 1)
                {
                    contract.Properties.Add(new XProperty
                    {
                        Name = $"{x.Name}Id",
                        Type = target.GetIds()?.FirstOrDefault()?.Type,
                        Classifier = contract,
                        TypeReference = target.GetIds()?.FirstOrDefault()?.TypeReference,
                        IsCollection = x is XOneToMany or XManyToMany,
                        Constraints = new List<XConstraint> { new XRequiredConstraint() }
                    });
                }
                else if (target.GetIds().Count > 1)
                {
                    var idContract = CreateIdContract(target);
                    contracts.Add(idContract);

                    contract.Properties.Add(new XProperty
                    {
                        Name = x is XOneToMany or XManyToMany ? $"{target.Name}Ids" : $"{target.Name}Id",
                        Type = idContract,
                        Classifier = contract,
                        IsCollection = x is XOneToMany or XManyToMany,
                        Constraints = new List<XConstraint> { new XRequiredConstraint() }
                    });
                }
            }
        });

        return contracts;
    }

    private XContract CreateReduceContract(XEntity entity)
    {
        var contract = new XContract
        {
            Name = $"{entity.Name}ReduceModel"
        };


        entity.GetIds().ToList().ForEach(x =>
        {
            contract.Properties.Add(new XProperty
            {
                Name = x.Name ?? "Id",
                Type = x.Type,
                Classifier = contract,
                TypeReference = x.TypeReference,
                Constraints = x.Constraints
            });
        });

        entity.GetProperties().Where(x => x.Displayed).ToList().ForEach(x =>
        {
            contract.Properties.Add(new XProperty
            {
                Type = x.Type,
                Name = x.Name,
                Classifier = contract,
                TypeReference = x.TypeReference,
                Constraints = x.Constraints
            });
        });

        return contract;
    }

    private XContract CreateIdContract(XEntity entity)
    {
        var contract = new XContract
        {
            Name = $"{entity.Name}IdModel"
        };

        entity.GetIds().ToList().ForEach(x =>
        {
            contract.Properties.Add(new XProperty
            {
                Type = x.Type,
                Classifier = contract,
                TypeReference = x.TypeReference,
                Name = x.Name ?? "Id",
                Constraints = new List<XConstraint> { new XRequiredConstraint() }
            });
        });

        return contract;
    }

    private delegate XEndPoint Builder(XAction action);
}