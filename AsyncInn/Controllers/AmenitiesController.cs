﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsyncInn.Data;
using AsyncInn.Models;
using AsyncInn.Interfaces;
using AsyncInn.Models.DTOs;

namespace AsyncInn.Controllers
{
    [Route("api/Amenities")]
    [ApiController]
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenities _context;

        public AmenitiesController(IAmenities context)
        {
            _context = context;
        }

        // GET: api/Amenities

        [HttpGet ]
        public async Task<ActionResult<IEnumerable<AmenityDTO>>> GetAmenities()
        {
          var Ameninites=await _context.GetAmenities();
            return Ok(Ameninites);
        }

        // GET: api/Amenities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AmenityDTO>> GetAmenities(int id)
        {
            var amenity = await _context.GetAmenities(id);
            return Ok(amenity);
        }

        // PUT: api/Amenities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmenities(int id, AmenityDTO amenities)
        {
            return  Ok( _context.PutAmenities(id, amenities));    
        
        }

        // POST: api/Amenities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AmenityDTO>> PostAmenities(AmenityDTO amenities)
        {
            return Ok(_context.PostAmenities(amenities));
        }

        // DELETE: api/Amenities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenities(int id)
        {
            return Ok(_context.DeleteAmenities(id));
        }

        private bool AmenitiesExists(int id)
        {
            return  _context.AmenitiesExists(id);
        }
    }
}
