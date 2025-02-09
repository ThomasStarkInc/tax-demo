using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Abstractions.Messaging;

using Domain.Municipalities;

namespace Application.Municipalities.CreateMunicipality;

public sealed class CreateMunicipalityCommand : ICommand<Guid>
{
    public string Name { get; set; }
}
