using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Mappers;

public class SaveOrderMapper : IDtoToModelMapper<SaveOrderDto, Order>
{
    private readonly IDtoToModelMapper<SaveOrderDetailDto, OrderDetail> _saveOrderDetailMapper;

    public SaveOrderMapper(IDtoToModelMapper<SaveOrderDetailDto, OrderDetail> saveOrderDetailMapper)
    {
        _saveOrderDetailMapper = saveOrderDetailMapper;
    }
    
    public Order ToModel(SaveOrderDto dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto));
        }

        var model = new Order();
        model.UserId = dto.UserId;
        model.OrderDetails = _saveOrderDetailMapper.ToListModels(dto.Products);

        return model;
    }

    public ICollection<Order> ToListModels(ICollection<SaveOrderDto> dtos)
    {
        return dtos.Select(d => ToModel(d)).ToList();
    }
}