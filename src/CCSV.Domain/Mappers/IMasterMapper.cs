using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSV.Domain.Mappers;

public interface IMasterMapper
{
    TDestiny Map<TOrigin, TDestiny>(TOrigin origin);
}
