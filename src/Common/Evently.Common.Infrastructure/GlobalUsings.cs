﻿global using System.Buffers;
global using System.Data.Common;
global using System.Reflection;
global using System.Text.Json;
global using Evently.Common.Application.Caching;
global using Evently.Common.Application.Clock;
global using Evently.Common.Application.EventBus;
global using Evently.Common.Application.Persistence;
global using Evently.Common.Domain;
global using Evently.Common.Infrastructure.Services.Caching;
global using Evently.Common.Infrastructure.Services.Clock;
global using Evently.Common.Infrastructure.Services.EventBus;
global using Evently.Common.Infrastructure.Services.Persistence.Factories;
global using Evently.Common.Infrastructure.Services.Persistence.Interceptors;
global using MassTransit;
global using MediatR;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Diagnostics;
global using Microsoft.Extensions.Caching.Distributed;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.DependencyInjection.Extensions;
global using Npgsql;
global using StackExchange.Redis;
