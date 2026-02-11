using DrustvenaMrezaApi.Repositories;
using DrustvenaMrezaApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using social_network_api.Repositories;

namespace DrustvenaMrezaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupMembersController : ControllerBase
    {

        private UserRepository userRepository = new UserRepository();
        private GroupMembersRepository membershipRepository = new GroupMembersRepository();
        private GroupRepository groupRepository = new GroupRepository();

        //Put api/groups/{groupId}/groups/{groupId}
        [HttpPut("{userId}")]
        public ActionResult<User> Add(int userId, int groupId)
        {
            if (!GroupRepository.Data.ContainsKey(groupId))
            {
                return NotFound("Group not found!");
            }
            if(!UserRepository.Data.ContainsKey(userId))
            {
                return NotFound("User not found!");
            }
            Group group = GroupRepository.Data[groupId];
            foreach (User member in group.Members)
            {
                if (member.Id == userId)
                {
                    return Conflict();
                }
            }
            User user = UserRepository.Data[userId];
            group.Members.Add(user);
            membershipRepository.Save();

            return Ok(group);

        }
        [HttpDelete("{userId}")]
        public ActionResult<Group> Remove(int groupId, int userId)
        {
            if (!GroupRepository.Data.ContainsKey(groupId))
            {
                return NotFound("Group not found");
            }

            if (!UserRepository.Data.ContainsKey(userId))
            {
                return NotFound("User not found");
            }

            Group group = GroupRepository.Data[groupId];
            User user = UserRepository.Data[userId];
            group.Members.Remove(user);
            // Sačuvamo podatke o članstvima
            membershipRepository.Save();

            return Ok(group);
        }

    }
}