﻿namespace PhotosiAddressBooks.Repository;

public interface IGenericRepository<TDbEntity>
{
    Task<List<TDbEntity>> GetAsync();
}