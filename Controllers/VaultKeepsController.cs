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
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vks;
        private readonly KeepsService _ks;
        public VaultKeepsController(VaultKeepsService vks, KeepsService ks)
        {
            _vks = vks;
            _ks = ks;
        }

        // [HttpGet("{id}")]
        // [Authorize]
        // public async Task<ActionResult<string>> GetVaultKeep([FromBody] string keepId, int id){}


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep newVc)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newVc.CreatorId = userInfo.Id;
                VaultKeep created = _vks.Create(userInfo.Id, newVc);
                _ks.AddedToVault(created.KeepId);
                return Ok(created);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<VaultKeep>> Delete(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                _vks.Delete(userInfo.Id, id);
                return Ok("Successfully Delorted");
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}