﻿using System.Collections.Generic;
using CleanArchMvc.Domain.Entities;
using MediatR;

namespace CleanArchMvc.Application.Mediator.CQRS.Products.Queries;

public class GetProductsQuery : IRequest<IEnumerable<Product>>
{
}