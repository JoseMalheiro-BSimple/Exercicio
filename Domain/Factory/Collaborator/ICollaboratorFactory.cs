﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;
public interface ICollaboratorFactory
{
    Task<Collaborator> Create(long userId, PeriodDateTime periodDateTime);
    Collaborator Create(ICollaboratorVisitor visitor);
}

