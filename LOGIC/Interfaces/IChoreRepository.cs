using LOGIC.DTOs;

namespace LOGIC
{
    public interface IChoreRepository
    {
        List<ChoreDTO> GetChores(int projectId, string _connectionString);
        void AddChore(string chore, int projectId, string _connectionString);
        void RemoveChore(int choreId, string _connectionString);
        void ToggleChoreStatus(int choreId, int status, string _connectionString);
        ChoreDTO GetChore(int choreId, string _connectionString);
        void UpdateChore(ChoreDTO chore, string _connectionString);

    }
}
