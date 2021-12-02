using APPZ.BLL.Interfaces;
using APPZ.DAL;
using APPZ.DAL.DTO;
using APPZ.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APPZ.BLL.Services
{
    public class TaskService: ITaskService
    {
        readonly IUnitOfWork _unit;

        public TaskService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<IEnumerable<LabTask>> GetAllTasksAsync() =>
            await _unit.TasksRepository.GetAsync(include: x => x.Include(y => y.Theme));

        public async Task<LabTask> GetTaskByIdAsync(int id) =>
            (await _unit.TasksRepository.GetAsync(
                x => x.Id == id,
                include: x => x.Include(y => y.Theme)))
            .FirstOrDefault();

        public async Task AddTaskAsync(LabTask labTask)
        {
            await _unit.TasksRepository.InsertAsync(labTask);
            await _unit.SaveAsync();
        }

        public async Task UpdateAsync(LabTask labTask)
        {
            _unit.TasksRepository.Update(labTask);
            await _unit.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unit.TasksRepository.Delete(id);
            await _unit.SaveAsync();
        }

        public async Task<LabTask> GetTaskByFilters(TaskFilterDTO filter)
        {
            var tasksByTheme = await _unit.TasksRepository.GetAsync(
                x => x.ThemeId == filter.ThemeId && x.ComplexityLevel == filter.Complexity,
                include: x => x.Include(y => y.Theme));

            if(tasksByTheme.Count == 0)
            {
                return null;
            }

            Random r = new Random();
            return tasksByTheme[r.Next(0, tasksByTheme.Count)];
        }

        public async Task<ResultDTO> EvaluateAnswer(AnswerDTO answer)
        {
            var task = await _unit.TasksRepository.GetByIdAsync(answer.TaskId);
            var trimmedEthalon = task.Answer.Trim().ToLower();
            var trimmedAnswer = answer.Answer.Trim().ToLower();
            int rightSymbols = trimmedEthalon.Length;
            int matchingSymbols = 0;
            for(int i = 0; i < rightSymbols; ++i)
            {
                if (i > trimmedAnswer.Length)
                    break;

                if(trimmedAnswer[i] == trimmedEthalon[i] ||
                    (char.IsWhiteSpace(trimmedAnswer[i]) && 
                        char.IsWhiteSpace(trimmedEthalon[i])))
                {
                    ++matchingSymbols;
                }
            }
            double rightInPercent = matchingSymbols * 100.0 / rightSymbols;
            var result = new ResultDTO
            {
                RightAnswer = task.Answer,
                RightInPercent = (int)Math.Round(rightInPercent, 0)
            };

            await _unit.StatisticsRepository.InsertAsync(new TaskStatistics
            {
                Grade = rightInPercent,
                TaskId = answer.TaskId,
                UserId = answer.UserId
            });
            await _unit.SaveAsync();

            return result;
        }
    }
}
