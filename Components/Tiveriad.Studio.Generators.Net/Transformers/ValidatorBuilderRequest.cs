using Tiveriad.Pipelines;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Net.Transformers;


public record ValidatorBuilderRequest(XAction Action, Record Request) : IRequest<ClassCodeBuilder>;