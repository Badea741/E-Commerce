// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECommerce
{
    using AutoMapper;
    using FluentValidation;
    using FluentValidation.Results;
    using Microsoft.AspNetCore.Mvc;

    [ApiController, Route("api/[controller]")]
    public class BaseController<TEntity, TViewModel> : ControllerBase
    where TEntity : class
    where TViewModel : class
    {
        protected readonly BaseUnitOfWork<TEntity> _unitOfWork;

        protected IMapper _mapper;
        private readonly AbstractValidator<TViewModel> _validator;

        public BaseController(BaseUnitOfWork<TEntity> unitOfWork, IMapper mapper, AbstractValidator<TViewModel> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
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
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(Guid id)
        {
            TEntity entity = await _unitOfWork.ReadByIdAsync(id);
            TViewModel entityViewModel = _mapper.Map<TViewModel>(entity);

            return Ok(entityViewModel);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TViewModel entityViewModel)
        {
            TEntity entity = _mapper.Map<TEntity>(entityViewModel);

            ValidationResult result = await _validator.ValidateAsync(entityViewModel);
            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));
        
            entity = await _unitOfWork.CreateAsync(entity);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(Post), new { id = entityViewModel.GetType().ToString() }, entityViewModel);
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