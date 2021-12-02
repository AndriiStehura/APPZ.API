using APPZ.BLL.Interfaces;
using APPZ.DAL.DTO;
using APPZ.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace APPZ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        readonly ITaskService _taskService;
        
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasksAsync() => 
            Ok(await _taskService.GetAllTasksAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskByIdAsync([FromRoute] int id) => 
            Ok(await _taskService.GetTaskByIdAsync(id));

        [HttpGet("byfilter")]
        public async Task<IActionResult> GetTaskByFilter([FromQuery] TaskFilterDTO filter) => 
            Ok(await _taskService.GetTaskByFilters(filter));

        [HttpPost]
        public async Task<IActionResult> AddNewTask([FromBody] LabTask task)
        {
            await _taskService.AddTaskAsync(task);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask([FromBody] LabTask task)
        {
            await _taskService.UpdateAsync(task);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskService.DeleteAsync(id);
            return Ok();
        }

        [HttpPost("check")]
        public async Task<IActionResult> CheckTask([FromBody] AnswerDTO answer)
        {
            var result = await _taskService.EvaluateAnswer(answer);
            return Ok(result);
        }
    }
}
