using System;
using bootcamp_store_backend.Application.Dtos;
using bootcamp_store_backend.Application.Services;
using bootcamp_store_backend.Domain.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace bootcamp_store_backend.Infraestructure.Rest
{
	public class GenericCrudController<D>:ControllerBase
		where D : class
	{
        protected readonly IItemService<D> _service;

        public GenericCrudController(IItemService<D> service)
		{
			_service = service;
        }

        [HttpGet]
        [Produces("application/json")]
        public ActionResult<IEnumerable<D>> Get()
        {
            var dto = _service.GetAll();
            return Ok(dto);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public ActionResult<D> Get(long id)
        {
            try
            {
                D dto = _service.Get(id);
                return Ok(dto);
            }
            catch (ElementNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public ActionResult<D> Insert(D dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            dto = _service.Insert(dto);
            return CreatedAtAction(nameof(Get), new { id = ((IDto)dto).Id }, dto);
        }

        [HttpPut]
        [Produces("application/json")]
        [Consumes("application/json")]
        public ActionResult<D> Update(D dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            dto = _service.Update(dto);
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            try
            {
                _service.Delete(id);
                return NoContent();
            }
            catch (ElementNotFoundException)
            {
                return NotFound();
            }
        }
    }
}

