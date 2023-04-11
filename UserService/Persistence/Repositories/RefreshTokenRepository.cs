﻿using Microsoft.EntityFrameworkCore;
using UserService.Domain.Models;
using UserService.Domain.Repositories;
using UserService.Persistence.Contexts;

namespace UserService.Persistence.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _dbContext;

    public RefreshTokenRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddAsync(RefreshToken token)
    {
        await _dbContext.RefreshTokens.AddAsync(token);
    }

    public async Task<RefreshToken?> FindByTokenAsync(string token)
    {
        return await _dbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
    }

    public async Task InvalidateUserTokens(string userId)
    {
        var tokens = await _dbContext.RefreshTokens.Where(rt => rt.UserId == userId).ToListAsync();

        foreach (var t in tokens)
        {
            t.Invalidated = true;
            _dbContext.RefreshTokens.Update(t);
        }
    }

    public void Update(RefreshToken token)
    {
        _dbContext.RefreshTokens.Update(token);
    }
}