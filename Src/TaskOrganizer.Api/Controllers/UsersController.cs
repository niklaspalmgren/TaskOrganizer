using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskOrganizer.Api.Entities;
using TaskOrganizer.Api.Services;
using TaskOrganizer.Shared;

namespace TaskOrganizer.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserRepo _userRepo;

    public UsersController(
        IMapper mapper,
        IUserRepo userRepo)
    {
        _mapper = mapper;
        _userRepo = userRepo;
    }


    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetUsersAsync()
    {
        var userFromRepo = await _userRepo.GetUsersAsync();

        if (!userFromRepo.Any())
            return NotFound();

        var userDtos = _mapper.Map<List<User>, List<UserDto>>(userFromRepo);

        return Ok(userDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUserByIdAsync(int id)
    {
        var userFromRepo = await _userRepo.GetUserByIdAsync(id);

        if (userFromRepo == null)
            return NotFound();

        var userDto = _mapper.Map<UserDto>(userFromRepo);

        return Ok(userDto);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUserAsync(UserDto dto)
    {
        var user = _mapper.Map<User>(dto);

        _userRepo.AddUser(user);
        await _userRepo.SaveChangesAsync();

        var userDto = _mapper.Map<UserDto>(user);

        return CreatedAtAction("GetUserById", new { userDto.Id }, userDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUserAsync(int id, UserDto dto)
    {
        if (id != dto.Id)
            return BadRequest();

        var taskFromRepo = await _userRepo.GetUserByIdAsync(id);

        if (taskFromRepo == null)
            return NotFound();

        // Updates our entity from db context with values from our incoming dto
        _mapper.Map(dto, taskFromRepo);
        await _userRepo.SaveChangesAsync();

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTaskAsync(int id)
    {
        var userFromRepo = await _userRepo.GetUserByIdAsync(id);

        if (userFromRepo == null)
            return NotFound();

        _userRepo.DeleteUser(userFromRepo);
        await _userRepo.SaveChangesAsync();

        return NoContent();
    }
}