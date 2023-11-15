using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    
    public class DriverController : BaseApiController
    {
        private IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        
        public DriverController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<DriverDto>>> Get()
        {
            var drivers = await _unitOfWork.Drivers.GetAllAsync();
            return _mapper.Map<List<DriverDto>>(drivers);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DriverDto>> Get(int id)
        {
            var driver = await _unitOfWork.Drivers.GetByIdAsync(id);
            return _mapper.Map<DriverDto>(driver);
        }
        
        [HttpPost("addteam")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddTeam(AddTeamDto model)
        {
            var driver = await _unitOfWork.Drivers
                        .GetDriverNameAsync(model.DriverName);
            
            if (driver == null)
            {
                return BadRequest($"Driver {model.DriverName} does not exists");
            }
            var teams = await _unitOfWork.Teams
                        .GetTeamAsync(model.Team);
            if (teams != null)
                {
                    var driverHashTeam = driver.Teams.Any(e=>e.Id == teams.Id);

                    if (!driverHashTeam)
                    {
                        driver.Teams.Add(teams);
                        _unitOfWork.Drivers.Update(driver);
                        await _unitOfWork.SaveAsync();

                        return Ok($"Team {model.Team} added to driver {model.DriverName} successfully");
                    }
                    else{
                        return Ok($"Driver {model.DriverName} already has Team {model.Team}");
                    }
                }
            return BadRequest($"Team {model.Team} was not found");
           /* return null; */
        }
        
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Driver>> Post(DriverDto DriverDto)
        {
            var driver = _mapper.Map<Driver>(DriverDto);
            _unitOfWork.Drivers.Add(driver);
            await _unitOfWork.SaveAsync();
        
            if (driver == null)
            {
                return BadRequest();
            }
            driver.Id = driver.Id;
            return CreatedAtAction(nameof(Post), new { id = driver.Id }, driver);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DriverDto>> Put(int id, [FromBody]DriverDto DriverDto)
        {
            if (DriverDto == null)
            {
                return NotFound();
            }
            var driver = _mapper.Map<Driver>(DriverDto);
            _unitOfWork.Drivers.Update(driver);
            await _unitOfWork.SaveAsync();
            return DriverDto;
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DriverDto>> Delete(int id)
        {
            var driver = await _unitOfWork.Drivers.GetByIdAsync(id);
            if (driver == null)
            {
                return NotFound();
            }
            _unitOfWork.Drivers.Remove(driver);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}