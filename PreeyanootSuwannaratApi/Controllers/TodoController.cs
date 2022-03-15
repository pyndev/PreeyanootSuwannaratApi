using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PreeyanootSuwannaratApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        //Create this to access data from database
        private readonly DataContext _context;

        public TodoController(DataContext context)
        {
            _context = context;
        }

        //Get All Todo List
        [HttpGet]
        public async Task<ActionResult<List<TodoItem>>> Get()
        {
            return Ok(await _context.TodoItems.ToListAsync());
        }

        //Get Todo by id
        [HttpGet("{id}")]
        public async Task<ActionResult<List<TodoItem>>> Get(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);

            if (item == null)
            {
                return BadRequest("Item not found.");
            }

            return Ok(item);
        }

        //Create Todo item
        [HttpPost]
        public async Task<ActionResult<List<TodoItem>>> AddItem(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
            return Ok(await _context.TodoItems.ToListAsync());
        }

        //Update Todo item
        [HttpPut]
        public async Task<ActionResult<List<TodoItem>>> UpdateItem(TodoItem req)
        {
            var item = await _context.TodoItems.FindAsync(req.Id);
            if (item == null)
            {
                return BadRequest("Item not found.");
            }
            item.Name = req.Name;
            item.IsComplete = req.IsComplete;

            await _context.SaveChangesAsync();

            return Ok(await _context.TodoItems.ToListAsync());
        }

        //Delete Todo item
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<TodoItem>>> Delete(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item == null)
                return BadRequest("Task item not found.");

            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();

            return Ok(await _context.TodoItems.ToListAsync());
        }

    }
}
