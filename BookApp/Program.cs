using Microsoft.Extensions.DependencyInjection;
using BookApp;
using BookApp.DataProviders;
using BookApp.Entities;
using BookApp.Repositories;
using BookApp.Services;

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