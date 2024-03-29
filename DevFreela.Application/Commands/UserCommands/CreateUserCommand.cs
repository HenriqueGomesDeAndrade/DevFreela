﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.UserCommands
{
    public class CreateUserCommand : IRequest<int>
    {
        public CreateUserCommand(string fullName, string email, string password, DateTime birthDate, string role)
        {
            FullName = fullName;
            Email = email;
            Password = password;
            BirthDate = birthDate;
            Role = role;
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }

        public DateTime BirthDate { get; private set; }
    }
}
