using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;

using Ksu.Gdc.Api.Core.Configurations;
using Ksu.Gdc.Api.Core.Exceptions;
using Ksu.Gdc.Api.Core.Contracts;
using Ksu.Gdc.Api.Data.Entities;
using Ksu.Gdc.Api.Core.Models;

namespace Ksu.Gdc.Api.Web.Controllers
{
    [Route("[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;

        public GroupsController(IGroupService groupService, IUserService userService)
        {
            _groupService = groupService;
            _userService = userService;
        }

        [HttpPost]
        [Route("", Name = "CreateGroup")]
        public async Task<IActionResult> CreateGroup([FromBody] CreateDto_Group newGroup)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var dbGroup = await _groupService.CreateGroupAsync(newGroup);
                return StatusCode(StatusCodes.Status201Created, Mapper.Map<Dto_Group>(dbGroup));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("", Name = "GetGroups")]
        public async Task<IActionResult> GetGroups([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var dbGroups = await _groupService.GetGroupsAsync();
                var dtoGroups = Mapper.Map<List<Dto_Group>>(dbGroups);
                if (PaginatedList.IsValid(pageNumber, pageSize))
                {
                    PaginatedList paginatedGroups = new PaginatedList<Dto_Group>(dtoGroups, pageNumber, pageSize);
                    return Ok(paginatedGroups);
                }
                return Ok(dtoGroups);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("{groupId}", Name = "GetGroupById")]
        public async Task<IActionResult> GetGroupById([FromRoute] int groupId)
        {
            try
            {
                var group = await _groupService.GetGroupByIdAsync(groupId);
                return Ok(Mapper.Map<Dto_Group>(group));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("{groupId}/users", Name = "GetGroupMembers")]
        public async Task<IActionResult> GetGroupMembers([FromRoute] int groupId, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var dbUsers = await _groupService.GetMembersOfGroupAsync(groupId);
                var dtoUsers = Mapper.Map<List<Dto_User>>(dbUsers);
                if (PaginatedList.IsValid(pageNumber, pageSize))
                {
                    PaginatedList paginatedUsers = new PaginatedList<Dto_User>(dtoUsers, pageNumber, pageSize);
                    return Ok(paginatedUsers);
                }
                return Ok(dtoUsers);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("{groupId}/portfolio/games", Name = "GetGamesOfGroup")]
        public async Task<IActionResult> GetGamesOfGroup([FromRoute] int groupId, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var dbGames = await _groupService.GetGamesOfGroupAsync(groupId);
                var dtoGames = Mapper.Map<List<Dto_Game>>(dbGames);
                if (PaginatedList.IsValid(pageNumber, pageSize))
                {
                    PaginatedList paginatedGames = new PaginatedList<Dto_Game>(dtoGames, pageNumber, pageSize);
                    return Ok(paginatedGames);
                }
                return Ok(dtoGames);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Route("{groupId}", Name = "UpdateGroup")]
        public async Task<IActionResult> UpdateGroup([FromRoute] int groupId, [FromBody] UpdateDto_Group updateGroup)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var dbGroup = await _groupService.GetGroupByIdAsync(groupId);
                await _groupService.UpdateGroupAsync(dbGroup, updateGroup);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Route("{groupId}/users/{userId}", Name = "AddMemberToGroup")]
        public async Task<IActionResult> AddMemberToGroup([FromRoute] int groupId, [FromRoute] int userId)
        {
            try
            {
                var group = await _groupService.GetGroupByIdAsync(groupId);
                var user = await _userService.GetUserByIdAsync(userId);
                await _groupService.AddMemberToGroupAsync(group.GroupId, user.UserId);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch]
        [Route("{groupId}", Name = "PatchGroup")]
        public async Task<IActionResult> PatchGroup([FromRoute] int groupId, [FromBody] JsonPatchDocument<UpdateDto_Group> patchGroup)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var dbGroup = await _groupService.GetGroupByIdAsync(groupId);
                var updateGroup = Mapper.Map<UpdateDto_Group>(dbGroup);
                patchGroup.ApplyTo(updateGroup, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                await _groupService.UpdateGroupAsync(dbGroup, updateGroup);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("{groupId}", Name = "DeleteGroupById")]
        public async Task<IActionResult> DeleteGroupById([FromRoute] int groupId)
        {
            try
            {
                await _groupService.DeleteGroupByIdAsync(groupId);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("{groupId}/users/{userId}")]
        public async Task<IActionResult> RemoveMemberFromGroup([FromRoute] int groupId, [FromRoute] int userId)
        {
            try
            {
                var group = await _groupService.GetGroupByIdAsync(groupId);
                var user = await _userService.GetUserByIdAsync(userId);
                await _groupService.RemoveMemberFromGroupAsync(group.GroupId, user.UserId);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
