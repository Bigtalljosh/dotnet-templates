﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyProject.Business.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyProject.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly House[] houses = new House[]
        {
            new House { Id = 123, Description = "This is a nice house!" },
            new House { Id = 456, Description = "This is a very nice house!" }
        };

        public HouseController(ILogger<HouseController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get all houses.
        /// </summary>
        /// <returns>Details of all houses.</returns>
        /// <response code="200">The houses were successfully retrieved</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<House>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<House>> List()
        {
            return houses;
        }

        /// <summary>
        /// Get a single house.
        /// </summary>
        /// <returns>The requested house.</returns>
        /// <response code="200">The house was successfully retrieved.</response>
        /// <response code="404">The house does not exist.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(House), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<House> Get(int id)
        {
            if (id is 0)
            {
                return NotFound();
            }

            var house = houses.Where(h => h.Id.Equals(id)).SingleOrDefault();

            if (house is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(house);
            }
        }

        /// <summary>
        /// Save a new house.
        /// </summary>
        /// <returns>The saved house.</returns>
        /// <response code="201">The house was successfully saved.</response>
        /// <response code="400">The house is invalid.</response>
        [HttpPost]
        [ProducesResponseType(typeof(House), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<House> Post([FromBody] House house)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("A new house has been created with id {id}", house.Id);
            return CreatedAtRoute(nameof(Get), new { id = house.Id }, house);
        }

        /// <summary>
        /// Update an existing house.
        /// </summary>
        /// <response code="204">The house was successfully updated.</response>
        /// <response code="400">The house is invalid.</response>
        /// <response code="404">The house does not exist.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] House house)
        {
            if (id is 0)
            {
                return NotFound();
            }

            _logger.LogInformation("An existing house has been updated with id {id}", house.Id);
            return NoContent();
        }

        /// <summary>
        /// Delete an existing house.
        /// </summary>
        /// <response code="204">The house was successfully deleted.</response>
        /// <response code="404">The house does not exist.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            if (id is 0)
            {
                return NotFound();
            }

            _logger.LogInformation("An existing house has been deleted with id {id}", id);
            return NoContent();
        }
    }
}
