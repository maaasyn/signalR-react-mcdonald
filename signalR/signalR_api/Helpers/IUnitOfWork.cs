using System;

using System.Collections.Generic;
using signalR_api.Models;


public interface IUnitOfWork

{

    IRepository<DummyClassDto> Customers { get; }

    IRepository<DummyClassDto> Orders { get; }

    void Commit();

}