﻿using CleanArchitecture.Domain.Entites.OrderLines;
using CleanArchitecture.Domain.Primitives.DomainEvents;

namespace CleanArchitecture.Domain.DomainEvents.OrderLines;

public record OrderLineUpdatedDomainEvent(Guid Id, OrderLineId OrderLineId) : DomainEvent(Id);