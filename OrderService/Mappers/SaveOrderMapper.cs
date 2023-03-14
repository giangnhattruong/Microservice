using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Mappers;

public class SaveOrderMapper : ISaveOrderMapper
{
    private readonly ISaveOrderDetailMapper _saveOrderDetailMapper;

    public SaveOrderMapper(ISaveOrderDetailMapper saveOrderDetailMapper)
    {
        _saveOrderDetailMapper = saveOrderDetailMapper;
    }
    
    public Order ToModel(SaveOrderDto dto)
    {
        var model = new Order();
        model.UserId = dto.UserId;
        model.OrderDetails = _saveOrderDetailMapper.ToListModels(dto.Products);

        return model;
    }
}