using Tiveriad.Pipelines;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Generators.Builders;

namespace Tiveriad.Studio.Generators.Net.Transformers;

public record ContractBuilderRequest(XContract Contract) : IRequest<ClassCodeBuilder>;