﻿using AutoMapper;
using JWTBearer.Application.DataTransferObjects;
using JWTBearer.Application.Interface;
using JWTBearer.Domain.Entities;

namespace JWTBearer.Application.Service
{

    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashingHelper _hashingHelper;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IHashingHelper hashingHelper, IMapper mapper)
        {
            _userRepository = userRepository;
            _hashingHelper = hashingHelper;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(UserRegisterDto userRegisterDto)
        {
            userRegisterDto.Password = _hashingHelper.HashPassword(userRegisterDto.Password);
            User userEntity = _mapper.Map<User>(userRegisterDto);
            await _userRepository.AddUser(userEntity);
            return true;
        }

        public async Task<bool> GetByUserNameAsync(UserLoginDto userLoginDto)
        {
            User userEntity = await _userRepository.GetByUserNameAsync(userLoginDto.Username);

            if (userEntity == null)
            {
                return false;
            }

            string hashedPassword = _hashingHelper.HashPassword(userLoginDto.Password);

            if (userEntity.Username == userLoginDto.Username && hashedPassword == userEntity.Password)
            {
                return true;
            }

            return false;
        }
    }
}
