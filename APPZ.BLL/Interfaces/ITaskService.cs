using APPZ.DAL.DTO;
using APPZ.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APPZ.BLL.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<LabTask>> GetAllTasksAsync();
        Task<LabTask> GetTaskByIdAsync(int id);
        Task AddTaskAsync(LabTask labTask);
        Task UpdateAsync(LabTask labTask);
        Task DeleteAsync(int id);
        Task<LabTask> GetTaskByFilters(TaskFilterDTO filter);
        Task<ResultDTO> EvaluateAnswer(AnswerDTO answer);
    }
}
