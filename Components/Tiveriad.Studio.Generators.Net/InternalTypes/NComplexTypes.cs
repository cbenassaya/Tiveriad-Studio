using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Net.InternalTypes;

public static class NComplexTypes
{
    public static InternalType IENUMERABLE =>
        Code.CreateInternalType("IEnumerable", "System.Collections.Generic").Build();
    public static InternalType IENTITY =>
        Code.CreateInternalType("IEntity", "Tiveriad.Repositories").Build();
    public static InternalType IREPOSITORY =>
        Code.CreateInternalType("IRepository", "Tiveriad.Repositories").Build();
    public static InternalType IAUDITABLE =>
        Code.CreateInternalType("IAuditable", "Tiveriad.Repositories").Build();
    public static InternalType MAXLENGTHATTRIBUTE =>
        Code.CreateInternalType("MaxLength", "System.ComponentModel.DataAnnotations").Build();
    public static InternalType MINLENGTHATTRIBUTE =>
        Code.CreateInternalType("MinLength", "System.ComponentModel.DataAnnotations").Build();
    public static InternalType REQUIREDATTRIBUTE =>
        Code.CreateInternalType("Required", "System.ComponentModel.DataAnnotations").Build();
    public static InternalType ISUNIQUEATTRIBUTE =>
        Code.CreateInternalType("IsUnique", "System.ComponentModel.DataAnnotations").Build();
    public static InternalType IREQUEST =>
        Code.CreateInternalType("IRequest", "MediatR").Build();
    public static InternalType IREQUESTHANDLER =>
        Code.CreateInternalType("IRequestHandler", "MediatR").Build();
    public static InternalType IMEDIATOR =>
        Code.CreateInternalType("IMediator", "MediatR").Build();
    public static InternalType CANCELLATIONTOKEN =>
        Code.CreateInternalType("CancellationToken", "System.Threading").Build();
    public static InternalType TASK =>
        Code.CreateInternalType("Task", "System.Threading.Tasks").Build();
    public static InternalType CONTROLLERBASE =>
        Code.CreateInternalType("ControllerBase", "Microsoft.AspNetCore.Mvc").Build();
    public static InternalType ACTIONRESULT =>
        Code.CreateInternalType("ActionResult", "Microsoft.AspNetCore.Mvc").Build();
    public static InternalType FROMROUTE =>
        Code.CreateInternalType("FromRoute", "Microsoft.AspNetCore.Mvc").Build();
    public static InternalType FROMBODY =>
        Code.CreateInternalType("FromBody", "Microsoft.AspNetCore.Mvc").Build();
    public static InternalType HTTPGET =>
        Code.CreateInternalType("HttpGet", "Microsoft.AspNetCore.Mvc").Build();
    public static InternalType HTTPPOST =>
        Code.CreateInternalType("HttpPost", "Microsoft.AspNetCore.Mvc").Build();
    public static InternalType HTTPDELETE =>
        Code.CreateInternalType("HttpDelete", "Microsoft.AspNetCore.Mvc").Build();
    public static InternalType IENTITYTYPECONFIGURATION =>
        Code.CreateInternalType("IEntityTypeConfiguration", "Microsoft.EntityFrameworkCore").Build();
    public static InternalType ENTITYTYPEBUILDER =>
        Code.CreateInternalType("EntityTypeBuilder", "Microsoft.EntityFrameworkCore.Metadata.Builders").Build();
    public static InternalType IMAPPER =>
        Code.CreateInternalType("IMapper", "AutoMapper").Build();
    public static InternalType TRESPONSE =>
        Code.CreateInternalType("TResponse", "Tiveriad.Apis.Contracts").Build();
    public static InternalType ABSTRACTVALIDATOR =>
        Code.CreateInternalType("AbstractValidator", "FluentValidation").Build();
    public static List<InternalType> Types => new()
    {
        IENUMERABLE, IENTITY, IAUDITABLE, MAXLENGTHATTRIBUTE, REQUIREDATTRIBUTE,
        ISUNIQUEATTRIBUTE, IREQUEST, IREQUESTHANDLER, IREPOSITORY, CANCELLATIONTOKEN,
        CONTROLLERBASE, IMAPPER, IMEDIATOR, TASK, FROMROUTE, FROMBODY, HTTPGET, HTTPPOST, HTTPDELETE,
        IENTITYTYPECONFIGURATION
    };

    public static InternalType Get(string name)
    {
        return Types.First(x => x.Name.Contains(name));
    }
}