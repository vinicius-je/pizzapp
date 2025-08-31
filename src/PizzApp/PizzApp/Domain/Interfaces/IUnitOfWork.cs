﻿namespace PizzApp.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit(CancellationToken cancellationToken);
    }
}
