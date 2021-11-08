using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.Dtos;
using Catalog.Entitites;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{

    [ApiController]
    [Route("items")]
    public class ItemsController: ControllerBase
    {
        private readonly IItemsRepository repository;

        public ItemsController( IItemsRepository repository){
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<ItemDto> GetItems(){
            var items = repository.GetItems().Select(items => items.AsDto());
            return items;

        }

        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id){
            var item = repository.GetItem(id);
            if(item is null){
                return NotFound();
            }
            return item.AsDto();
            
        }

        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto){
            Item item = new(){
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow 
            };
            repository.CreateItem(item);
            return CreatedAtAction(nameof(GetItem), new {id = item.Id}, item.AsDto());
        }


        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id , UpdateItemDto itemDto)
        {
            var existingItem = repository.GetItem(id);
            if(existingItem is null){
                return NotFound();
            }

            Item updateItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };
            repository.UpdateItem(updateItem);
            return NoContent();

        }


        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id){
            var existingITem = repository.GetItem(id);

            if(existingITem is null){
                return NotFound();
            }
            repository.DeleteItem(id);
            return NoContent();
        }

    
    }
}