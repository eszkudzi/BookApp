using Microsoft.Extensions.DependencyInjection;
using BookApp;
using BookApp.Services;
using BookApp.Repositories;
using BookApp.Entities;
using BookApp.DataProviders;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IRepository<Book>, FileRepository<Book>>();
services.AddSingleton<IRepository<BookOwner>, FileRepository<BookOwner>>();

services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<IEventHandler, BookApp.Services.EventHandler>();
services.AddSingleton<IDataProvider, DataProvider>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>();
app.Run();