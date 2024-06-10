using LOGIC.Models;
using LOGIC.DTOs;
using System.ComponentModel.DataAnnotations;

namespace LOGIC
{
    public class ChoreService
    {
        private IChoreRepository repository;
        public ChoreService(IChoreRepository choreRepository, string _connectionString)
        {
            repository = choreRepository;
        }
        public List<Chore> GetChores(int projectId, string _connectionString)
        {
            List<ChoreDTO> choreDTOList = repository.GetChores(projectId, _connectionString);
            List<Chore> choreList = choreDTOList.ConvertAll(p => new Chore
            {
                Id = p.Id,
                ChoreName = p.ChoreName,
                Finished = p.Finished
            });

            return choreList;
        }

        public void RemoveChore(int choreId, string _connectionString)
        {
            repository.RemoveChore(choreId, _connectionString);
        }

        public string AddChore(string choreName, int projectId, string _connectionString)
        {
            if (choreName != null)
            {
                if (choreName.Length < 500)
                {
                    repository.AddChore(choreName, projectId, _connectionString);
                    return "Chore added";
                } 
                else
                {
                    return "Chore name is too long";
                }
            }
            else 
            { 
                return "Chore name cannot be empty";
            }
            
        }

        public void ToggleChoreStatus(int choreId, bool lastState, string _connectionString)
        {
            if (lastState)
            {
                repository.ToggleChoreStatus(choreId, 0, _connectionString);
            }
            else
            {
                repository.ToggleChoreStatus(choreId, 1, _connectionString);
            }
        }

        public Chore GetChore(int choreId, string _connectionString)
        {
            ChoreDTO choreDTO = repository.GetChore(choreId, _connectionString);
            Chore chore = new Chore
            {
                Id = choreDTO.Id,
                ChoreName = choreDTO.ChoreName,
                Finished = choreDTO.Finished
            };
            return chore;
        }

        public void UpdateChore(Chore chore, string _connectionString)
        {
            ChoreDTO choreDTO = new ChoreDTO
            {
                Id = chore.Id,
                ChoreName = chore.ChoreName,
                Finished = chore.Finished,
                Deadline = chore.Deadline
            };
            repository.UpdateChore(choreDTO, _connectionString);
        }
    }
}
