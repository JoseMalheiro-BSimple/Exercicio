﻿using Domain.Interfaces;
using Domain.IRepository;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepositoryEF : GenericRepository<IUser, IUserVisitor>, IUserRepository
{
    private readonly IMapper<IUser,IUserVisitor> _mapper;
    public UserRepositoryEF(AbsanteeContext context, IMapper<IUser,IUserVisitor> mapper) : base(context,mapper)
    {
        _mapper = mapper;
    }

    public async Task<IEnumerable<IUser>> GetByNamesAsync(string names)
    {
        if (string.IsNullOrWhiteSpace(names))
            return new List<IUser>();

        var usersDM = await this._context.Set<UserDataModel>()
                    .Where(u => u.Names.Contains(names, StringComparison.OrdinalIgnoreCase)).ToListAsync();

        var users = _mapper.ToDomain(usersDM);

        return users;
    }

    public async Task<IEnumerable<IUser>> GetBySurnamesAsync(string surnames)
    {
        if (string.IsNullOrWhiteSpace(surnames))
            return new List<IUser>();

        var usersDM = await this._context.Set<UserDataModel>()
                    .Where(u => u.Surnames.Contains(surnames, StringComparison.OrdinalIgnoreCase)).ToListAsync();

        var users = _mapper.ToDomain(usersDM);

        return users;
    }

    public async Task<IEnumerable<IUser>> GetByNamesAndSurnamesAsync(string names, string surnames)
    {
        if (string.IsNullOrWhiteSpace(names) && string.IsNullOrWhiteSpace(surnames))
            return new List<IUser>();

        var usersDM = await this._context.Set<UserDataModel>()
                    .Where(u => u.Names.Contains(names, StringComparison.OrdinalIgnoreCase)
                             && u.Surnames.Contains(surnames, StringComparison.OrdinalIgnoreCase)).ToListAsync();

        var users = _mapper.ToDomain(usersDM);

        return users;
    }

    public async Task<IUser?> GetByEmailAsync(string email)
    {
        var userDM = await _context.Set<UserDataModel>().FirstOrDefaultAsync(c => c.Email == email);

        if (userDM == null)
        {
            return null;
        }

        var user = _mapper.ToDomain(userDM);
        return user;
    }

    public override IUser? GetById(long id)
    {
        var userDM = _context.Set<UserDataModel>().FirstOrDefault(c => c.Id == id);

        if (userDM == null)
            return null;

        var user = _mapper.ToDomain(userDM);
        return user;
    }

    public override async Task<IUser?> GetByIdAsync(long id)
    {
        var userDM = await _context.Set<UserDataModel>().FirstOrDefaultAsync(c => c.Id == id);

        if (userDM == null)
            return null;

        var user = _mapper.ToDomain(userDM);
        return user;
    }
}
