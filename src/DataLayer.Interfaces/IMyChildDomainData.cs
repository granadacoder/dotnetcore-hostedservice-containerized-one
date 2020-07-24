using System;
using MyCompany.MyExamples.WorkerServiceExampleOne.Domain.Entities;

namespace MyCompany.MyExamples.WorkerServiceExampleOne.DomainDataLayer.Interfaces
{
    public interface IMyChildDomainData : IDataRepository<Guid, MyChildEntity>
    {
    }
}
