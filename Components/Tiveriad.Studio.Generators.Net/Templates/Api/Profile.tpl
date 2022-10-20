using AutoMapper;
{{for dependency in model.dependencies}} 
using {{dependency}};
{{end}}

namespace {{model.itemnamespace}};

public class {{model.entity.name}}Profile : Profile
{
    public {{model.entity.name}}Profile ()
    {
        {{for mapping in model.mappings}} 
        CreateMap<{{mapping.from.name}}, {{mapping.to.name}}>();
        {{end}}
    }
}