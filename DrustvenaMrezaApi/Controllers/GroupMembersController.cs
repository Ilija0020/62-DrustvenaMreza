using DrustvenaMrezaApi.Models;
using DrustvenaMrezaApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrustvenaMrezaApi.Controllers
{
    [Route("api/groups/{groupId}/users")]
    [ApiController]
    public class GroupMembersController : ControllerBase
    {

        private UserRepository userRepository = new UserRepository();
        private GroupMembersRepository membershipRepository = new GroupMembersRepository();
        private GroupRepository groupRepository = new GroupRepository();

        [HttpGet]
        public ActionResult<List<User>> GetUsersByGroup(int groupId)
        {
            if (!GroupRepository.Data.ContainsKey(groupId))
            {
                 return NotFound("Group not found.");
            }
            Group group = GroupRepository.Data[groupId];
            return Ok(group.Members);

        }
    }
}
