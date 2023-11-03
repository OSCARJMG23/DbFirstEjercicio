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

    public class TeamController : BaseApiController
    {
        private IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        
        public TeamController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TeamDto>>> Get()
        {
            var ciudad = await _unitOfWork.Teams.GetAllAsync();
            return _mapper.Map<List<TeamDto>>(ciudad);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TeamDto>> Get(int id)
        {
            var team = await _unitOfWork.Teams.GetByIdAsync(id);
            return _mapper.Map<TeamDto>(team);
        }
        
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Team>> Post(TeamDto TeamDto)
        {
            var team = _mapper.Map<Team>(TeamDto);
            _unitOfWork.Teams.Add(team);
            await _unitOfWork.SaveAsync();
        
            if (team == null)
            {
                return BadRequest();
            }
            team.Id = team.Id;
            return CreatedAtAction(nameof(Post), new { id = team.Id }, team);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TeamDto>> Put(int id, [FromBody]TeamDto TeamDto)
        {
            if (TeamDto == null)
            {
                return NotFound();
            }
            var team = _mapper.Map<Team>(TeamDto);
            _unitOfWork.Teams.Update(team);
            await _unitOfWork.SaveAsync();
            return TeamDto;
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TeamDto>> Delete(int id)
        {
            var team = await _unitOfWork.Teams.GetByIdAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            _unitOfWork.Teams.Remove(team);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}