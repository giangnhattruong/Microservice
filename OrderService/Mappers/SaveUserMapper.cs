﻿using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Mappers;

public class SaveUserMapper : ISaveUserMapper
{
    public User ToModel(SaveUserDto dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto));
        }

        var model = new User();
        model.Name = dto.Name;

        return model;
    }
}