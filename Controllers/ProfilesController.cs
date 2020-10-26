using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _ps;
        private readonly KeepsService _ks;
        private readonly VaultsService _vs;
        public ProfilesController(ProfilesService ps, KeepsService ks, VaultsService vs)
        {
            _ps = ps;
            _ks = ks;
            _vs = vs;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Profile>> Get()
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_ps.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Profile>> GetProfile(string id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_ps.GetProfileById(id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}/keeps")]
        public async Task<ActionResult<IEnumerable<Keep>>> GetKeeps(string id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                Profile queryProfile = _ps.GetProfileById(id);
                return Ok(_ks.GetAllByCreatorId(queryProfile.Id, userInfo?.Id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}/vaults")]
        public async Task<ActionResult<IEnumerable<Vault>>> GetVaults(string id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                Profile queryProfile = _ps.GetProfileById(id);
                return Ok(_vs.GetVaultsById(userInfo?.Id, queryProfile.Id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}