using Catalog.Dtos;
using Catalog.Entitites;

namespace Catalog
{
public static class Extensiones{

    public static ItemDto AsDto(this Item item){

        return new ItemDto{
            Id = item.Id,
            Name = item.Name,
            Price = item.Price, 
        };
    } 

}
}