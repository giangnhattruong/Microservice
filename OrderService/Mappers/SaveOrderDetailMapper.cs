using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Mappers;

public class SaveOrderDetailMapper : IDtoToModelMapper<SaveOrderDetailDto, OrderDetail>
{
    public OrderDetail ToModel(SaveOrderDetailDto dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto));
        }

        return new OrderDetail() {ProductId = dto.ProductId, Quantity = dto.Quantity};
    }
    
    public ICollection<OrderDetail> ToListModels(ICollection<SaveOrderDetailDto> dtos)
    {
        return dtos.Select(dto => ToModel(dto)).ToList();
    }
}