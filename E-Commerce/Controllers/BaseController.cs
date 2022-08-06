// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECommerce
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    [ApiController, Route("api/[controller]")]
    public class BaseController<TEntity,TViewModel> :ControllerBase 
    where TEntity :class 
    where TViewModel :class
    {
        protected readonly BaseUnitOfWork<TEntity> _unitOfWork;

        protected IMapper _mapper;
        public BaseController(BaseUnitOfWork<TEntity> unitOfWork,IMapper mapper){
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }
        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(Guid id)
        {
            TEntity entity = await _unitOfWork.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
            return Ok(_mapper.Map<TViewModel>(entity));
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            List<TEntity> entities = await _unitOfWork.ReadAllAsync();
            return Ok(entities.Select(product => _mapper.Map<TViewModel>(product)));
            // return Ok(entities);
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(Guid id)
        {
            TEntity entity = await _unitOfWork.ReadByIdAsync(id);
            TViewModel entityViewModel = _mapper.Map<TViewModel>(entity);

            // FluentValidation.Results.ValidationResult validationResult = await new ProductValidator().ValidateAsync(productViewModel);

            // if (!validationResult.IsValid)
            //     return BadRequest(new { errors = validationResult.Errors });

            return Ok(entityViewModel);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TEntity entity)
        {
            entity = await _unitOfWork.CreateAsync(entity);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(Post), new { id = entity.GetType().ToString() }, entity);
        }

        // PUT api/<ProductsController>/5
        [HttpPut]
        public virtual async Task<IActionResult> Put([FromBody] TEntity entity)
        {
            entity = _unitOfWork.Update(entity);
            await _unitOfWork.SaveAsync();

            return Ok(_mapper.Map<TViewModel>(entity));
        }
    }
}