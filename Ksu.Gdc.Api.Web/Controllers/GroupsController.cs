using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Ksu.Gdc.Api.Core.Configurations;
using Ksu.Gdc.Api.Core.Exceptions;
using Ksu.Gdc.Api.Core.Contracts;
using Ksu.Gdc.Api.Data.Entities;
using Ksu.Gdc.Api.Core.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ksu.Gdc.Api.Web.Controllers
{
    [Route("[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        [Route("", Name = "GetGroups")]
        public async Task<IActionResult> GetGroups([FromQuery] int userId)
        {
            try
            {
                List<Dto_Group> groups;
                if (userId == 0)
                {
                    groups = await _groupService.GetGroupsAsync();
                }
                else
                {
                    groups = await _groupService.GetGroupsByUserIdAsync(userId);
                }
                return Ok(groups);
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
                return Ok(group);
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
        public async Task<IActionResult> GetGroupMembers([FromRoute] int groupId)
        {
            try
            {
                var members = await _groupService.GetGroupMembersByGroupIdAsync(groupId);
                return Ok(members);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
