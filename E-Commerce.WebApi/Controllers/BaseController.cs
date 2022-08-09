// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECommerce
{
    using AutoMapper;
    using FluentValidation;
    using FluentValidation.Results;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ECommerce.Constants;
    using System.Security.Claims;


    [ApiController, Route("api/[controller]")]
    [Authorize(Roles = $"{Roles.Admin},{Roles.User}")]
    public class BaseController<TEntity, TViewModel> : ControllerBase
        where TEntity : class
        where TViewModel : class
    {
        protected readonly BaseUnitOfWork<TEntity> _unitOfWork;

        protected IMapper _mapper;
        private readonly AbstractValidator<TEntity> _validator;

        public BaseController(BaseUnitOfWork<TEntity> unitOfWork, IMapper mapper, AbstractValidator<TEntity> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }
        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(Guid id)
        {
            foreach (Claim claim in User.Claims)
            {
                if (claim.Value == Roles.User)
                    return Unauthorized("User not authorized");
            }
            TEntity entity = await _unitOfWork.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
            return Ok(_mapper.Map<TViewModel>(entity));
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            foreach (Claim claim in User.Claims)
            {
                if (claim.Value == Roles.User
                    && (typeof(TEntity) == typeof(ECommerce.Admin)
                    || typeof(TEntity) == typeof(ECommerce.User)))
                    return Unauthorized("User not authorized");
            }
            List<TEntity> entities = await _unitOfWork.ReadAllAsync();
            return Ok(entities.Select(product => _mapper.Map<TViewModel>(product)));
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(Guid id)
        {
            foreach (Claim claim in User.Claims)
            {
                if (claim.Value == Roles.User
                    && (typeof(TEntity) == typeof(ECommerce.Admin)
                    || typeof(TEntity) == typeof(ECommerce.User)))
                    return Unauthorized("User not authorized");
            }
            TEntity entity = await _unitOfWork.ReadByIdAsync(id);
            TViewModel entityViewModel = _mapper.Map<TViewModel>(entity);

            return Ok(entityViewModel);
        }

        // POST api/<ProductsController>
        [AllowAnonymous]
        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TViewModel entityViewModel)
        {
            foreach (Claim claim in User.Claims)
            {
                if (claim.Value == Roles.User
                    && (typeof(TEntity) == typeof(ECommerce.Category)
                    || typeof(TEntity) == typeof(ECommerce.Product)
                    || typeof(TEntity) == typeof(ECommerce.Admin)))
                    return Unauthorized("User not authorized");
            }

            TEntity entity = _mapper.Map<TEntity>(entityViewModel);

            ValidationResult result = await _validator.ValidateAsync(entity);
            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            entity = await _unitOfWork.CreateAsync(entity);
            try
            {
                await _unitOfWork.SaveAsync();
                return CreatedAtAction(nameof(Post), new { id = entity.GetType().ToString() }, entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        // PUT api/<ProductsController>/5
        [HttpPut]
        public virtual async Task<IActionResult> Put([FromBody] TEntity entity)
        {
            foreach (Claim claim in User.Claims)
            {
                if (claim.Value == Roles.User)
                    return Unauthorized("User not authorized");
            }
            entity = _unitOfWork.Update(entity);
            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

            return Ok(_mapper.Map<TViewModel>(entity));
        }
    }
}