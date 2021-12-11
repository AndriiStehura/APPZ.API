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
            double distance = LevenshteinDistance(trimmedAnswer, trimmedEthalon);
            double rightInPercent = (1 - distance / trimmedEthalon.Length) * 100;
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

        private int LevenshteinDistance(string source1, string source2)
        {
            var source1Length = source1.Length;
            var source2Length = source2.Length;

            var matrix = new int[source1Length + 1, source2Length + 1];

            // First calculation, if one entry is empty return full length
            if (source1Length == 0)
                return source2Length;

            if (source2Length == 0)
                return source1Length;

            // Initialization of matrix with row size source1Length and columns size source2Length
            for (var i = 0; i <= source1Length; matrix[i, 0] = i++) { }
            for (var j = 0; j <= source2Length; matrix[0, j] = j++) { }

            // Calculate rows and collumns distances
            for (var i = 1; i <= source1Length; i++)
            {
                for (var j = 1; j <= source2Length; j++)
                {
                    var cost = (source2[j - 1] == source1[i - 1]) ? 0 : 1;

                    matrix[i, j] = Math.Min(
                        Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                        matrix[i - 1, j - 1] + cost);
                }
            }
            // return result
            return matrix[source1Length, source2Length];
        }
    }
}
