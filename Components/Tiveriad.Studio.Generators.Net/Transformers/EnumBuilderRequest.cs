using Tiveriad.Pipelines;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Generators.Builders;

namespace Tiveriad.Studio.Generators.Net.Transformers;

public record EnumBuilderRequest(XEnum Enum) : IRequest<EnumCodeBuilder>;