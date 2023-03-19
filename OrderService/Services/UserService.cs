using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.Domain.Repositories;
using OrderService.Domain.Services;
using OrderService.DTOs;
using OrderService.Services.Communication;

namespace OrderService.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserMapper _userMapper;
    private readonly ISaveUserMapper _saveUserMapper;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUserRepository userRepository, IUserMapper userMapper, ISaveUserMapper saveUserMapper, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
        _saveUserMapper = saveUserMapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<UserDto>?> ListAsync()
    {
        var users = await _userRepository.ListAsync();
        if (users == null) return null;
        return _userMapper.ToListDtos(users);
    }
    
    public async Task<BaseResponse<UserDto>> AddAsync(SaveUserDto user) 
    {
        try
        {
            var userModel = _saveUserMapper.ToModel(user);
            await _userRepository.AddAsync(userModel);
            await _unitOfWork.CompleteAsync();

            return new BaseResponse<UserDto>(_userMapper.ToDto(userModel));
        }
        catch (Exception ex)
        {
            return new BaseResponse<UserDto>($"An error occurred: {ex.Message}");
        }
    }
    
    public async Task<BaseResponse<UserDto>> UpdateAsync(string id, SaveUserDto user) 
    {
        var existingUser = await _userRepository.GetAsync(id);

        if (existingUser == null)
            return new BaseResponse<UserDto>("Data not found.");

        existingUser.Name = user.Name;

        try
        {
            await _unitOfWork.CompleteAsync();

            return new BaseResponse<UserDto>(_userMapper.ToDto(existingUser));
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new BaseResponse<UserDto>($"An error occurred: {ex.Message}");
        }
    }
    
    public async Task<BaseResponse<UserDto>> RemoveAsync(string id) 
    {
        var existingUser = await _userRepository.GetAsync(id);

        if (existingUser == null)
            return new BaseResponse<UserDto>("Data not found.");

        try
        {
            _userRepository.Remove(existingUser);
            await _unitOfWork.CompleteAsync();

            return new BaseResponse<UserDto>(_userMapper.ToDto(existingUser));
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new BaseResponse<UserDto>($"An error occurred: {ex.Message}");
        }
    }
    
}