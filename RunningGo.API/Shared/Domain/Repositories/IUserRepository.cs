﻿using RunningGo.API.Shared.Domain.Models;

namespace RunningGo.API.Shared.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> List();
    Task Add(User user);
    Task<User> FindById(long id);
    Task<User> FindByFullName(string name, string lastName);
    void Update(User user);
    void Remove(User user);
}