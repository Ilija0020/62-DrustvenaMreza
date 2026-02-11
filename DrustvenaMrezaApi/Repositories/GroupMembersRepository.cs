using DrustvenaMrezaApi.Models;
using DrustvenaMrezaApi.Repositories;
using DrustvenaMrezaApi.Models;

namespace DrustvenaMrezaApi.Repositories
{
    public class GroupMembersRepository
    {

        private const string membershipFilePath = "data/memberships.csv"; // Putanja do fajla sa članstvom
        public static Dictionary<int, List<int>> Data;

        public GroupMembersRepository()
        {
            if (Data == null)
            {
                Load();
            }
        }
        private void Load()
        {
            // Dictionary gde je ključ groupId, a vrednost lista korisnika (ID-evi korisnika)
            Data = new Dictionary<int, List<int>>();
            string[] lines = File.ReadAllLines(membershipFilePath);

            foreach (string line in lines)
            {
                string[] attributes = line.Split(',');

                int userId = int.Parse(attributes[0]);  // ID korisnika
                int groupId = int.Parse(attributes[1]); // ID grupe

                if (!Data.ContainsKey(groupId))
                {
                    Data[groupId] = new List<int>();  // Ako grupa nije u mapi, dodaj je
                }
                Data[groupId].Add(userId);  // Dodaj korisnika u grupu
            }
        }


        public void Save()
        {
            List<string> lines = new List<string>();
            foreach (Group group in GroupRepository.Data.Values)
            {
                foreach (User member in group.Members)
                {
                    lines.Add($"{member.Id},{group.Id}");
                }
            }
            File.WriteAllLines(membershipFilePath, lines);
        }
    }
}
