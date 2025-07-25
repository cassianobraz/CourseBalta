﻿using Mediator.Abstractions;
using Mediator.Extensions;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddMediator(typeof(Program).Assembly);
//services.AddTransient<IMediator, Mediator.Mediator>();
services.AddTransient<AccountRepository>();
//services.AddTransient<IHandler<CreateAccountCommand, string>, CreateAccountHandler>();

var servicesProvider = services.BuildServiceProvider();
var mediator = servicesProvider.GetRequiredService<IMediator>();

var repository = new AccountRepository();
var request = new CreateAccountCommand { Username = "batman", Password = "123456" };
var result = await mediator.SendAsync(request);

Console.WriteLine(result);
public class AccountRepository
{
    public void Save()
    {
        Console.WriteLine("Saving...");
    }
}

public class CreateAccountCommand : IRequest<string>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class CreateAccountHandler(AccountRepository accountRepository) : IHandler<CreateAccountCommand, string>
{
    public Task<string> HandleAsync(CreateAccountCommand request, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Creating {request.Username} account...");
        accountRepository.Save();
        return Task.FromResult($"{request.Username} account created.");
    }
}